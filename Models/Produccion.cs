using System;
using System.Collections.Generic;

namespace Lonches_Restaurant.Models;

public partial class Produccion
{
    public int IdProduccion { get; set; }

    public int IdComanda { get; set; }

    public int IdEmpleado { get; set; }

    public DateTime Fecha { get; set; }

    public virtual Comandum IdComandaNavigation { get; set; } = null!;

    public virtual Empleado IdEmpleadoNavigation { get; set; } = null!;
}
