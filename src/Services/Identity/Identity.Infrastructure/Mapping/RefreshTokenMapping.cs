using System;
using Identity.Domain.Entities;
using Identity.Infrastructure.Models;

namespace Identity.Infrastructure.Mapping
{
    public static class RefreshTokenMapping
    {
        public static RefreshToken ToDomain(this RefreshTokenEntity entity)
        {
            return new RefreshToken
            {
                Id = entity.Id,
                Token = entity.Token,
                UserId = entity.UserId,
                ExpiresOnUtc = entity.ExpiresOnUtc,
                CreatedOnUtc = entity.CreatedOnUtc,
                RevokedOnUtc = entity.RevokedOnUtc,
                ReplacedByTokenId = entity.ReplacedByTokenId

            };
        }

        public static RefreshTokenEntity ToEntity(this RefreshToken domain)
        {
            return new RefreshTokenEntity
            {
                Id = domain.Id,
                Token = domain.Token,
                UserId = domain.UserId,
                ExpiresOnUtc = domain.ExpiresOnUtc,
                RevokedOnUtc = domain.RevokedOnUtc,
            };
        }
    }
}
