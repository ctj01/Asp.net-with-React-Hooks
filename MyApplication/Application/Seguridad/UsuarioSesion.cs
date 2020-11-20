using Application.Contratos;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;

namespace Application.Seguridad
{
    public class UsuarioSesion : IUsuarioSesion
    {
        private readonly IHttpContextAccessor contextAccessor;
        public UsuarioSesion(IHttpContextAccessor httpContextAccessor)
        {
            this.contextAccessor = httpContextAccessor;
        }
        public string ObtenerUsuarioSesion()
        {
            var UserName = contextAccessor.HttpContext.User?.Claims?.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier)?.Value;
            return UserName;
        }
    }
}
