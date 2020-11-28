using System;
using System.Collections.Generic;
using System.Text;

namespace Persistencia.InstructorModelos
{
    public class InstructoModel
    {
        public Guid Instructorid { get; set; }
        public string Nombre { get; set; }
        public string Apellido { get; set; }
        public string Grado { get; set; }
        public byte[] Fotodeperfil { get; set; }
        public DateTime? FechaCreacion { get; set; }
    }
}
