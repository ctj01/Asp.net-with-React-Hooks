using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Dominio.Entidades
{
    public class Comentario:DTO
    {
        [Key]
        public Guid Comentarioid { get; set; }
        public string Alumno { get; set; }
        public int Puntaje { get; set; }
        public string ComentarioTexto { get; set; }
        public Guid Cursoid { get; set; }
        public Curso Tcurso { get; set; }
    }
}
