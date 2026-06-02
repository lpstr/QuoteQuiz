using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuoteQuiz.Application.DTOs
{
    public record QuoteDto(int Id, string Text, int AuthorId, string AuthorName);
    public record CreateQuoteDto(string Text, int AuthorId);
    public record UpdateQuoteDto(string Text, int AuthorId);
}
