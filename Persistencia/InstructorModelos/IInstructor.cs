using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Persistencia.InstructorModelos
{
   public interface IInstructor<T> where T: class
    {
        Task<IEnumerable<T>> Getall();
        Task<T> ObtenerPorId();
        Task<int> NuevoInstructor(T paramteros);
        Task<int> Actualizar(T Parametros);
        Task<int> Eliminar(Guid id);
    }
}
