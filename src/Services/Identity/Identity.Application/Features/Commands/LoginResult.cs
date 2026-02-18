namespace Identity.Application.Features.Commands;

public record LoginResult
{
    public string AccessToken { get; set; }

    public DateTime Expiration { get; set; }
    public string RefreshToken { get; set; }

    public LoginResult(string accessToken, DateTime expiration, string refreshToken)
    {
        AccessToken = accessToken;
       
        Expiration = expiration;
        RefreshToken = refreshToken;

    }
}
