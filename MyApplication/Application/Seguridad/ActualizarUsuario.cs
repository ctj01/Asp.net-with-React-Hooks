using Application.Contratos;
using Dominio.Entidades;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Persistencia;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Seguridad
{
    public class ActualizarUsuario
    {
        public class Ejecuta : IRequest<UsuarioData>
        {
            public string UserName { get; set; }
            public string Email { get; set; }
            public string Apellido { get; set; }
            public string Nombre { get; set; }
            public string Password { get; set; }
        }
        public class Manejador : IRequestHandler<Ejecuta, UsuarioData>
        {
            private readonly UserManager<Usuario> user;
            private readonly ContextoCurso contextoCurso;
            private readonly IJwtGenerator generator;
            private readonly IPasswordHasher<Usuario> hasher;
            public Manejador(UserManager<Usuario> user, ContextoCurso contextoCurso, IJwtGenerator generator, IPasswordHasher<Usuario> hasher)
            {
                this.generator = generator;
                this.user = user;
                this.contextoCurso = contextoCurso;
                this.hasher = hasher;
            }
            public async Task<UsuarioData> Handle(Ejecuta request, CancellationToken cancellationToken)
            {
                var userid =  await user.FindByNameAsync(request.UserName);

                if (userid == null)
                {
                    throw new ManejadorErr.ErrorHandler(System.Net.HttpStatusCode.NotFound, new {Mensaje = "No existe este usuario"});

                }

               var result = await  contextoCurso.Users.Where(z => z.Email == request.Email && z.UserName != request.UserName).AnyAsync();

                if (result)
                {
                    throw new ManejadorErr.ErrorHandler(System.Net.HttpStatusCode.BadRequest, new { mensaje = "email ya pertenece a otro usuario"});

                }
                userid.NombreCompleto = request.Nombre + " " + request.Apellido;
                userid.PasswordHash = hasher.HashPassword(userid, request.Password);
                userid.Email = request.Email;
                var resultupdate = await user.UpdateAsync(userid);
                var listaroles = await user.GetRolesAsync(userid);
               

                if (resultupdate.Succeeded)
                {
                    return new UsuarioData
                    {
                        NombreCompleto = userid.NombreCompleto,
                        UserName = userid.UserName,
                        Email = userid.Email,
                        Token = generator.CrearToken(userid, new List<string>(listaroles))

                    };
                }
                throw new Exception("No se pudo actualizar el usuario");
            }
        }
    }
}
