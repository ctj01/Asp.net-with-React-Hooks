using System;
using System.Collections.Generic;
using System.Text;

namespace Application.DTO
{
    public class InstructorDto
    {
        public Guid Instructorid { get; set; }
        public string Nombre { get; set; }
        public string Apellido { get; set; }
        public string Grado { get; set; }
        public byte[] Fotodeperfil { get; set; }
        public DateTime? FechaCreacion { get; set; }
    }
}
