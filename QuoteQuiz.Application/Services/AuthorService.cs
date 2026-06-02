using QuoteQuiz.Application.Contracts.Repositories;
using QuoteQuiz.Application.Contracts.Services;
using QuoteQuiz.Application.DTOs;
using QuoteQuiz.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuoteQuiz.Application.Services
{
    public class AuthorService : IAuthorService
    {
        private readonly IAuthorRepository _authors;

        public AuthorService(IAuthorRepository authors)
        {
            _authors = authors;
        }

        public async Task<List<AuthorDto>> GetAllAsync(CancellationToken ct = default)
        {
            var list = await _authors.GetAllAsync(ct);
            return list.Select(a => new AuthorDto(a.Id, a.Name)).ToList();
        }

        public async Task<AuthorDto?> GetByIdAsync(int id, CancellationToken ct = default)
        {
            var author = await _authors.GetByIdAsync(id, ct);
            return author is null ? null : new AuthorDto(author.Id, author.Name);
        }

        public async Task<AuthorDto> CreateAsync(CreateAuthorDto dto, CancellationToken ct = default)
        {
            var author = new Author { Name = dto.Name };
            await _authors.AddAsync(author, ct);
            return new AuthorDto(author.Id, author.Name);
        }
    }
}
