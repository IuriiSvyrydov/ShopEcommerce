using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Identity.Infrastructure.Models;

public class RefreshTokenEntity
{
    public Guid Id { get; set; }
    public string Token { get; set; } = null!;

    public Guid UserId { get; set; }
    public ApplicationUser User { get; set; } = null!;

    public DateTime ExpiresOnUtc { get; set; }
    public DateTime CreatedOnUtc { get; set; }
    public DateTime? RevokedOnUtc { get; set; }

    public Guid? ReplacedByTokenId { get; set; }
    public RefreshTokenEntity? ReplacedByToken { get; set; }
}
