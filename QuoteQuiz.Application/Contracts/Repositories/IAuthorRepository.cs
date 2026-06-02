using QuoteQuiz.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuoteQuiz.Application.Contracts.Repositories
{
    public interface IAuthorRepository
    {
        Task<Author?> GetByIdAsync(int id, CancellationToken ct = default);
        Task<List<Author>> GetAllAsync(CancellationToken ct = default);
        Task AddAsync(Author author, CancellationToken ct = default);
    }
}
