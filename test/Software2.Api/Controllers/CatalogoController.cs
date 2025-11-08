using Microsoft.AspNetCore.Mvc;
using Software2.Api.Models;
using Software2.Api.Services;

namespace Software2.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CatalogoController : ControllerBase
    {
        private readonly ICatalogoService _catalogoService;

        // Inyección de Dependencias (DIP): El Controller depende de la interfaz.
        public CatalogoController(ICatalogoService catalogoService)
        {
            _catalogoService = catalogoService;
        }

        // GET: api/Catalogo (Para listar todos los cursos)
        [HttpGet]
        public ActionResult<IEnumerable<Curso>> Get()
        {
            var cursos = _catalogoService.ObtenerTodos();
            return Ok(cursos);
        }

        // GET api/Catalogo/5 (Para obtener los detalles de un curso)
        [HttpGet("{id}")]
        public ActionResult<Curso> Get(int id)
        {
            var curso = _catalogoService.ObtenerPorId(id);

            if (curso == null)
            {
                return NotFound(); // Código 404 si no se encuentra
            }

            return Ok(curso);
        }
    }
}