using System;
using System.Collections.Generic;
using System.Text;

namespace Application.DTO
{
    public class ComentarioDto
    {
        public Guid Comentarioid { get; set; }
        public string Alumno { get; set; }
        public int Puntaje { get; set; }
        public string ComentarioTexto { get; set; }
    }
}
