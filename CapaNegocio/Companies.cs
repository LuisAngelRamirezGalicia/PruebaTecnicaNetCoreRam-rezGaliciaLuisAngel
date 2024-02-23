using CapaModelo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaNegocio
{
    public class Companies
    {
        public static Result Insertar(CapaModelo.Cargo compra)
        {
            Result result = new Result();
            //aqui se crea el objeto que se retorna

            try
            {   //manda la cadena de conexion 
                using (CapaDatos.PruebaNtContext context = new CapaDatos.PruebaNtContext())

                {

                   
                    CapaDatos.Company company = new CapaDatos.Company();

                    company.CompanyId = compra.CompanyId;
                    company.CompanyName = compra.Name;

                    context.Companies.Add(company);
                   
                    int RowsAffected = context.SaveChanges();

                    if (RowsAffected > 0)
                    {
                        result.Correct = true;
                        result.ErrorMessage = "compañia insertada correctamente insertado correctamente";
                    }

                }
            }
            catch (Exception ex)
            {
                result.Correct = false;
                result.ErrorMessage = ex.Message;

            }
            return result;
        }
    }
}
