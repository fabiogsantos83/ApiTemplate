using ApiTemplate.Api.Models;
using ApiTemplate.Application.Commands;
using ApiTemplate.Infrastructure.JwtConfigurations;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace ApiTemplate.Api.Controllers
{
    [ApiController]
    public class AuthorizationController : ControllerBase
    {
        private readonly JwtOptions _jwtOptions;
        public AuthorizationController(JwtOptions jwtOptions)
        {
            _jwtOptions = jwtOptions;
        }

        [HttpPost("token")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Token))]
        public async Task<IActionResult> CreateToken(UserAuthentication userAutenticator)
        {         
            if (!userAutenticator.IsValid())
            {
                throw new ValidationException(userAutenticator.ValidationResult.Errors);
            }

            //creates the access token (jwt token)
            var tokenExpiration = TimeSpan.FromSeconds(_jwtOptions.ExpirationSeconds);
            var accessToken = CreateAccessToken(
                _jwtOptions,
                userAutenticator.UserName,
                TimeSpan.FromMinutes(60),
                new[] { "read_todo", "create_todo" });

            //returns a json response with the access token
            return Ok(new Token
            (
                accessToken,
                (int)tokenExpiration.TotalSeconds,
                "bearer"
            ));
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
