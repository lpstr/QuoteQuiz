using QuoteQuiz.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuoteQuiz.Application.Contracts.Repositories
{
    public interface IGameSessionRepository
    {
        Task<GameSession?> GetByIdAsync(int id, CancellationToken ct = default);
        Task AddAsync(GameSession session, CancellationToken ct = default);
        Task UpdateAsync(GameSession session, CancellationToken ct = default);
        Task<List<GameSession>> GetByUserAsync(int userId, CancellationToken ct = default);
    }
}
