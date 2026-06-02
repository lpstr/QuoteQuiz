using QuoteQuiz.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuoteQuiz.Domain.Entities
{
    public class GameQuestion
    {
        public int Id { get; set; }

        public int GameSessionId { get; set; }
        public GameSession GameSession { get; set; } = null!;

        public int QuoteId { get; set; }
        public Quote Quote { get; set; } = null!;

        public GameMode Mode { get; set; }

        public int? SuggestedAuthorId { get; set; }
        public Author? SuggestedAuthor { get; set; }

        public int? SelectedAuthorId { get; set; }
        public Author? SelectedAuthor { get; set; }

        public bool? AnswerYesNo { get; set; }

        public bool IsCorrect { get; set; }
        public DateTime AnsweredAt { get; set; } = DateTime.UtcNow;
    }
}
