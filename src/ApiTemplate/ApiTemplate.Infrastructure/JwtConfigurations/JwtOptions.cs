namespace ApiTemplate.Infrastructure.JwtConfigurations
{
    public record class JwtOptions
    (
        string Issuer,
        string Audience,
        string SigningKey,
        int ExpirationSeconds
    );
}
