using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Dominio.Entidades
{
    public class Instructor:DTO
    {
        [Key]
        public Guid Instructorid { get; set; }
        public string Nombre { get; set; }
        public string Apellido { get; set; }
        public string Grado { get; set; }
        public byte[] Fotodeperfil { get; set; }
        public ICollection<InstructorCurso> TinstructorCursos { get; set; }
    }
}
