namespace Lonches_Restaurant.Dtos
{
    public class ComandaDetalleDTO
    {
        public int IdProducto { get; set; }
        public int CantidadPedida { get; set; }
        public double PrecioUnitario { get; set; }
        public string? Observaciones { get; set; }
    }
}
