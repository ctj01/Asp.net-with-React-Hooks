using Dominio.Entidades;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Contratos
{
    public interface IJwtGenerator
    {
        string CrearToken(Usuario usuario, List<string> user);
    }
}
