using QuoteQuiz.Application.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuoteQuiz.Application.Contracts.Services
{
    public interface IAuthorService
    {
        Task<List<AuthorDto>> GetAllAsync(CancellationToken ct = default);
        Task<AuthorDto?> GetByIdAsync(int id, CancellationToken ct = default);
        Task<AuthorDto> CreateAsync(CreateAuthorDto dto, CancellationToken ct = default);
    }
}
