using CapaModelo;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Text.Json.Nodes;
using System.Threading.Tasks;

namespace CapaNegocio
{
    public class VistaTotal
    {
        public static Result GetAll()
        {
            Result result = new Result();

            try
            {
                using (CapaDatos.PruebaNtContext context = new CapaDatos.PruebaNtContext())
                {
                    
                    var query = (from VistaTotal in context.Totals
                                 select new
                                 {
                                     
                                     name = VistaTotal.CompanyName,
                                     company_id = VistaTotal.CompanyId,
                                     created_at = VistaTotal.CreatedAt,
                                     total = VistaTotal.Total1


                                 }).ToList();

                    result.Objects = new List<object>();

                    if (query != null && query.ToList().Count > 0)
                    {
                        foreach (var obj in query)
                        {

                            CapaModelo.VistaTotal vistaTotal = new CapaModelo.VistaTotal(); 
                            vistaTotal.Cargo = new CapaModelo.Cargo();
                            vistaTotal.Cargo.Name = obj.name;
                            vistaTotal.Cargo.CompanyId = obj.company_id;
                            vistaTotal.Cargo.CreatedAt = obj.created_at.ToString();
                            vistaTotal.Total = obj.total.Value;



                            
                            result.Objects.Add(vistaTotal);
                        }

                        result.Correct = true;
                    }
                    else
                    {
                        result.Correct = false;
                        result.ErrorMessage = "No se encontraron registros";
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

        

        public static CapaModelo.Result GetAllStoredProcedure()
        {
            CapaModelo.Result result = new CapaModelo.Result();
            //aqui se crea el objeto que se retorna
            result.Objects = new List<object> { };
            try
            {
                using (CapaDatos.PruebaNtContext context = new CapaDatos.PruebaNtContext())
                {
                    var fs = FormattableStringFactory.Create("OrderView");
                  
                    var query = context.Totals.FromSql(fs);
                    // for each recuperar info 
                    foreach (var obj in query)
                    {

                        CapaModelo.VistaTotal vistaTotal = new CapaModelo.VistaTotal();
                        vistaTotal.Cargo = new CapaModelo.Cargo();
                        vistaTotal.Cargo.Name = obj.CompanyName;
                        vistaTotal.Cargo.CompanyId = obj.CompanyId;
                        vistaTotal.Cargo.CreatedAt = obj.CreatedAt.ToString();
                        vistaTotal.Total = obj.Total1.Value;




                        result.Objects.Add(vistaTotal);
                    }


                    if (query != null)
                    {
                        result.Correct = true;
                    }
                    else
                    {
                        result.Correct = false;
                    }

                }
            }
            catch (Exception e)
            {
                result.Correct = false;
                result.ErrorMessage = e.InnerException.Message;
                result.Ex = e;
            }

            return result;
        }



    }
}
