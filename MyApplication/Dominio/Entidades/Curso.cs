using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Dominio.Entidades
{
   public class Curso:DTO
    {
        [Key]
        public Guid Cursoid { get; set; }
        public string Titulo { get; set; }
        public string Descripcion { get; set; }
        public DateTime Fechadepublicacion { get; set; }
        public byte[] Fotodeportada { get; set; }
        public Precio Tprecio { get; set; }
        public ICollection<Comentario> Comentarios { get; set; }
        public ICollection<InstructorCurso> TinstructorCursos { get; set; }
    }
}
