using System;
using System.Collections.Generic;

namespace Lonches_Restaurant.Models;

public partial class ComandaDetalle
{
    public int IdComandaDetalle { get; set; }

    public int IdComanda { get; set; }

    public int IdProducto { get; set; }

    public int CantidadPedida { get; set; }

    public double PrecioUnitario { get; set; }

    public string? Observaciones { get; set; }

    public virtual Comandum IdComandaNavigation { get; set; } = null!;

    public virtual Producto IdProductoNavigation { get; set; } = null!;
}
