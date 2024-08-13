using System;
using System.Collections.Generic;

namespace Lonches_Restaurant.Models;

public partial class Comandum
{
    public int IdComanda { get; set; }
    public int IdCliente { get; set; }
    public int IdMesa { get; set; }
    public int Estatus { get; set; }
    public int IdEmpleado { get; set; }
    public virtual ICollection<ComandaDetalle> ComandaDetalles { get; set; } = new List<ComandaDetalle>();
    public virtual Mesa IdMesaNavigation { get; set; } = null!;
    public virtual ICollection<Comandum> Comanda { get; set; } = new List<Comandum>();
    public virtual Cliente IdClienteNavigation { get; set; } = null!;
    public virtual Empleado IdEmpleadoNavigation { get; set; } = null!;
    public virtual ICollection<Produccion> Produccions { get; set; } = new List<Produccion>();
}
