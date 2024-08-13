using Lonches_Restaurant.Dtos;
using Lonches_Restaurant.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Lonches_Restaurant.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MeseroController : Controller
    {
        private readonly LonchesRestaurantContext _baseDatos;

        public MeseroController(LonchesRestaurantContext baseDatos)
        {
            _baseDatos = baseDatos;
        }        

        [HttpPost]
        [Route("OrdenarComida")]
        public async Task<IActionResult> OrdenarComida([FromBody] ComandaDTO request, int idMesa)
        {

            var comanda = new Comandum
            {
                IdCliente = request.IdCliente,
                IdMesa = request.IdMesa,
                Estatus = 1,
                IdEmpleado = request.IdEmpleado
            };

            var mesaAsignar = await _baseDatos.Mesas.FindAsync(idMesa);
            if (mesaAsignar == null)
            {
                return BadRequest("No existe la mesa");
            }

            mesaAsignar.Estatus = 3; //Cambia el estatus a 3 (Pedido)
            await _baseDatos.SaveChangesAsync();

            await _baseDatos.Comanda.AddAsync(comanda);
            await _baseDatos.SaveChangesAsync();

            //Insertar los detalles de la compra
            var detallesComanda = request.DetallesComanda.Select(c => new ComandaDetalle
            {
                IdComanda = comanda.IdComanda,
                IdProducto = c.IdProducto,
                CantidadPedida = c.CantidadPedida,
                PrecioUnitario = c.PrecioUnitario,
                Observaciones = c.Observaciones
            }).ToList();

            await _baseDatos.ComandaDetalles.AddRangeAsync(detallesComanda);
            await _baseDatos.SaveChangesAsync();

            //Mandar a producción 
            var produccion = request.Producciones.Select(p => new Produccion
            {
                IdComanda = comanda.IdComanda,
                IdEmpleado = p.IdEmpleado,
                Fecha = DateTime.Now
            });

            await _baseDatos.Produccions.AddRangeAsync(produccion);
            await _baseDatos.SaveChangesAsync();

            return Ok("El pedido fue solicitado");
        }

        [HttpGet]
        [Route("ListaProductos")]
        public async Task<ActionResult<IEnumerable<object>>> ListaProductos()
        {
            var productos = await _baseDatos.Productos
                .Where(p => p.Estatus == 1)
                .Select(p => new
                {
                    p.IdProducto,
                    p.Nombre,
                    p.Precio,
                    Rol = ObtenerEstatusProducto(p.Estatus)
                })
                .ToListAsync();

            return Ok(productos);
        }


        [HttpGet]
        [Route("ObtenerCliente/{idMesa:int}/{idEmpleado:int}")]
        public async Task<ActionResult<IEnumerable<object>>> ObtenerCliente(int idMesa, int idEmpleado)
        {
            var cliente = await _baseDatos.Clientes
                .Join(_baseDatos.Mesas,
                      c => c.IdMesa,
                      m => m.IdMesa,
                      (c, m) => new { Cliente = c, Mesa = m })
                .Join(_baseDatos.EmpleadoMesaIntermedia,
                      cm => cm.Mesa.IdMesa,
                      em => em.IdMesa,
                      (cm, em) => new { cm.Cliente, cm.Mesa, EmpleadoMesaIntermedia = em })
                .Where(cme => cme.Cliente.IdMesa == idMesa && cme.Mesa.Estatus == 2 && cme.EmpleadoMesaIntermedia.IdEmpleado == idEmpleado)
                .Select(cme => cme.Cliente)
                .ToListAsync();

            return Ok(cliente);
        }

        //Método que facilita filtrar los estatus de las mesas
        public static string ObtenerEstatusProducto(int estatus)
        {
            switch (estatus)
            {
                case 1: return "Activo";
                case 2: return "Inactivo";
                default: return "Rol no definido";
            }
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
    }
}
