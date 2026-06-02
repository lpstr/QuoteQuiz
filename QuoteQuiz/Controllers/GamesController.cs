using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using QuoteQuiz.Application.Contracts.Services;
using QuoteQuiz.Application.DTOs;
using QuoteQuiz.Domain.Enums;

namespace QuoteQuiz.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class GamesController : ControllerBase
    {
        private readonly IGameService _games;

        public GamesController(IGameService games)
        {
            _games = games;
        }

        [HttpPost("start")]
        public async Task<ActionResult<StartGameResponse>> Start(StartGameRequest request, CancellationToken ct)
        {
            var result = await _games.StartGameAsync(request, ct);
            return Ok(result);
        }

        [HttpGet("{sessionId:int}/next")]
        public async Task<ActionResult<NextQuestionResponse>> Next(
            int sessionId,
            [FromQuery] GameMode mode,
            CancellationToken ct)
        {
            var result = await _games.GetNextQuestionAsync(sessionId, mode, ct);
            if (result is null) return NoContent();
            return Ok(result);
        }

        [HttpPost("answer")]
        public async Task<ActionResult<SubmitAnswerResponse>> Answer(SubmitAnswerRequest request, CancellationToken ct)
        {
            var result = await _games.SubmitAnswerAsync(request, ct);
            if (result is null) return BadRequest();
            return Ok(result);
        }

        [HttpGet("user/{userId:int}/sessions")]
        public async Task<ActionResult<List<GameSessionReviewDto>>> GetUserSessions(int userId, CancellationToken ct)
        {
            var result = await _games.GetUserSessionsAsync(userId, ct);
            return Ok(result);
        }
    }
}
