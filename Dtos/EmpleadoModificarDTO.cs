namespace Lonches_Restaurant.Dtos
{
    public class EmpleadoModificarDTO
    {
        public int IdEmpleado { get; set; }
        public string Nombre { get; set; } = null!;

        public string ApePaterno { get; set; } = null!;

        public string ApeMaterno { get; set; } = null!;

        public string Email { get; set; } = null!;

        public int Rol { get; set; }
    }
}
