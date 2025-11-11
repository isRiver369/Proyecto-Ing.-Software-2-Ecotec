namespace Software2.Api.Models
{
    public class Curso
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public string Descripcion { get; set; }
        public decimal PrecioBase { get; set; }
        public string Proveedor { get; set; }
        public string Modalidad { get; set; } // "Online" o "Presencial"
        public double RatingPromedio { get; set; } // Simulación de calificación
        public int CuposDisponibles { get; set; } // Para la lógica de Reserva
    }
}
