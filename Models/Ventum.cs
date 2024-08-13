using System;
using System.Collections.Generic;

namespace Lonches_Restaurant.Models;

public partial class Ventum
{
    public int IdVenta { get; set; }

    public int IdEmpleado { get; set; }

    public int IdCliente { get; set; }

    public int IdComanda { get; set; }

    public DateTime Fecha { get; set; }

    public virtual Cliente IdClienteNavigation { get; set; } = null!;

    public virtual Empleado IdEmpleadoNavigation { get; set; } = null!;

    public virtual ICollection<VentaDetalle> VentaDetalles { get; set; } = new List<VentaDetalle>();
}
