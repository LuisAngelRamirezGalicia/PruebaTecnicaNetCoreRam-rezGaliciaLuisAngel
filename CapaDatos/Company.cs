using System;
using System.Collections.Generic;

namespace CapaDatos;

public partial class Company
{
    public string CompanyId { get; set; } = null!;

    public string CompanyName { get; set; } = null!;

    public virtual ICollection<Cargo> Cargos { get; set; } = new List<Cargo>();
}
