using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using NexusGym.Domain.Abstractions.Usuarios;
using NexusGym.Domain.Entities;

namespace NexusGym.Application.Services.Security
{
    public class GerarTokenJWT : IToken
    {
        private readonly JwtSettings _settings;


        public GerarTokenJWT(IOptions<JwtSettings> options)
        {
            _settings = options.Value;
        }

        public string GerarToken(Usuario usuario)
        {
            var chaveSimetrica = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_settings.Key));
            var credenciais = new SigningCredentials(chaveSimetrica, SecurityAlgorithms.HmacSha256);

            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, usuario.Email),
                new Claim("nome", usuario.Nome),
                new Claim(ClaimTypes.Role, usuario.Role.ToString()),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
            };

            var token = new JwtSecurityToken(
                issuer: _settings.Issuer,
                audience: _settings.Audience,
                claims: claims,
                expires: DateTime.UtcNow.AddHours(_settings.ExpiracaoHoras),
                signingCredentials: credenciais
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
    public class JwtSettings
    {
        public string Key { get; set; } = string.Empty;
        public string Issuer { get; set; } = string.Empty;
        public string Audience { get; set; } = string.Empty;
        public int ExpiracaoHoras { get; set; } = 8;
    }
}
