using Software2.Api.Data;
using Software2.Api.Models;

namespace Software2.Api.Services
{
    public class ReservaService : IReservaService
    {
        private readonly ICatalogoService _catalogoService;
        private readonly INotificacionService _notificacionService;

        // Inyeccion de dependencias (DIP)
        public ReservaService(ICatalogoService catalogoService, INotificacionService notificacionService)
        {
            _catalogoService = catalogoService;
            _notificacionService = notificacionService;
        }

        public Reserva? CrearReserva(CrearReservaDto reservaDto)
        {
            // 1. Validar Lógica de Negocio (Regla: No permitir más reservas que cupoMax)
            var curso = _catalogoService.ObtenerPorId(reservaDto.CursoId);

            if (curso == null || curso.CuposDisponibles < reservaDto.CantidadCupos || reservaDto.CantidadCupos <= 0)
            {
                // Falla: Curso no encontrado o no hay suficientes cupos
                return null;
            }

            // 2. Transacción Simulada: Registrar y Actualizar Cupos
            var nuevaReserva = new Reserva
            {
                Id = InMemoryData.GetNextReservaId(),
                CursoId = reservaDto.CursoId,
                NombreCliente = reservaDto.NombreCliente,
                EmailCliente = reservaDto.EmailCliente,
                FechaHoraReserva = reservaDto.FechaHoraReserva,
                CantidadCupos = reservaDto.CantidadCupos,
                Estado = "Confirmada"
            };

            InMemoryData.Reservas.Add(nuevaReserva);
            curso.CuposDisponibles -= reservaDto.CantidadCupos; // Restamos cupos

            // 3. Notificación (Simulada)
            _notificacionService.EnviarConfirmacion(nuevaReserva);

            return nuevaReserva;
        }
    }
}