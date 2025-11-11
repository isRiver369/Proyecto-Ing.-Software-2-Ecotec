using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Software2.Api.Models;
using Software2.Api.Services;

namespace Software2.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReservasController : ControllerBase
    {
        private readonly IReservaService _reservaService;

        // Inyección de Dependencias (DIP)
        public ReservasController(IReservaService reservaService)
        {
            _reservaService = reservaService;
        }

        // POST: api/Reservas
        // Endpoint para crear una nueva reserva (servicio mínimo funcional)
        [HttpPost]
        public ActionResult<Reserva> Post([FromBody] CrearReservaDto reservaDto)
        {
            // La lógica de negocio (validar cupos, actualizar datos) está en ReservaService.
            var nuevaReserva = _reservaService.CrearReserva(reservaDto);

            if (nuevaReserva == null)
            {
                // Código 400 Bad Request si la reserva falla (por ejemplo: sin cupos, ID de curso inválido).
                return BadRequest(new { message = "No se pudo crear la reserva. Verifique los datos o la disponibilidad del curso." });
            }

            // Código 201 Created si es exitoso.
            return CreatedAtAction(nameof(Post), new { id = nuevaReserva.Id }, nuevaReserva);
        }
    }
}