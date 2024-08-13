using Lonches_Restaurant.Dtos;
using Lonches_Restaurant.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Lonches_Restaurant.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmpleadoController : Controller
    {
        //Creando la variable de context de BD
        private readonly LonchesRestaurantContext _baseDatos;

        public EmpleadoController(LonchesRestaurantContext baseDatos)
        {
            _baseDatos = baseDatos;
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

        [HttpGet]
        [Route("ListaEmpleados")]
        public async Task<ActionResult<IEnumerable<object>>> GetEmpleados()
        {
            var empleados = await _baseDatos.Empleados
                .Select(e => new
                {
                    e.IdEmpleado,
                    e.Nombre,
                    e.ApePaterno,
                    e.ApeMaterno,
                    e.Email,
                    Rol = ObtenerRoles(e.Rol)
                })
                .ToListAsync();

            return Ok(empleados);
        }

        [HttpPost]
        [Route("AgregarEmpleado")]
        public async Task<IActionResult> Agregar([FromBody] EmpleadoInsertarDTO request)
        {
            var empleado = new Empleado
            {
                Nombre = request.Nombre,
                ApePaterno = request.ApePaterno,
                ApeMaterno = request.ApeMaterno,
                Email = request.Email,
                Contrasenia = request.Contrasenia,
                Rol = request.Rol,
                Estatus = 1
            };

            await _baseDatos.Empleados.AddAsync(empleado);
            await _baseDatos.SaveChangesAsync();

            return Ok(empleado);
        }

        [HttpPut]
        [Route("ModificarEmpleado/{id:int}")]
        public async Task<IActionResult> Modificar(int id, [FromBody] EmpleadoModificarDTO request)
        {
            var empleadoModificar = await _baseDatos.Empleados.FindAsync(id);
            if (empleadoModificar == null)
            {
                return BadRequest("No existe el empleado");
            }

            empleadoModificar.Nombre = request.Nombre;
            empleadoModificar.ApePaterno = request.ApePaterno;
            empleadoModificar.ApeMaterno = request.ApeMaterno;
            empleadoModificar.Email = request.Email;
            empleadoModificar.Rol = request.Rol;

            try
            {
                await _baseDatos.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                return NotFound();
            }

            return Ok();
        }

        [HttpDelete]
        [Route("EliminarEmpleado/{id:int}")]
        public async Task<IActionResult> Eliminar(int id)
        {
            var empleadoEliminar = await _baseDatos.Empleados.FindAsync(id);
            if (empleadoEliminar == null)
            {
                return BadRequest("No existe el empleado");
            }

            empleadoEliminar.Estatus = 0;
            await _baseDatos.SaveChangesAsync();

            return Ok();
        }

    }
}
