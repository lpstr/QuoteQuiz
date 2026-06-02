using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuoteQuiz.Application.DTOs
{
    public record UserDto(int Id, string Username, string Email, bool IsDisabled);
    public record CreateUserDto(string Username, string Email);
    public record UpdateUserDto(string Username, string Email, bool IsDisabled);
}
