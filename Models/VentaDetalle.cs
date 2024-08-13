using System;
using System.Collections.Generic;

namespace Lonches_Restaurant.Models;

public partial class VentaDetalle
{
    public int IdVentaDetalle { get; set; }

    public int IdVenta { get; set; }

    public int IdProducto { get; set; }

    public int CantidadVendida { get; set; }

    public double PrecioUnitario { get; set; }

    public string? Observaciones { get; set; }

    public virtual Producto IdProductoNavigation { get; set; } = null!;

    public virtual Ventum IdVentaNavigation { get; set; } = null!;
}
