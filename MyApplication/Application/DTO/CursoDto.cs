using System;
using System.Collections.Generic;
using System.Text;

namespace Application.DTO
{
    public class CursoDto
    {
        public Guid Cursoid { get; set; }
        public string Titulo { get; set; }
        public string Descripcion { get; set; }
        public DateTime Fechadepublicacion { get; set; }
        public byte[] Fotodeportada { get; set; }
        public ICollection<InstructorDto> Instructor { get; set; }
        public ICollection<ComentarioDto> Comentarios { get; set; }
        public PrecioDto Precio { get; set; }
    }
}
