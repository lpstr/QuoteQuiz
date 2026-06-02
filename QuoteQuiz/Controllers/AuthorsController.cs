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
    public class AuthorsController : ControllerBase
    {
        private readonly IAuthorService _authors;

        public AuthorsController(IAuthorService authors)
        {
            _authors = authors;
        }

        [HttpGet]
        public async Task<ActionResult<List<AuthorDto>>> GetAll(CancellationToken ct)
        {
            var result = await _authors.GetAllAsync(ct);
            return Ok(result);
        }

        [HttpPost]
        public async Task<ActionResult<AuthorDto>> Create(CreateAuthorDto dto, CancellationToken ct)
        {
            var created = await _authors.CreateAsync(dto, ct);
            return CreatedAtAction(nameof(GetAll), new { id = created.Id }, created);
        }
    }
}
