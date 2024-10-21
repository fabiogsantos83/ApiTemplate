namespace ApiTemplate.Application.Models
{
    public record class JwtOptions
       (
           string Issuer,
           string Audience,
           string SigningKey,
           int ExpirationSeconds
       );
}
