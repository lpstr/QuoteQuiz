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
    public class UserService : IUserService
    {
        private readonly IUserRepository _users;

        public UserService(IUserRepository users)
        {
            _users = users;
        }

        public async Task<List<UserDto>> GetAllAsync(CancellationToken ct = default)
        {
            var list = await _users.GetAllAsync(ct);
            return list.Select(u => new UserDto(u.Id, u.Username, u.Email, u.IsDisabled)).ToList();
        }

        public async Task<UserDto?> GetByIdAsync(int id, CancellationToken ct = default)
        {
            var user = await _users.GetByIdAsync(id, ct);
            return user is null ? null : new UserDto(user.Id, user.Username, user.Email, user.IsDisabled);
        }

        public async Task<UserDto> CreateAsync(CreateUserDto dto, CancellationToken ct = default)
        {
            var user = new User
            {
                Username = dto.Username,
                Email = dto.Email,
                IsDisabled = false
            };

            await _users.AddAsync(user, ct);
            return new UserDto(user.Id, user.Username, user.Email, user.IsDisabled);
        }

        public async Task<UserDto?> UpdateAsync(int id, UpdateUserDto dto, CancellationToken ct = default)
        {
            var user = await _users.GetByIdAsync(id, ct);
            if (user is null) return null;

            user.Username = dto.Username;
            user.Email = dto.Email;
            user.IsDisabled = dto.IsDisabled;

            await _users.UpdateAsync(user, ct);
            return new UserDto(user.Id, user.Username, user.Email, user.IsDisabled);
        }

        public async Task<bool> DeleteAsync(int id, CancellationToken ct = default)
        {
            var user = await _users.GetByIdAsync(id, ct);
            if (user is null) return false;

            await _users.DeleteAsync(user, ct);
            return true;
        }
    }
}
