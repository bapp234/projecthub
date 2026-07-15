using ProjectHub.Domain.Interfaces;
using ProjectHub.Domain.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectHub.Persistence.Authentication
{
    public sealed class BCryptPasswordHasher : IPasswordHasher
    {
        private const int WorkFactor = 12;
        public PasswordHash Hash(string password)
        {
            ArgumentException.ThrowIfNullOrWhiteSpace(password);

            var hash=BCrypt.Net.BCrypt.HashPassword(password, workFactor:WorkFactor);

            return PasswordHash.Create(hash);
        }

        public bool Verify(string password, PasswordHash passwordHash)
        {
            ArgumentException.ThrowIfNullOrWhiteSpace(password);
            ArgumentNullException.ThrowIfNull(passwordHash);
            return BCrypt.Net.BCrypt.Verify(password, passwordHash.Value);
        }
    }
}
