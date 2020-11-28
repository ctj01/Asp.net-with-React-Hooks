using Application.Contratos;
using Dominio.Entidades;
using MediatR;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Seguridad
{
    public class UsuarioActual
    {
        public class Ejecutar : IRequest<UsuarioData> { }
        public class Manejador : IRequestHandler<Ejecutar, UsuarioData>
        {
            UserManager<Usuario> user;
            IJwtGenerator jwtGenerator;
            IUsuarioSesion UsuarioSesion;
            public Manejador(UserManager<Usuario> usermanager, IJwtGenerator ijwtgenerator, IUsuarioSesion iusuariosesion)
            {
                this.user = usermanager;
                this.jwtGenerator = ijwtgenerator;
                this.UsuarioSesion = iusuariosesion;
            }
            public async Task<UsuarioData> Handle(Ejecutar request, CancellationToken cancellationToken)
            {
                var usuario = await user.FindByNameAsync(UsuarioSesion.ObtenerUsuarioSesion());
                var listRoles = await user.GetRolesAsync(usuario);
                return new UsuarioData
                {
                    UserName = usuario.UserName,
                    Email = usuario.Email,
                    NombreCompleto = usuario.NombreCompleto,
                    Token = jwtGenerator.CrearToken(usuario, new List<string>(listRoles)),
                    Imagen = null
                };
            }
        }
    }
}
