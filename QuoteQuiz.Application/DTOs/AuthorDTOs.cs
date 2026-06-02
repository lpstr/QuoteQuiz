using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuoteQuiz.Application.DTOs
{
    public record AuthorDto(int Id, string Name);
    public record CreateAuthorDto(string Name);
}
