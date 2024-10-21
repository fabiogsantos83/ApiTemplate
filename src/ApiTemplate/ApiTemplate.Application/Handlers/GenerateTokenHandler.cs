using ApiTemplate.Application.Commands;
using ApiTemplate.Application.Models;
using FluentValidation;
using MediatR;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace ApiTemplate.Application.Handlers
{
    public class GenerateTokenHandler : IRequestHandler<GenerateTokenCommand, GenerateTokenCommandResponse>
    {
        private readonly JwtOptions _jwtOptions;

        public GenerateTokenHandler(JwtOptions jwtOptions)
        {
            _jwtOptions = jwtOptions;
        }
        public async Task<GenerateTokenCommandResponse> Handle(GenerateTokenCommand request, CancellationToken cancellationToken)
        {
            if (!request.IsValid())
            {
                throw new ValidationException(request.ValidationResult.Errors);
            }

            //creates the access token (jwt token)
            var tokenExpiration = TimeSpan.FromSeconds(_jwtOptions.ExpirationSeconds);
            var accessToken = CreateAccessToken(
                _jwtOptions,
                request.UserName,
                TimeSpan.FromMinutes(60),
                new[] { "read_todo", "create_todo" });

            //returns a json response with the access token
            return new GenerateTokenCommandResponse
            (
                accessToken,
                (int)tokenExpiration.TotalSeconds,
                "bearer"
            );
        }

        private string CreateAccessToken(
           JwtOptions jwtOptions,
           string username,
           TimeSpan expiration,
           string[] permissions)
        {
            var keyBytes = Encoding.UTF8.GetBytes(jwtOptions.SigningKey);
            var symmetricKey = new SymmetricSecurityKey(keyBytes);

            var signingCredentials = new SigningCredentials(
                symmetricKey,
                // 👇 one of the most popular. 
                SecurityAlgorithms.HmacSha256);

            var claims = new List<Claim>()
            {
                new Claim("sub", username),
                new Claim("name", username),
                new Claim("aud", jwtOptions.Audience)
            };

            var roleClaims = permissions.Select(x => new Claim("role", x));
            claims.AddRange(roleClaims);

            var token = new JwtSecurityToken(
                issuer: jwtOptions.Issuer,
                audience: jwtOptions.Audience,
                claims: claims,
                expires: DateTime.Now.Add(expiration),
                signingCredentials: signingCredentials);

            var rawToken = new JwtSecurityTokenHandler().WriteToken(token);
            return rawToken;
        }
    }
}
