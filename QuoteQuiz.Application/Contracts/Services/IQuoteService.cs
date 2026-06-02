using QuoteQuiz.Application.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuoteQuiz.Application.Contracts.Services
{
    public interface IQuoteService
    {
        Task<List<QuoteDto>> GetAllAsync(CancellationToken ct = default);
        Task<QuoteDto?> GetByIdAsync(int id, CancellationToken ct = default);
        Task<QuoteDto> CreateAsync(CreateQuoteDto dto, CancellationToken ct = default);
        Task<QuoteDto?> UpdateAsync(int id, UpdateQuoteDto dto, CancellationToken ct = default);
        Task<bool> DeleteAsync(int id, CancellationToken ct = default);
    }
}
