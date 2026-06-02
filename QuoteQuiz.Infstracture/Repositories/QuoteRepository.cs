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
    public class QuoteRepository : IQuoteRepository
    {
        private readonly QuizDbContext _context;

        public QuoteRepository(QuizDbContext context)
        {
            _context = context;
        }

        public Task<Quote?> GetByIdAsync(int id, CancellationToken ct = default) =>
            _context.Quotes.Include(q => q.Author)
                .FirstOrDefaultAsync(q => q.Id == id, ct);

        public Task<List<Quote>> GetAllAsync(CancellationToken ct = default) =>
            _context.Quotes.Include(q => q.Author)
                .OrderBy(q => q.Author.Name)
                .ToListAsync(ct);

        public Task<List<Quote>> GetActiveAsync(CancellationToken ct = default) =>
            _context.Quotes.Include(q => q.Author).ToListAsync(ct);

        public async Task AddAsync(Quote quote, CancellationToken ct = default)
        {
            _context.Quotes.Add(quote);
            await _context.SaveChangesAsync(ct);
        }

        public async Task UpdateAsync(Quote quote, CancellationToken ct = default)
        {
            _context.Quotes.Update(quote);
            await _context.SaveChangesAsync(ct);
        }

        public async Task DeleteAsync(Quote quote, CancellationToken ct = default)
        {
            _context.Quotes.Remove(quote);
            await _context.SaveChangesAsync(ct);
        }
    }
}
