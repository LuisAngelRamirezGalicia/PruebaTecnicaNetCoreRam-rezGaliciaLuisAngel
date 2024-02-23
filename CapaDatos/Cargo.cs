using System;
using System.Collections.Generic;

namespace CapaDatos;

public partial class Cargo
{
    public string Id { get; set; } = null!;

    public string CompanyId { get; set; } = null!;

    public decimal Amount { get; set; }

    public string Status { get; set; } = null!;

    public DateTime CreatedAt { get; set; }

    public DateTime? UpdatedAt { get; set; }

    public virtual Company Company { get; set; } = null!;
}
