using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.Contratos;
using Application.CursoRequest;
using Application.Seguridad;
using AutoMapper;
using Dominio.Entidades;
using FluentValidation.AspNetCore;
using MediatR;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Internal;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using Persistencia;
using Seguridad.TokenSeguridad;
using WebApiRest.MiddleWare;
using ISystemClock = Microsoft.AspNetCore.Authentication.ISystemClock;
using SystemClock = Microsoft.AspNetCore.Authentication.SystemClock;

namespace WebApiRest
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {

            services.AddDbContext<ContextoCurso>(options => options.UseSqlServer(Configuration.GetConnectionString("CursoConection")));
            services.AddMediatR(typeof(Consulta.Manejador).Assembly);
            services.AddControllers(opt => {
                var policy = new AuthorizationPolicyBuilder().RequireAuthenticatedUser().Build();
                opt.Filters.Add(new AuthorizeFilter(policy));
                })
                .AddFluentValidation(c => c.RegisterValidatorsFromAssemblyContaining<NuevoCurso>());
            var builder = services.AddIdentityCore<Usuario>();
            var identityBuilder = new IdentityBuilder(builder.UserType, builder.Services);
            identityBuilder.AddEntityFrameworkStores<ContextoCurso>();
            identityBuilder.AddSignInManager<SignInManager<Usuario>>();
            services.TryAddSingleton<ISystemClock, SystemClock>();
            services.AddScoped<IJwtGenerator, JwtGenerador>();
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("mipalabra esta es mi palabra secreta para autenticar"));
            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(opt => opt.TokenValidationParameters
           = new Microsoft.IdentityModel.Tokens.TokenValidationParameters
           {
               ValidateIssuerSigningKey = true,
               IssuerSigningKey = key,
               ValidateAudience = false,
               ValidateIssuer = false
           });
            services.AddScoped<IUsuarioSesion, UsuarioSesion>();
            services.AddAutoMapper(typeof(Consulta.Manejador));



        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseMiddleware<ManejadorErrorMiddleWare> ();
            if (env.IsDevelopment())
            {
               
            }

            //app.UseHttpsRedirection();
            app.UseAuthentication();
            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
