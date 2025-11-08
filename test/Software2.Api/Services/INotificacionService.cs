using Software2.Api.Models;

namespace Software2.Api.Services
{
    public interface INotificacionService
    {
        void EnviarConfirmacion(Reserva reserva);
    }
}
