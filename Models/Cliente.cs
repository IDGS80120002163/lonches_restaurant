using System;
using System.Collections.Generic;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace Lonches_Restaurant.Models;

public partial class Cliente
{
    public int IdCliente { get; set; }

    public string Nombre { get; set; } = null!;

    public int IdMesa { get; set; }

    public virtual Mesa IdMesaNavigation { get; set; } = null!;

    public virtual ICollection<Ventum> Venta { get; set; } = new List<Ventum>();
    public virtual ICollection<Comandum> Comanda { get; set; } = new List<Comandum>();
}
