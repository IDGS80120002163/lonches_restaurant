using Lonches_Restaurant.Dtos;
using Lonches_Restaurant.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Lonches_Restaurant.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HostController : Controller
    {
        //Creando la variable de context de BD
        private readonly LonchesRestaurantContext _baseDatos;

        public HostController(LonchesRestaurantContext baseDatos)
        {
            _baseDatos = baseDatos;
        }

        [HttpGet]
        [Route("ListaMesas")]
        public async Task<IActionResult> ListaMesas()
        {
            var mesas = await _baseDatos.Mesas
                .Select(m => new
                {
                    m.IdMesa,
                    m.NumMesa,
                    Num_Estatus = m.Estatus,
                    Estatus = ObtenerEstatus(m.Estatus)
                })
                .ToListAsync();

            return Ok(mesas);
        }

        [HttpPost]
        [Route("AsignarMesa/{idMesa:int}/{estatus:int}")]
        public async Task<IActionResult> AsignarMesa([FromBody] ClienteDTO request, int idMesa, int estatus)
        {
            var cliente = new Cliente
            {
                Nombre = request.Nombre,
                IdMesa = idMesa
            };

            await _baseDatos.Clientes.AddAsync(cliente);
            await _baseDatos.SaveChangesAsync();

            var mesaAsignar = await _baseDatos.Mesas.FindAsync(idMesa);
            if (mesaAsignar == null)
            {
                return BadRequest("No existe la mesa");
            }

            mesaAsignar.Estatus = estatus; //Cambia el estatus al que presione el host
            await _baseDatos.SaveChangesAsync();

            return Ok(new { message = "La mesa fue asignada correctamente" });

        }

        [HttpPut]
        [Route("ModificarMesa/{idMesa:int}/{estatus:int}")]
        public async Task<IActionResult> ModificarMesa(int idMesa, int estatus)
        {

            var mesaAsignar = await _baseDatos.Mesas.FindAsync(idMesa);
            if (mesaAsignar == null)
            {
                return BadRequest("No existe la mesa");
            }

            mesaAsignar.Estatus = estatus; //Cambia el estatus al que presione el host
            await _baseDatos.SaveChangesAsync();

            return Ok(new { message = "La mesa fue cambiada de rol correctamente" });

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
