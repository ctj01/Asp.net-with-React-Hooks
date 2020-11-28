using Dominio.Entidades;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persistencia
{
    public class DataPrueba
    {
        public static async Task Insertar(ContextoCurso context, UserManager<Usuario> userManager)
        {
            if (!userManager.Users.Any())
            {
                var usuario = new Usuario { NombreCompleto = "cristian", UserName = "ctj01", Email = "cristianmt023@gmail.com" };
                await userManager.CreateAsync(usuario, "310633Cr@");

            }
        }
    }
}
