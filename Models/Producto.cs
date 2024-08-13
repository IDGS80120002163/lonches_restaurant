using System;
using System.Collections.Generic;

namespace Lonches_Restaurant.Models;

public partial class Producto
{
    public int IdProducto { get; set; }

    public string Nombre { get; set; } = null!;

    public string? Fotografia { get; set; }

    public double Precio { get; set; }

    public int Estatus { get; set; }

    public virtual ICollection<ComandaDetalle> ComandaDetalles { get; set; } = new List<ComandaDetalle>();

    public virtual ICollection<VentaDetalle> VentaDetalles { get; set; } = new List<VentaDetalle>();
}
