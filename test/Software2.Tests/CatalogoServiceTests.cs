using Moq;
using Software2.Api.Models;
using Software2.Api.Services;
using Xunit;

namespace Software2.Tests
{
    public class CatalogoServiceTests
    {
        // El [Fact] indica que este método es una prueba unitaria.
        [Fact]
        public void ObtenerTodos_DebeRetornarListaDeCursosNoVacia()
        {
            // ARRANGE (Preparar): 
            // Como usamos datos en memoria y el servicio no tiene dependencias (es sencillo), 
            // podemos instanciarlo directamente.
            var servicio = new CatalogoService();

            // ACT (Actuar): 
            // Ejecutar el método que queremos probar.
            var resultado = servicio.ObtenerTodos();

            // ASSERT (Afirmar): 
            // Verificar que el resultado es el esperado.
            // 1. Que no sea nulo.
            Assert.NotNull(resultado);
            // 2. Que el conteo sea mayor que cero (que haya cursos).
            Assert.True(resultado.Count > 0);
            // 3. O, si quieres ser específico, verificar que sean 4 cursos (lo que hay en InMemoryData).
            Assert.Equal(4, resultado.Count);
        }

        [Fact]
        public void ObtenerPorId_DebeRetornarCursoCorrecto()
        {
            // ARRANGE (Preparar):
            var servicio = new CatalogoService();
            int idEsperado = 2; // El curso con ID 2 existe

            // ACT (Actuar):
            var resultado = servicio.ObtenerPorId(idEsperado);

            // ASSERT (Afirmar):
            Assert.NotNull(resultado);
            Assert.Equal(idEsperado, resultado.Id);
            Assert.Equal("Consultoría SEO para Principiantes", resultado.Nombre);
        }

        [Fact]
        public void ObtenerPorId_DebeRetornarNuloParaIdInexistente()
        {
            // ARRANGE (Preparar):
            var servicio = new CatalogoService();
            int idInexistente = 99;

            // ACT (Actuar):
            var resultado = servicio.ObtenerPorId(idInexistente);

            // ASSERT (Afirmar):
            Assert.Null(resultado);
        }
    }
    // NUEVAS PRUEBAS PARA EL SERVICIO DE RESERVAS
    public class ReservaServiceTests
    {
        [Fact]
        public void CrearReserva_DebeCrearReservaYRestarCupos()
        {
            // ARRANGE (Preparar): 
            // 1. Simular las dependencias (Mocks)
            var mockCatalogo = new Mock<ICatalogoService>();
            var mockNotificacion = new Mock<INotificacionService>();

            // 2. Definir un curso mock con cupos disponibles (ID 1, 5 cupos)
            var cursoDisponible = new Curso { Id = 1, CuposDisponibles = 5 };

            // 3. Configurar el mock: Cuando se busque ID 1, devuelve el curso disponible
            mockCatalogo.Setup(s => s.ObtenerPorId(1)).Returns(cursoDisponible);

            // 4. Instanciar el servicio a probar, inyectando los mocks
            var servicio = new ReservaService(mockCatalogo.Object, mockNotificacion.Object);
            var reservaDto = new CrearReservaDto { CursoId = 1, CantidadCupos = 3, EmailCliente = "test@test.com", NombreCliente = "Cliente Prueba", FechaHoraReserva = System.DateTime.Now.AddDays(1) };

            // ACT (Actuar): 
            var resultado = servicio.CrearReserva(reservaDto);

            // ASSERT (Afirmar): 
            Assert.NotNull(resultado);
            Assert.Equal(2, cursoDisponible.CuposDisponibles); // Verifica que los cupos se restaron (5 - 3 = 2)

            // Verifica que la notificación simulada se llamó exactamente una vez
            mockNotificacion.Verify(n => n.EnviarConfirmacion(resultado), Times.Once);
        }

        [Fact]
        public void CrearReserva_DebeFallarSiNoHayCuposSuficientes()
        {
            // ARRANGE (Preparar): 
            var mockCatalogo = new Mock<ICatalogoService>();
            var mockNotificacion = new Mock<INotificacionService>();

            // Curso con pocos cupos (ID 2, 1 cupo)
            var cursoSinCupos = new Curso { Id = 2, CuposDisponibles = 1 };
            mockCatalogo.Setup(s => s.ObtenerPorId(2)).Returns(cursoSinCupos);

            var servicio = new ReservaService(mockCatalogo.Object, mockNotificacion.Object);
            // Intentar reservar 5 cupos
            var reservaDto = new CrearReservaDto { CursoId = 2, CantidadCupos = 5 };

            // ACT (Actuar): 
            var resultado = servicio.CrearReserva(reservaDto);

            // ASSERT (Afirmar): 
            Assert.Null(resultado); // Debe retornar nulo al fallar la validación
            Assert.Equal(1, cursoSinCupos.CuposDisponibles); // Verifica que los cupos no se tocaron

            // Verifica que la notificación simulada NUNCA se llamó
            mockNotificacion.Verify(n => n.EnviarConfirmacion(It.IsAny<Reserva>()), Times.Never);
        }

        [Fact]
        public void CrearReserva_DebeFallarSiCursoNoExiste()
        {
            // ARRANGE (Preparar): 
            var mockCatalogo = new Mock<ICatalogoService>();
            var mockNotificacion = new Mock<INotificacionService>();

            // Configurar el mock para que devuelva NULL si se busca ID 99
            mockCatalogo.Setup(s => s.ObtenerPorId(99)).Returns((Curso)null);

            var servicio = new ReservaService(mockCatalogo.Object, mockNotificacion.Object);
            var reservaDto = new CrearReservaDto { CursoId = 99, CantidadCupos = 1 };

            // ACT (Actuar): 
            var resultado = servicio.CrearReserva(reservaDto);

            // ASSERT (Afirmar): 
            Assert.Null(resultado); // Debe retornar nulo
            mockNotificacion.Verify(n => n.EnviarConfirmacion(It.IsAny<Reserva>()), Times.Never);
        }
    }
}