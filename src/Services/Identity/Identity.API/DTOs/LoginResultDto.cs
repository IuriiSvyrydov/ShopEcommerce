namespace Identity.API.DTOs
{
    public record LoginResultDto(string JwtToken,
        string RefreshToken);
    
}
