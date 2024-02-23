using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaDatos
{
    public class Conexion
    {
        public static string GetConnectionString()
        {
            string connectionString = "Data Source=.;Initial Catalog=PruebaNT;User ID=sa;Password=pass@word!";

            return connectionString;
        }
    }
}
