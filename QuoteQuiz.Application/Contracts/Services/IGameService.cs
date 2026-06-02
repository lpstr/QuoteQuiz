using QuoteQuiz.Application.DTOs;
using QuoteQuiz.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuoteQuiz.Application.Contracts.Services
{
    public interface IGameService
    {
        Task<StartGameResponse> StartGameAsync(StartGameRequest request, CancellationToken ct = default);
        Task<NextQuestionResponse?> GetNextQuestionAsync(int sessionId, GameMode mode, CancellationToken ct = default);
        Task<SubmitAnswerResponse?> SubmitAnswerAsync(SubmitAnswerRequest request, CancellationToken ct = default);
        Task<List<GameSessionReviewDto>> GetUserSessionsAsync(int userId, CancellationToken ct = default);
    }
}
