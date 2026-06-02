using Microsoft.EntityFrameworkCore;
using QuoteQuiz.Application.Contracts.Repositories;
using QuoteQuiz.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuoteQuiz.Infrastructure.Repositories
{
    public class GameSessionRepository : IGameSessionRepository
    {
        private readonly QuizDbContext _context;

        public GameSessionRepository(QuizDbContext context)
        {
            _context = context;
        }

        public Task<GameSession?> GetByIdAsync(int id, CancellationToken ct = default) =>
            _context.GameSessions
                .Include(gs => gs.Questions)
                    .ThenInclude(q => q.Quote)
                        .ThenInclude(q => q.Author)
                .Include(gs => gs.Questions)
                    .ThenInclude(q => q.SuggestedAuthor)
                .Include(gs => gs.Questions)
                    .ThenInclude(q => q.SelectedAuthor)
                .FirstOrDefaultAsync(gs => gs.Id == id, ct);

        public async Task AddAsync(GameSession session, CancellationToken ct = default)
        {
            _context.GameSessions.Add(session);
            await _context.SaveChangesAsync(ct);
        }

        public async Task UpdateAsync(GameSession session, CancellationToken ct = default)
        {
            _context.GameSessions.Update(session);
            await _context.SaveChangesAsync(ct);
        }

        public Task<List<GameSession>> GetByUserAsync(int userId, CancellationToken ct = default) =>
            _context.GameSessions
                .Include(gs => gs.Questions)
                    .ThenInclude(q => q.Quote)
                        .ThenInclude(q => q.Author)
                .Include(gs => gs.Questions)
                    .ThenInclude(q => q.SuggestedAuthor)
                .Include(gs => gs.Questions)
                    .ThenInclude(q => q.SelectedAuthor)
                .Where(gs => gs.UserId == userId)
                .OrderByDescending(gs => gs.StartedAt)
                .ToListAsync(ct);

    }
}