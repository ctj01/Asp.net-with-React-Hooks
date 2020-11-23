using Application.DTO;
using AutoMapper;
using Dominio.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Application.Mapping
{
    public class MappingEntities:Profile
    {
        public MappingEntities()
        {
            CreateMap<Curso, CursoDto>().ForMember(x => x.Instructor, y => y.
            MapFrom( x => x.TinstructorCursos.Select(a => a.Tinstructor).ToList()))
                .ForMember(x =>  x.Comentarios, y => y.MapFrom(y => y.Comentarios))
                .ForMember(x => x.Precio, y => y.MapFrom(x => x.Tprecio));
            CreateMap<Instructor, InstructorDto>();
            CreateMap<Precio, PrecioDto>();
            CreateMap<Comentario, ComentarioDto>();
        }
    }
}
