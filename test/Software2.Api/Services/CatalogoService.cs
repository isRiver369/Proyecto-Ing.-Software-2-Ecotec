using Software2.Api.Data;
using Software2.Api.Models;

namespace Software2.Api.Services
{
    public class CatalogoService : ICatalogoService
    {
        public List<Curso> ObtenerTodos()
        {
            // Retorna la lista estática. Esto simula una consulta a la BD.
            return InMemoryData.CursosDisponibles;
        }

        public Curso? ObtenerPorId(int id)
        {
            // Usa LINQ para buscar un curso por su ID.
            return InMemoryData.CursosDisponibles.FirstOrDefault(c => c.Id == id);
        }
    }
}