using Lonches_Restaurant.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Lonches_Restaurant.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LimpiezaController : Controller
    {
        private readonly LonchesRestaurantContext _baseDatos;

        public LimpiezaController(LonchesRestaurantContext baseDatos)
        {
            _baseDatos = baseDatos;
        }

        [HttpGet]
        [Route("ListaMesas/{id_empleado:int}")]
        public async Task<ActionResult<IEnumerable<object>>> GetMesasAsignadas(int id_empleado)
        {
            var mesas_asignadas = await _baseDatos.EmpleadoMesaIntermedia
                .Where(e => e.IdEmpleado == id_empleado)
                .Join(_baseDatos.Mesas, emi => emi.IdMesa, m => m.IdMesa, (emi, m) => new
                {
                    m.IdMesa,
                    m.NumMesa,
                    Estatus = ObtenerEstatus(m.Estatus),
                    Num_Estatus = m.Estatus
                })
                .ToListAsync();

            return Ok(mesas_asignadas);
        }

        [HttpPut]
        [Route("LimpiarMesa/{id_mesa:int}")]
        public async Task<IActionResult> LimpiarMesa(int id_mesa)
        {
            var mesaLimpiar = await _baseDatos.Mesas.FindAsync(id_mesa);
            if (mesaLimpiar == null)
            {
                return BadRequest("No existe el empleado");
            }

            mesaLimpiar.Estatus = 1; // Cambia el estatus a 0
            await _baseDatos.SaveChangesAsync();

            return Ok("La mesa está limpia y libre");
        }

        //Método que facilita filtrar los estatus de las mesas
        public static string ObtenerEstatus(int estatus)
        {
            switch (estatus)
            {
                case 1: return "Libre";
                case 2: return "Asignada";
                case 3: return "Pedido";
                case 4: return "Comiendo";
                case 5: return "Limpieza";
                default: return "Rol no definido";
            }
        }
    }
}
