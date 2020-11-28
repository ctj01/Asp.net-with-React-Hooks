using Application.Contratos;
using Application.ManejadorErr;
using Dominio.Entidades;
using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Persistencia;
using System;
using System.Collections.Generic;
using System.Net;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Seguridad
{
    public class Login
    {
        public class Ejecutar : IRequest<UsuarioData>
        {
            public string Email { get; set; }
            public string Password { get; set; }
        }
        public class LoginValidation : AbstractValidator<Ejecutar>
        {
            public LoginValidation()
            {
                RuleFor(x => x.Email).NotEmpty();
                RuleFor(x => x.Password).NotEmpty();
            }
        }
        public class Manejador : IRequestHandler<Ejecutar, UsuarioData>
        {
            private readonly UserManager<Usuario> userManager;
            private readonly SignInManager<Usuario> signInManager;
            private readonly IJwtGenerator Generador;
            public Manejador(UserManager<Usuario> userManager, SignInManager<Usuario> signInManager, IJwtGenerator ijwtgenerator)
            {
                this.userManager = userManager;
                this.signInManager = signInManager;
                this.Generador = ijwtgenerator;
            }
            public async Task<UsuarioData> Handle(Ejecutar request, CancellationToken cancellationToken)
            {
                var usuario = await userManager.FindByEmailAsync(request.Email);
                if (usuario == null)
                {
                    throw new ErrorHandler(HttpStatusCode.Unauthorized, "el Email no esta registrado");
                }

                var result = await signInManager.CheckPasswordSignInAsync(usuario, request.Password, false);

                var listRoles =  await userManager.GetRolesAsync(usuario);

                if (result.Succeeded)
                {
                    return (new UsuarioData { 
                    NombreCompleto = usuario.NombreCompleto,
                    Email = usuario.Email,
                    UserName = usuario.UserName,
                    Token = Generador.CrearToken(usuario, new List<string>(listRoles)),
                    Imagen = null  
                    });
                }
                throw new ErrorHandler(HttpStatusCode.Unauthorized, "password incorrecto");
            }
        }
    }
}
