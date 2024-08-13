using System;
using System.Collections.Generic;

namespace Lonches_Restaurant.Models;

public partial class Empleado
{
    public int IdEmpleado { get; set; }

    public string Nombre { get; set; } = null!;

    public string ApePaterno { get; set; } = null!;

    public string ApeMaterno { get; set; } = null!;

    public string Email { get; set; } = null!;

    public string Contrasenia { get; set; } = null!;

    public int Rol { get; set; }

    public int Estatus { get; set; }

    public virtual ICollection<EmpleadoMesaIntermedium> EmpleadoMesaIntermedia { get; set; } = new List<EmpleadoMesaIntermedium>();

    public virtual ICollection<Produccion> Produccions { get; set; } = new List<Produccion>();

    public virtual ICollection<Ventum> Venta { get; set; } = new List<Ventum>();

    public virtual ICollection<Comandum> Comanda { get; set; } = new List<Comandum>();
}
