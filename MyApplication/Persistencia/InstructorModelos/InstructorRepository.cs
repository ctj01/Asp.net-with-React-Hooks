using Dapper;
using Persistencia.Dapper;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Persistencia.InstructorModelos
{
    public class InstructorRepository : IInstructor<InstructoModel>
    {
        IFactoryConection conexion;
        public InstructorRepository(IFactoryConection factoryConection)
        {
            this.conexion = factoryConection;
        }
        public async Task<int> Actualizar(InstructoModel Parametros)
        {
            var storeProcedure = "ActualizaInstructor";
            try
            {
                var connection = conexion.GetConnection();
                var result = await connection.ExecuteAsync(
                    storeProcedure,
                    new { 
                    InstructorId = Parametros.Instructorid,
                    Parametros.Nombre,
                    Parametros.Apellido,
                    Parametros.Grado
                    },
                    commandType: System.Data.CommandType.StoredProcedure               
                    );
                 conexion.closeConection();
                return result;
            }
            catch (Exception ex)
            {

                throw new Exception(" no se puedo actualizar el registro {0}", ex);
            }
        }

        public async Task<int> Eliminar(Guid Instructorid)
        {
            var TransctSql = "DELETE FROM INSTRUCTOR WHERE Instructorid =  @Instructorid DELETE FROM INSTRUCTORCURSO WHERE Instructorid =  @Instructorid";
            try
            {
                var conection = conexion.GetConnection();
                var result =  await conection.ExecuteAsync(TransctSql, new { InstructorId = Instructorid}, commandType: System.Data.CommandType.Text);
                conexion.closeConection();
                return result;
            }
            catch (Exception ex)
            {

                throw new Exception("No se pudo eliminar el registro {0}" + ex);
            }
        }

        public async Task<IEnumerable<InstructoModel>> Getall()
        {
            IEnumerable<InstructoModel> ListaInstructor = null;
            var procedure = "List_all";
            try
            {
                var conectionz = conexion.GetConnection();
                ListaInstructor = await conectionz.QueryAsync<InstructoModel>(procedure, null, commandType: System.Data.CommandType.StoredProcedure);
                
            }
            catch (Exception ex)
            {

                throw new Exception(" error en la consulta de datos {0}", ex);
            }
            finally
            {
                conexion.closeConection();
            }
            return ListaInstructor;
        }

        public async Task<int> NuevoInstructor(InstructoModel paramteros)
        {
            var storeProcedure = "Nuevo_Instructor";
            try
            {
                var conection = conexion.GetConnection();

                var resultado = await conection.ExecuteAsync(storeProcedure,
                    new
                    {
                        Instructorid = Guid.NewGuid(),
                        Nombre = paramteros.Nombre,
                        Apellido = paramteros.Apellido,
                        Grado = paramteros.Grado
                    },
                    commandType: System.Data.CommandType.StoredProcedure);
                conexion.closeConection();
                return (resultado);
            }
            catch (Exception ex)
            {

                throw new Exception("no se pudo guardar el nuevo instructor {0}", ex);
            }

        }

        public Task<InstructoModel> ObtenerPorId()
        {
            throw new NotImplementedException();
        }
    }
}
