using Software2.Api.Models;

namespace Software2.Api.Services
{
    public interface ICatalogoService
    {
        // Contrato para listar todos los cursos
        List<Curso> ObtenerTodos();

        // Contrato para obtener un curso por su ID
        Curso? ObtenerPorId(int id);
    }
}