using Lonches_Restaurant.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Lonches_Restaurant.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : Controller
    {
        //Creando la variable de context de BD
        private readonly LonchesRestaurantContext _baseDatos;

        public LoginController(LonchesRestaurantContext baseDatos)
        {
            _baseDatos = baseDatos;
        }

        [HttpPost]
        [Route("Login/{email}&{contrasenia}")]
        public async Task<ActionResult<object>> Login(string email, string contrasenia)
        {
            var empleado = await _baseDatos.Empleados
                .Where(e => e.Email == email && e.Contrasenia == contrasenia)
                .Select(e => new
                {
                    e.IdEmpleado,
                    e.Nombre,
                    e.ApePaterno,
                    e.ApeMaterno,
                    e.Email,
                    Num_Rol = e.Rol,
                    Rol = ObtenerRoles(e.Rol)
                })
                .FirstOrDefaultAsync();

            if (empleado == null)
            {
                return NotFound("Empleado no encontrado");
            }

            return Ok(empleado);
        }

        //Método que facilita filtrar los roles de los empleados
        public static string ObtenerRoles(int rol)
        {
            switch (rol)
            {
                case 1: return "Administrador";
                case 2: return "Host";
                case 3: return "Mesero";
                case 4: return "Cocina";
                case 5: return "Limpieza";
                case 6: return "Caja";
                default: return "Rol no definido";
            }
        }
    }
}
