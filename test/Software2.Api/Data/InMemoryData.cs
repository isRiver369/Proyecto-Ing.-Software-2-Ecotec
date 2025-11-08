using Software2.Api.Models;

namespace Software2.Api.Data
{
    public static class InMemoryData
    {
        public static List<Curso> CursosDisponibles { get; } = new List<Curso>
        {
            new Curso { Id = 1, Nombre = "Clase de Piano Avanzado", Descripcion = "Taller intensivo para músicos intermedios.", PrecioBase = 50.00m, Proveedor = "Maria Lopez", Modalidad = "Presencial", RatingPromedio = 4.8, CuposDisponibles = 5 },
            new Curso { Id = 2, Nombre = "Consultoría SEO para Principiantes", Descripcion = "Aprende a optimizar tu sitio web.", PrecioBase = 99.99m, Proveedor = "Juan Perez", Modalidad = "Online", RatingPromedio = 4.2, CuposDisponibles = 10 },
            new Curso { Id = 3, Nombre = "Masaje Relajante Express", Descripcion = "Sesión de 30 minutos anti-estrés.", PrecioBase = 35.00m, Proveedor = "Mario Galindez", Modalidad = "Presencial", RatingPromedio = 5.0, CuposDisponibles = 1 },
            new Curso { Id = 4, Nombre = "Clase de Pintura ", Descripcion = "Curso completo de técnicas de pintura.", PrecioBase = 80.00m, Proveedor = "Ana Martínez", Modalidad = "Online", RatingPromedio = 4.6, CuposDisponibles = 7 }
        };

        // Lista para almacenar las reservas
        public static List<Reserva> Reservas { get; } = new List<Reserva>();

        // Contador simple para simular IDs de autoincremento
        private static int _nextReservaId = 1001;

        public static int GetNextReservaId()
        {
            return _nextReservaId++;
        }
    } 
}