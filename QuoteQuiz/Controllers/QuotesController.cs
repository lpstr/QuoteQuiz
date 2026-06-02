using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using QuoteQuiz.Application.Contracts.Services;
using QuoteQuiz.Application.DTOs;

namespace QuoteQuiz.API.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class QuotesController : ControllerBase
    {
        private readonly IQuoteService _quotes;

        public QuotesController(IQuoteService quotes)
        {
            _quotes = quotes;
        }

        [HttpGet]
        public async Task<ActionResult<List<QuoteDto>>> GetAll(CancellationToken ct)
        {
            var result = await _quotes.GetAllAsync(ct);
            return Ok(result);
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult<QuoteDto>> GetById(int id, CancellationToken ct)
        {
            var quote = await _quotes.GetByIdAsync(id, ct);
            if (quote is null) return NotFound();
            return Ok(quote);
        }

        [HttpPost]
        public async Task<ActionResult<QuoteDto>> Create(CreateQuoteDto dto, CancellationToken ct)
        {
            var created = await _quotes.CreateAsync(dto, ct);
            return CreatedAtAction(nameof(GetById), new { id = created.Id }, created);
        }

        [HttpPut("{id:int}")]
        public async Task<ActionResult<QuoteDto>> Update(int id, UpdateQuoteDto dto, CancellationToken ct)
        {
            var updated = await _quotes.UpdateAsync(id, dto, ct);
            if (updated is null) return NotFound();
            return Ok(updated);
        }

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> Delete(int id, CancellationToken ct)
        {
            var ok = await _quotes.DeleteAsync(id, ct);
            if (!ok) return NotFound();
            return NoContent();
        }
    }
}
