using CapaModelo;

namespace CapaNegocio
{
    public class Cargo
    {

        public static Result Insertar(CapaModelo.Cargo compra)
        {
            Result result = new Result();
            //aqui se crea el objeto que se retorna

            try
            {   //manda la cadena de conexion 
                using (CapaDatos.PruebaNtContext context = new CapaDatos.PruebaNtContext())

                {

                    CapaDatos.Cargo cargo = new CapaDatos.Cargo();

                    cargo.Id = compra.Id;
                    cargo.CompanyId = compra.CompanyId;
                    cargo.Amount = decimal.Parse(compra.Amount);
                    cargo.Status = compra.Status;
                    cargo.CreatedAt = DateTime.Parse(compra.CreatedAt);
                    if(compra.PaidAt !="")
                    {
                    cargo.UpdatedAt = DateTime.Parse(compra.PaidAt);
                    }
                    

                    context.Cargos.Add(cargo); //INSERT 
                    int RowsAffected = context.SaveChanges();

                    if (RowsAffected > 0)
                    {
                        result.Correct = true;
                        result.ErrorMessage = "Cargo insertado correctamente";
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
        public static Result GetAll()
        {
            Result result = new Result();

            try
            {
                using (CapaDatos.PruebaNtContext context = new CapaDatos.PruebaNtContext())
                {
                    var query = (from compra in context.DataPruebaTecnicas 
                                 select new
                                 {
                                     id = compra.Id,
                                      name = compra.Name,
                                     company_id = compra.CompanyId,
                                      amount = compra.Amount,
                                      status = compra.Status,
                                      created_at = compra.CreatedAt,
                                      paid_at = compra.PaidAt


                                 }).ToList();

                    result.Objects = new List<object>();

                    if (query != null && query.ToList().Count > 0)
                    {
                        foreach (var obj in query)
                        {
                           CapaModelo.Cargo compra = new CapaModelo.Cargo();
                            compra.Id = obj.id;
                            compra.Name = obj.name;
                            compra.CompanyId = obj.company_id;
                            compra.Amount = obj.amount;
                            compra.Status = obj.status;
                            compra.CreatedAt = obj.created_at;
                            compra.PaidAt = obj.paid_at;
                            result.Objects.Add(compra);
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


    }
}