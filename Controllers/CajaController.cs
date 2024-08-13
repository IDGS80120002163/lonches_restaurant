using Lonches_Restaurant.Dtos;
using Lonches_Restaurant.Models;
using Microsoft.AspNetCore.Mvc;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace Lonches_Restaurant.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CajaController : Controller
    {
        private readonly LonchesRestaurantContext _baseDatos;

        public CajaController(LonchesRestaurantContext baseDatos)
        {
            _baseDatos = baseDatos;
        }

        [HttpPost]
        [Route("VenderPedido/{idMesa}")]
        public async Task<IActionResult> VenderPedido([FromBody] VentaDTO request, int idMesa)
        {

            var venta = new Ventum
            {
                IdEmpleado = request.IdEmpleado,
                IdCliente = request.IdCliente,
                IdComanda = request.IdComanda,
                Fecha = DateTime.Now
            };

            await _baseDatos.Venta.AddRangeAsync(venta);
            await _baseDatos.SaveChangesAsync();

            var mesaAsignar = await _baseDatos.Mesas.FindAsync(idMesa);
            if (mesaAsignar == null)
            {
                return BadRequest("No existe la mesa");
            }

            mesaAsignar.Estatus = 5; //Cambia el estatus a 5 (Limpieza)
            await _baseDatos.SaveChangesAsync();        

            return Ok("La venta fue registrada");
        }
    }
}
