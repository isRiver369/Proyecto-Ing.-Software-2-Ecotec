using Software2.Api.Models;

namespace Software2.Api.Services
{
    public interface IReservaService
    {
        // Devuelve la reserva creada o null si falla (ejemplo: sin cupos)
        Reserva? CrearReserva(CrearReservaDto reservaDto);
    }
}