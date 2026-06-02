using QuoteQuiz.Application.Contracts.Repositories;
using QuoteQuiz.Application.Contracts.Services;
using QuoteQuiz.Application.DTOs;
using QuoteQuiz.Domain.Entities;
using QuoteQuiz.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuoteQuiz.Application.Services
{
    public class GameService : IGameService
    {
        private readonly IGameSessionRepository _sessions;
        private readonly IQuoteRepository _quotes;
        private readonly IAuthorRepository _authors;
        private readonly IUserRepository _users;
        private readonly Random _random = new();

        public GameService(
            IGameSessionRepository sessions,
            IQuoteRepository quotes,
            IAuthorRepository authors,
            IUserRepository users)
        {
            _sessions = sessions;
            _quotes = quotes;
            _authors = authors;
            _users = users;
        }

        public async Task<StartGameResponse> StartGameAsync(StartGameRequest request, CancellationToken ct = default)
        {
            var user = await _users.GetByIdAsync(request.UserId, ct);
            if (user is null || user.IsDisabled)
                throw new InvalidOperationException("User not found or disabled.");

            var session = new GameSession
            {
                UserId = user.Id,
                StartedAt = DateTime.UtcNow
            };

            await _sessions.AddAsync(session, ct);
            return new StartGameResponse(session.Id, request.Mode);
        }

        public async Task<NextQuestionResponse?> GetNextQuestionAsync(int sessionId, GameMode mode, CancellationToken ct = default)
        {
            var session = await _sessions.GetByIdAsync(sessionId, ct);
            if (session is null) return null;

            var allQuotes = await _quotes.GetActiveAsync(ct);
            if (!allQuotes.Any()) return null;

            var usedQuoteIds = session.Questions.Select(q => q.QuoteId).ToHashSet();
            var remaining = allQuotes.Where(q => !usedQuoteIds.Contains(q.Id)).ToList();
            if (!remaining.Any())
            {
                session.FinishedAt = DateTime.UtcNow;
                await _sessions.UpdateAsync(session, ct);
                return null;
            }

            var quote = remaining[_random.Next(remaining.Count)];
            var allAuthors = await _authors.GetAllAsync(ct);
            var correctAuthor = allAuthors.First(a => a.Id == quote.AuthorId);

            if (mode == GameMode.Binary)
            {
                var suggested = PickRandomAuthor(allAuthors, correctAuthor.Id);

                return new NextQuestionResponse(
                    session.Id,
                    quote.Id,
                    quote.Text,
                    GameMode.Binary,
                    suggested.Id,
                    suggested.Name,
                    null
                );
            }
            else
            {
                var options = BuildMultipleChoiceOptions(allAuthors, correctAuthor);
                var optionDtos = options.Select(a => new AuthorOptionDto(a.Id, a.Name)).ToList();

                return new NextQuestionResponse(
                    session.Id,
                    quote.Id,
                    quote.Text,
                    GameMode.MultipleAnswer,
                    null,
                    null,
                    optionDtos
                );
            }
        }

        public async Task<SubmitAnswerResponse?> SubmitAnswerAsync(SubmitAnswerRequest request, CancellationToken ct = default)
        {
            var session = await _sessions.GetByIdAsync(request.SessionId, ct);
            if (session is null) return null;

            var quote = await _quotes.GetByIdAsync(request.QuoteId, ct);
            if (quote is null) return null;

            var correctAuthorId = quote.AuthorId;
            bool isCorrect;
            int? suggestedAuthorId = null;
            int? selectedAuthorId = null;
            bool? answerYesNo = null;

            if (request.Mode == GameMode.Binary)
            {
                if (request.AnswerYesNo is null || request.SelectedAuthorId is null)
                    throw new InvalidOperationException("Binary mode requires AnswerYesNo and SelectedAuthorId (suggested).");

                answerYesNo = request.AnswerYesNo.Value;
                suggestedAuthorId = request.SelectedAuthorId.Value;

                if (answerYesNo.Value)
                {
                    isCorrect = suggestedAuthorId == correctAuthorId;
                }
                else
                {
                    isCorrect = suggestedAuthorId != correctAuthorId;
                }
            }
            else
            {
                if (request.SelectedAuthorId is null)
                    throw new InvalidOperationException("MultipleChoice mode requires SelectedAuthorId.");

                selectedAuthorId = request.SelectedAuthorId.Value;
                isCorrect = selectedAuthorId == correctAuthorId;
            }

            var question = new GameQuestion
            {
                GameSessionId = session.Id,
                QuoteId = quote.Id,
                Mode = request.Mode,
                SuggestedAuthorId = suggestedAuthorId,
                SelectedAuthorId = selectedAuthorId,
                AnswerYesNo = answerYesNo,
                IsCorrect = isCorrect,
                AnsweredAt = DateTime.UtcNow
            };

            session.Questions.Add(question);
            await _sessions.UpdateAsync(session, ct);

            var correctAuthor = (await _authors.GetByIdAsync(correctAuthorId, ct))!;
            return new SubmitAnswerResponse(
                isCorrect,
                correctAuthor.Name,
                quote.Text
            );
        }

        public async Task<List<GameSessionReviewDto>> GetUserSessionsAsync(int userId, CancellationToken ct = default)
        {
            var sessions = await _sessions.GetByUserAsync(userId, ct);

            return sessions.Select(s =>
            {
                var questions = s.Questions.Select(q =>
                    new GameQuestionReviewDto(
                        q.Id,
                        q.Quote.Text,
                        q.Quote.Author.Name,
                        q.Mode,
                        q.IsCorrect,
                        q.AnswerYesNo,
                        q.SelectedAuthor?.Name,
                        q.SuggestedAuthor?.Name,
                        q.AnsweredAt
                    )).ToList();

                return new GameSessionReviewDto(
                    s.Id,
                    s.StartedAt,
                    s.FinishedAt,
                    questions.Count,
                    questions.Count(q => q.IsCorrect),
                    questions
                );
            }).ToList();
        }

        private Author PickRandomAuthor(List<Author> allAuthors, int correctAuthorId)
        {
            if (allAuthors.Count <= 1)
                return allAuthors.First(a => a.Id == correctAuthorId);

            Author candidate;
            do
            {
                candidate = allAuthors[_random.Next(allAuthors.Count)];
            } while (candidate.Id == correctAuthorId);

            return candidate;
        }

        private List<Author> BuildMultipleChoiceOptions(List<Author> allAuthors, Author correctAuthor)
        {
            var options = new HashSet<Author> { correctAuthor };

            while (options.Count < 3 && allAuthors.Count > options.Count)
            {
                var candidate = allAuthors[_random.Next(allAuthors.Count)];
                options.Add(candidate);
            }

            return options.OrderBy(_ => _random.Next()).ToList();
        }
    }
}
