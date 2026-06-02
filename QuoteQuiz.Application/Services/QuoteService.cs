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
    public class QuoteService : IQuoteService
    {
        private readonly IQuoteRepository _quotes;
        private readonly IAuthorRepository _authors;

        public QuoteService(IQuoteRepository quotes, IAuthorRepository authors)
        {
            _quotes = quotes;
            _authors = authors;
        }

        public async Task<List<QuoteDto>> GetAllAsync(CancellationToken ct = default)
        {
            var list = await _quotes.GetAllAsync(ct);
            return list.Select(q => new QuoteDto(q.Id, q.Text, q.AuthorId, q.Author.Name)).ToList();
        }

        public async Task<QuoteDto?> GetByIdAsync(int id, CancellationToken ct = default)
        {
            var quote = await _quotes.GetByIdAsync(id, ct);
            return quote is null ? null : new QuoteDto(quote.Id, quote.Text, quote.AuthorId, quote.Author.Name);
        }

        public async Task<QuoteDto> CreateAsync(CreateQuoteDto dto, CancellationToken ct = default)
        {
            var author = await _authors.GetByIdAsync(dto.AuthorId, ct)
                         ?? throw new InvalidOperationException("Author not found.");

            var quote = new Quote
            {
                Text = dto.Text,
                AuthorId = author.Id
            };

            await _quotes.AddAsync(quote, ct);
            return new QuoteDto(quote.Id, quote.Text, quote.AuthorId, author.Name);
        }

        public async Task<QuoteDto?> UpdateAsync(int id, UpdateQuoteDto dto, CancellationToken ct = default)
        {
            var quote = await _quotes.GetByIdAsync(id, ct);
            if (quote is null) return null;

            var author = await _authors.GetByIdAsync(dto.AuthorId, ct)
                         ?? throw new InvalidOperationException("Author not found.");

            quote.Text = dto.Text;
            quote.AuthorId = author.Id;

            await _quotes.UpdateAsync(quote, ct);
            return new QuoteDto(quote.Id, quote.Text, quote.AuthorId, author.Name);
        }

        public async Task<bool> DeleteAsync(int id, CancellationToken ct = default)
        {
            var quote = await _quotes.GetByIdAsync(id, ct);
            if (quote is null) return false;

            await _quotes.DeleteAsync(quote, ct);
            return true;
        }
    }
}
