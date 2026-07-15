using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ProjectHub.Persistence.Authentication;
using ProjectHub.Domain.ValueObjects;
using Xunit;
using System.Threading.Tasks;

namespace ProjectHub.Api.IntegrationTests.Authentication
{
    public sealed class BCryptPasswordHasherTests
    {
        [Fact]
        public void Hash_Should_ReturnPasswordHash()
        {
            var hasher = new BCryptPasswordHasher();

            var hash = hasher.Hash("123456");

            Assert.NotNull(hash);

            Assert.False(string.IsNullOrWhiteSpace(hash.Value));
        }
        [Fact]
        public void Verify_Should_ReturnTrue_ForCorrectPassword()
        {
            var hasher = new BCryptPasswordHasher();

            var hash = hasher.Hash("123456");

            var result = hasher.Verify(
                "123456",
                hash);

            Assert.True(result);
        }
        [Fact]
        public void Verify_Should_ReturnFalse_ForWrongPassword()
        {
            var hasher = new BCryptPasswordHasher();

            var hash = hasher.Hash("123456");

            var result = hasher.Verify(
                "abcdef",
                hash);

            Assert.False(result);
        }
        [Fact]
        public void Hash_Should_GenerateDifferentHashes()
        {
            var hasher = new BCryptPasswordHasher();

            var hash1 = hasher.Hash("123456");

            var hash2 = hasher.Hash("123456");

            Assert.NotEqual(
                hash1.Value,
                hash2.Value);
        }
        [Fact]
        public void Hash_Should_ReturnPasswordHashValueObject()
        {
            var hasher = new BCryptPasswordHasher();

            var hash = hasher.Hash("123456");

            Assert.IsType<PasswordHash>(hash);
        }
        [Fact]
        public void Hash_Should_Throw_WhenPasswordIsEmpty()
        {
            var hasher = new BCryptPasswordHasher();

            Assert.Throws<ArgumentException>(
                () => hasher.Hash(""));
        }
        [Fact]
        public void Hash_Should_Throw_WhenPasswordIsNull()
        {
            var hasher = new BCryptPasswordHasher();

            Assert.Throws<ArgumentNullException>(
        () => hasher.Hash(null!));
        }
    }
}
