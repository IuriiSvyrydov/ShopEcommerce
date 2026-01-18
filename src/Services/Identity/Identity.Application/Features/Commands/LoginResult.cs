namespace Identity.Application.Features.Commands;

public record LoginResult
{
    public string AccessToken { get; set; }

    public DateTime Expiration { get; set; }

    public LoginResult(string accessToken, DateTime expiration)
    {
        AccessToken = accessToken;
       
        Expiration = expiration;
        
    }
}
