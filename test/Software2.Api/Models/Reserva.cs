namespace Software2.Api.Models
{
    public class Reserva
    {
        public int Id { get; set; }
        public int CursoId { get; set; }
        public string NombreCliente { get; set; }
        public string EmailCliente { get; set; } // Necesario para la Notificación (simulada)
        public DateTime FechaHoraReserva { get; set; }
        public int CantidadCupos { get; set; }
        public string Estado { get; set; } = "Pendiente"; // Estados: Pendiente, Confirmada, Cancelada
    }

    // Modelo sencillo para recibir datos del cliente al hacer un POST
    public class CrearReservaDto
    {
        public int CursoId { get; set; }
        public string NombreCliente { get; set; }
        public string EmailCliente { get; set; }
        public DateTime FechaHoraReserva { get; set; }
        public int CantidadCupos { get; set; }
    }
}