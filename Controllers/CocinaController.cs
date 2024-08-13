using Lonches_Restaurant.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Lonches_Restaurant.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CocinaController : Controller
    {
        //Creando la variable de context de BD
        private readonly LonchesRestaurantContext _baseDatos;

        public CocinaController(LonchesRestaurantContext baseDatos)
        {
            _baseDatos = baseDatos;
        }

        [HttpGet]
        [Route("ListaPedidos")]
        public async Task<IActionResult> ListaPedidos()
        {
            var pedidos = await _baseDatos.Comanda
                .Include(c => c.IdMesaNavigation)
                .Include(c => c.IdClienteNavigation)
                .Select(c => new
                {
                    id_mesa = c.IdMesa,
                    nombre_cliente = c.IdClienteNavigation.Nombre,
                    id_cliente = c.IdCliente,
                    num_mesa = c.IdMesaNavigation.NumMesa,
                    id_comanda = c.IdComanda,
                    num_estatus = c.Estatus,
                    estatus = ObtenerEstatus(c.Estatus)
                })
                .ToListAsync();

            return Ok(pedidos);
        }

        [HttpGet]
        [Route("ListaPedidosDetallada/{id_comanda:int}")]
        public async Task<IActionResult> ListaPedidosDetallada(int id_comanda)
        {
            var pedidos = await _baseDatos.Comanda
                .Where(c => c.IdComanda == id_comanda)
                .Include(c => c.IdMesaNavigation)
                .Include(c => c.ComandaDetalles)
                    .ThenInclude(cd => cd.IdProductoNavigation)
                .SelectMany(c => c.ComandaDetalles.Select(cd => new
                {
                    id_comanda = c.IdComanda,
                    num_mesa = c.IdMesaNavigation.NumMesa,
                    id_producto = cd.IdProductoNavigation.IdProducto,
                    nombre_producto = cd.IdProductoNavigation.Nombre,
                    cantidad_pedida = cd.CantidadPedida,
                    observaciones = cd.Observaciones,
                    precio_unitario = cd.PrecioUnitario
                }))
                .ToListAsync();

            return Ok(pedidos);
        }

        //Método que facilita filtrar los estatus de las mesas
        public static string ObtenerEstatus(int estatus)
        {
            switch (estatus)
            {
                case 1: return "Pedido";
                case 2: return "Preparando";
                case 3: return "Terminado";
                default: return "Rol no definido";
            }
        }

        [HttpPut]
        [Route("CocinarPedido/{id_comanda:int}/{id_empleado:int}")]
        public async Task<IActionResult> CocinarPedido(int id_comanda, int id_empleado)
        {
            var comandaPedida = await _baseDatos.Comanda.FindAsync(id_comanda);
            if (comandaPedida == null)
            {
                return BadRequest("No existe el empleado");
            }

            comandaPedida.Estatus = 2; //Cambia el estatus a 2 (Cocinando)
            await _baseDatos.SaveChangesAsync();

            var produccion = await _baseDatos.Produccions.FindAsync(id_comanda);
            if (produccion == null)
            {
                return BadRequest("No existe el empleado");
            }

            produccion.IdEmpleado = id_empleado;
            await _baseDatos.SaveChangesAsync();

            return Ok("El pedido se está preparando");
        }

        [HttpPut]
        [Route("EntregarPedido/{id_comanda:int}/{idMesa}")]
        public async Task<IActionResult> EntregarPedido(int id_comanda, int idMesa)
        {
            var comandaPedida = await _baseDatos.Comanda.FindAsync(id_comanda);
            if (comandaPedida == null)
            {
                return BadRequest("No existe el empleado");
            }

            comandaPedida.Estatus = 3; //Cambia el estatus a 3 (Terminado)
            await _baseDatos.SaveChangesAsync();

            var mesaAsignar = await _baseDatos.Mesas.FindAsync(idMesa);
            if (mesaAsignar == null)
            {
                return BadRequest("No existe la mesa");
            }

            mesaAsignar.Estatus = 5; //Cambia el estatus a 5 (Comiendo)
            await _baseDatos.SaveChangesAsync();

            return Ok("El pedido ha sido preparado");
        }
    }
}
