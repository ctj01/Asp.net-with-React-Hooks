using Application.Contratos;
using Application.ManejadorErr;
using Dominio.Entidades;
using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Persistencia;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Seguridad
{
    public class Registrar
    {
        public class Ejecutar : IRequest<UsuarioData>
        {
            public string UserName { get; set; }
            public string Nombre { get; set; }
            public string Apellido { get; set; }
            public string Email { get; set; }
            public string Password { get; set; }
        }
        public class RegistroValidator:AbstractValidator<Ejecutar>
        {
            public RegistroValidator()
            {
                RuleFor(x => x.Email).NotEmpty();
                RuleFor(x => x.Nombre).NotEmpty();
                RuleFor(x => x.Apellido).NotEmpty();
                RuleFor(x => x.UserName).NotEmpty();
                RuleFor(x => x.Password).NotEmpty();
            }
        }
        public class Manejador: IRequestHandler<Ejecutar , UsuarioData>
        {
            private readonly ContextoCurso Context;
            private readonly UserManager<Usuario> user;
            private readonly IJwtGenerator jwtGenerator;

            public Manejador(ContextoCurso contexto, UserManager<Usuario> userManager, IJwtGenerator jwtGenerator)
            {
                this.Context = contexto;
                this.user = userManager;
                this.jwtGenerator = jwtGenerator;
            }
            public async Task<UsuarioData> Handle(Ejecutar request, CancellationToken cancellationToken)
            {
                var existe =  await Context.Users.Where(x => x.Email == request.Email || x.UserName == request.UserName).AnyAsync();
                if (existe)
                {
                    throw new ErrorHandler(HttpStatusCode.BadRequest, new { mensaje = "ya exite un usuario con este email o username" });
                }
                var usuario = new Usuario
                {
                    Email = request.Email,
                    NombreCompleto = request.Nombre + " " + request.Apellido,
                    UserName = request.UserName
                    
                };
                var resultados = await user.CreateAsync(usuario, request.Password);
                if (resultados.Succeeded)
                {
                    return new UsuarioData
                    {
                        NombreCompleto = usuario.NombreCompleto,
                        Token = jwtGenerator.CrearToken(usuario),
                        UserName = usuario.UserName,
                        Email = usuario.Email
                    };
                }
                throw new ErrorHandler(HttpStatusCode.BadRequest, new { mensaje = "no se puedo registrar el usuario" });
                
            }
        }
    }
}
