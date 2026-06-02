using QuoteQuiz.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuoteQuiz.Application.DTOs
{
    public record StartGameRequest(int UserId, GameMode Mode);
    public record StartGameResponse(int SessionId, GameMode Mode);

    public record AuthorOptionDto(int Id, string Name);

    public record NextQuestionResponse(
        int SessionId,
        int QuoteId,
        string QuoteText,
        GameMode Mode,
        int? SuggestedAuthorId,
        string? SuggestedAuthorName,
        List<AuthorOptionDto>? Options
    );

    public record SubmitAnswerRequest(
        int SessionId,
        int QuoteId,
        GameMode Mode,
        int? SelectedAuthorId,
        bool? AnswerYesNo
    );

    public record SubmitAnswerResponse(
        bool IsCorrect,
        string CorrectAuthor,
        string QuoteText
    );

    public record GameQuestionReviewDto(
        int QuestionId,
        string QuoteText,
        string AuthorName,
        GameMode Mode,
        bool IsCorrect,
        bool? AnswerYesNo,
        string? SelectedAuthorName,
        string? SuggestedAuthorName,
        DateTime AnsweredAt
    );

    public record GameSessionReviewDto(
        int SessionId,
        DateTime StartedAt,
        DateTime? FinishedAt,
        int TotalQuestions,
        int CorrectAnswers,
        List<GameQuestionReviewDto> Questions
    );
}
