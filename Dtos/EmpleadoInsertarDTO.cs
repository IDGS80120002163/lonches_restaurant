namespace Lonches_Restaurant.Dtos
{
    public class EmpleadoInsertarDTO
    {
        public string Nombre { get; set; } = null!;

        public string ApePaterno { get; set; } = null!;

        public string ApeMaterno { get; set; } = null!;

        public string Email { get; set; } = null!;

        public string Contrasenia { get; set; } = null!;

        public int Rol { get; set; }

        public int Estatus { get; set; }
    }
}
