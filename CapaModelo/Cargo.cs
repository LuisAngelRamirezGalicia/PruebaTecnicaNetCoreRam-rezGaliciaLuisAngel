using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaModelo
{
    public class Cargo
    {

        public string? Id { get; set; }

        public string? Name { get; set; }

        public string? CompanyId { get; set; }

        public string? Amount { get; set; }

        public string? Status { get; set; }

        public string? CreatedAt { get; set; }

        public string? PaidAt { get; set; }

        public List<object> Compras { get; set; }
       // public decimal Total { get; set; }
    }
}
