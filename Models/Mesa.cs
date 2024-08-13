using System;
using System.Collections.Generic;

namespace Lonches_Restaurant.Models;

public partial class Mesa
{
    public int IdMesa { get; set; }

    public string NumMesa { get; set; } = null!;

    public int Estatus { get; set; }

    public virtual ICollection<Cliente> Clientes { get; set; } = new List<Cliente>();

    public virtual ICollection<Comandum> Comanda { get; set; } = new List<Comandum>();

    public virtual ICollection<EmpleadoMesaIntermedium> EmpleadoMesaIntermedia { get; set; } = new List<EmpleadoMesaIntermedium>();
}
