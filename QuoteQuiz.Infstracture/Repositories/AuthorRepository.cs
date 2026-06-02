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
    public class AuthorRepository : IAuthorRepository
    {
        private readonly QuizDbContext _context;

        public AuthorRepository(QuizDbContext context)
        {
            _context = context;
        }

        public Task<Author?> GetByIdAsync(int id, CancellationToken ct = default) =>
            _context.Authors.FirstOrDefaultAsync(a => a.Id == id, ct);

        public Task<List<Author>> GetAllAsync(CancellationToken ct = default) =>
            _context.Authors.OrderBy(a => a.Name).ToListAsync(ct);

        public async Task AddAsync(Author author, CancellationToken ct = default)
        {
            _context.Authors.Add(author);
            await _context.SaveChangesAsync(ct);
        }
    }
}
