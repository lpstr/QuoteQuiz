using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuoteQuiz.Domain.Entities
{
    public class Quote
    {
        public int Id { get; set; }
        public string Text { get; set; }

        public int AuthorId { get; set; }
        public Author Author { get; set; }
        public bool IsActive { get; set; } = true;

        public ICollection<GameQuestion> GameQuestions { get; set; } = new List<GameQuestion>();

    }
}
