using System;
using System.Collections.Generic;

namespace Lonches_Restaurant.Models;

public partial class EmpleadoMesaIntermedium
{
    public int IdEmpleadoMesa { get; set; }

    public int IdEmpleado { get; set; }

    public int IdMesa { get; set; }

    public virtual Empleado IdEmpleadoNavigation { get; set; } = null!;

    public virtual Mesa IdMesaNavigation { get; set; } = null!;
}
