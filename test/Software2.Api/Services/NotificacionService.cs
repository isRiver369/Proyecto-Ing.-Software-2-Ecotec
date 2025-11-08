using Software2.Api.Models;
using System.Diagnostics; // Usamos Debug para la salida de la consola/log

namespace Software2.Api.Services
{
    public class NotificacionService : INotificacionService
    {
        public void EnviarConfirmacion(Reserva reserva)
        {
            // Simulación simple de envío de Email/SMS
            Debug.WriteLine($"[NOTIFICACIÓN SIMULADA] Reserva #{reserva.Id} CONFIRMADA.");
            Debug.WriteLine($"  Detalles: {reserva.NombreCliente} ({reserva.EmailCliente}) reservó el Curso ID {reserva.CursoId} para el {reserva.FechaHoraReserva}.");
        }
    }
}