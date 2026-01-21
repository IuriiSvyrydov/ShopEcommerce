public class RefreshToken
{
    public Guid Id { get; set; }
    public Guid UserId { get; set; }

    public string Token { get; set; } = null!;

    public DateTime ExpiresOnUtc { get; set; }
    public DateTime CreatedOnUtc { get; set; }

    public DateTime? RevokedOnUtc { get; set; }
    public Guid? ReplacedByTokenId { get; set; }

    public bool IsExpired => DateTime.UtcNow >= ExpiresOnUtc;

    public bool IsRevoked => RevokedOnUtc != null;
}
