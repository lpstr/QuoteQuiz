using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuoteQuiz.Domain.Entities
{
    public class User
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public string Email { get; set; }
        public bool IsDisabled { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        
        //public DateTime DisabledAt { get; set; } = DateTime.UtcNow;
        //public int DisabledByUserId { get; set; }
        public ICollection<GameSession> GameSessions { get; set; } = new List<GameSession>();
        public ICollection<UserRole> UserRoles { get; set; } = new List<UserRole>();
    }

}
