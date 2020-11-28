using Application.Contratos;
using Dominio.Entidades;
using Microsoft.IdentityModel.JsonWebTokens;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using JwtRegisteredClaimNames = Microsoft.IdentityModel.JsonWebTokens.JwtRegisteredClaimNames;

namespace Seguridad.TokenSeguridad
{
    public class JwtGenerador : IJwtGenerator
    {
        public string CrearToken(Usuario usuario, List<string> user)
        {
            var claim = new List<Claim>
            {
                new Claim(JwtRegisteredClaimNames.NameId, usuario.UserName)
            };
            if (user != null)
            {
                foreach (var rol in user)
                {
                    claim.Add(new Claim(ClaimTypes.Role, rol));
                }
            }
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("mipalabra esta es mi palabra secreta para autenticar"));
            var credenciales = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var TokenDescription = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claim),
                Expires = DateTime.Now.AddDays(30),
                SigningCredentials = credenciales

            };
            var tokenHandler = new JwtSecurityTokenHandler();
            var token = tokenHandler.CreateToken(TokenDescription);
            return tokenHandler.WriteToken(token);
        }
    }
}
