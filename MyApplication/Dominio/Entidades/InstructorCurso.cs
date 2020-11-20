using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Dominio.Entidades
{
   public class InstructorCurso:DTO
    {
        [Key]
        public Guid Cursoid { get; set; }
        [Key]
        public Guid Instructorid { get; set; }
        public Curso Tcurso { get; set; }
        public Instructor Tinstructor { get; set; }
    }
}
