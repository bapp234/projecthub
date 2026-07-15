using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectHub.Persistence.Authentication
{
    public sealed class JwtOptions
    {
        public const string SectionName = "Jwt";
        public string SecretKey { get; init; } = string.Empty;
        public string Issuer { get; init; } = string.Empty;
        public string Audience { get; init; } = string.Empty;
        public int AccessTokenExpirationInMinutes { get; init; }
    }
}
