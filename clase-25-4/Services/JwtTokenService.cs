using System;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using clase_25_4.Models;
using clase_25_4.Models.Auth;

namespace clase_25_4.Services
{
	public class JwtTokenService: ITokenService
	{
        const int EXPIRATION_MINUTES = 5;

        private readonly IConfiguration _configuration;

        public JwtTokenService(IConfiguration configuration)
        {
            _configuration = configuration ?? throw new Exception("Missing config");
        }

        public AuthenticationResponse CreateToken(User user)
        {
            DateTime expiration = DateTime.UtcNow.AddMinutes(EXPIRATION_MINUTES);
            
            JwtSecurityToken token = CreateJwtToken(
                CreateClaims(user),
                CreateSigningCredentials(),
                expiration
            );

            JwtSecurityTokenHandler tokenHandler = new JwtSecurityTokenHandler();
            
            return new AuthenticationResponse
            {
                Token = tokenHandler.WriteToken(token),
                Expiration = expiration.ToString()
            };
        }

        private JwtSecurityToken CreateJwtToken(Claim[] claims, SigningCredentials credentials, DateTime expiration) =>
            new JwtSecurityToken(
                _configuration["JwtSettings:Issuer"],
                _configuration["JwtSettings:Audience"],
                claims,
                expires: expiration,
                signingCredentials: credentials
            );

        private Claim[] CreateClaims(User user) =>
            new[] {
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(JwtRegisteredClaimNames.Iat, DateTime.UtcNow.ToString()),
                new Claim(ClaimTypes.NameIdentifier, user.Id),
                new Claim(ClaimTypes.Name, user.Email),
                new Claim(ClaimTypes.Email, user.Email)
            };

        private SigningCredentials CreateSigningCredentials() =>
            new SigningCredentials(
                new SymmetricSecurityKey(
                    Encoding.UTF8.GetBytes(_configuration["JwtSettings:Key"] ?? throw new Exception("Missing Jwt key in config")) 
                ),
                SecurityAlgorithms.HmacSha256
            );
    }
}

