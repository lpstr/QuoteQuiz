using QuoteQuiz.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuoteQuiz.Application.Contracts.Repositories
{
    public interface IQuoteRepository
    {
        Task<Quote?> GetByIdAsync(int id, CancellationToken ct = default);
        Task<List<Quote>> GetAllAsync(CancellationToken ct = default);
        Task<List<Quote>> GetActiveAsync(CancellationToken ct = default);
        Task AddAsync(Quote quote, CancellationToken ct = default);
        Task UpdateAsync(Quote quote, CancellationToken ct = default);
        Task DeleteAsync(Quote quote, CancellationToken ct = default);
    }
}
