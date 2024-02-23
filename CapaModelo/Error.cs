using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaModelo
{
    public class Error
    {

        public int IdRow { get; set; }

        public string MensajeDeError { get; set; }

        public List<object> Errores { get; set; }
    }
}
