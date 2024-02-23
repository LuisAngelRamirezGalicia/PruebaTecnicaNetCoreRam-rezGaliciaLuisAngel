using System;
using System.Collections.Generic;

namespace CapaDatos;

public partial class Total
{
    public string CompanyId { get; set; } = null!;

    public string CompanyName { get; set; } = null!;

    public DateTime CreatedAt { get; set; }

    public decimal? Total1 { get; set; }
}
