using Lonches_Restaurant.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Lonches_Restaurant.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DashboardController : Controller
    {        
        //Creando la variable de context de BD
        private readonly LonchesRestaurantContext _baseDatos;

        public DashboardController(LonchesRestaurantContext baseDatos)
        {
            _baseDatos = baseDatos;
        }

        [HttpGet]
        [Route("ReportePorDia")]
        public async Task<ActionResult<IEnumerable<object>>> ReportePorDia()
        {
            var reporte = await _baseDatos.Venta
                .Select(v => new
                {
                    v.IdEmpleado,
                    v.Fecha
                })
                .ToListAsync();

            return Ok(reporte);
        }
    }
}
