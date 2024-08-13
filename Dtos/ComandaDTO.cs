namespace Lonches_Restaurant.Dtos
{
    public class ComandaDTO
    {
        public int IdCliente { get; set; }
        public int IdMesa { get; set; }
        public int Estatus { get; set; }
        public int IdEmpleado {  get; set; }
        public List<ComandaDetalleDTO> DetallesComanda { get; set; } = new List<ComandaDetalleDTO>();
        public List<ProduccionDTO> Producciones {  get; set; } = new List<ProduccionDTO> { };
    }
}
