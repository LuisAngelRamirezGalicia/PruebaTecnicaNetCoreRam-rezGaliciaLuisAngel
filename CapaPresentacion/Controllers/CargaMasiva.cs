using CapaModelo;
using CapaNegocio;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using System.Reflection;
using System.Text;

using System.Text.Json;

namespace CapaPresentacion.Controllers
{
    public class CargaMasiva : Controller
    {

        public CapaModelo.Result Validar(string row, int contador)
        {
            CapaModelo.Result result = new CapaModelo.Result();
            List<object> error = new List<object>();
            CapaModelo.Cargo compra = new CapaModelo.Cargo();
            bool bandera = true;
            
            //lee linea 
            if (row != "")
            {

                string[] rowFinal = row.Split(',');//separa la linea en un arreglo de string cada que encuentre un '|'
                int contCamposVacios = 0;
                foreach(string i in rowFinal)
                {   
                    if (i == "")
                    {
                        contCamposVacios++;
                    }

                }

              
                if(contCamposVacios < 7 )
                {
                    if (rowFinal[0] != "")
                    {
                        compra.Id = rowFinal[0];
                    }
                    else
                    {
                        bandera = false;
                        CapaModelo.Error error1 = new CapaModelo.Error();
                        error1.IdRow = contador;
                        error1.MensajeDeError = "Id Vacio";
                        error.Add(error1);
                    }
                    if (rowFinal[1] != "")
                    {
                        compra.Name = rowFinal[1];
                    }
                    else
                    {
                        bandera = false;
                        CapaModelo.Error error1 = new CapaModelo.Error();
                        error1.IdRow = contador;
                        error1.MensajeDeError = "Name vacio";
                        error.Add(error1);
                    }
                    if (rowFinal[2] != "")
                    {
                        compra.CompanyId = rowFinal[2];
                    }
                    else
                    {
                        bandera = false;
                        CapaModelo.Error error1 = new CapaModelo.Error();
                        error1.IdRow = contador;
                        error1.MensajeDeError = "CompanyId vacio";
                        error.Add(error1);
                    }
                    if (rowFinal[3] != "")
                    {
                        compra.Amount = rowFinal[3];
                    }
                    else
                    {
                        bandera = false;
                        CapaModelo.Error error1 = new CapaModelo.Error();
                        error1.IdRow = contador;
                        error1.MensajeDeError = "Amount Vacio";
                        error.Add(error1);
                    }
                    if (rowFinal[4] != "")
                    {
                        compra.Status = rowFinal[4];
                    }
                    else
                    {
                        bandera = false;
                        CapaModelo.Error error1 = new CapaModelo.Error();
                        error1.IdRow = contador;
                        error1.MensajeDeError = "Status Vacio";
                        error.Add(error1);
                    }
                    if (rowFinal[5] != "")
                    {
                        compra.CreatedAt = rowFinal[5];
                    }
                    else
                    {
                        bandera = false;
                        CapaModelo.Error error1 = new CapaModelo.Error();
                        error1.IdRow = contador;
                        error1.MensajeDeError = "CreatedAt Vacio";
                        error.Add(error1);
                    }


                    if (rowFinal.Length == 7)
                    {
                        compra.PaidAt = rowFinal[6];
                    }
                result.Correct = bandera;
                 
                result.Object = compra;

                }
                else
                {
                    result.Correct = false;
                }

               

                result.Object = compra;


            } 
            else
            {
                result.Correct = false;
            }
               result.Objects = error;   
                
                
            
            return result;
        }






        [HttpGet]
        public IActionResult Carga()
        {
            return View();
        }

        [HttpPost]
        
        public IActionResult Carga(IFormFile file)
        {


            //lectura del archivo para carga masiva

           
            CapaModelo.Error error = new CapaModelo.Error();
            error.Errores = new List<object> { };
            int contador = 1;
            int contadorRow = 0;
            using (var reader = new StreamReader(file.OpenReadStream()))
            {
                string row = reader.ReadLine();

                
                while (!reader.EndOfStream)
                {

                    
                    bool bandera = true;
                    contador++;
                    row = reader.ReadLine();

                    CapaModelo.Result resultValidar = new CapaModelo.Result();
                    resultValidar = Validar(row, contador);
                    CapaModelo.Cargo cargo = new CapaModelo.Cargo();
                    cargo = (CapaModelo.Cargo)resultValidar.Object;
                    bandera = resultValidar.Correct;
                    error.Errores.AddRange(resultValidar.Objects);

                    CapaModelo.Result result1 = new CapaModelo.Result();
                    CapaModelo.Result result2 = new CapaModelo.Result();
                    
                        if (bandera)
                        {//para optimizacion se comenta esta parte 
                        //insercion en la tabla companies
                           // result2 = CapaNegocio.Companies.Insertar(cargo);
                            result1 = CapaNegocio.Cargo.Insertar(cargo);
                        }

                        if (!result1.Correct)
                        {
                            if (bandera)
                            {
                                CapaModelo.Error error1 = new CapaModelo.Error();
                                error1.IdRow = contador;
                                error1.MensajeDeError = result1.ErrorMessage;
                                error.Errores.Add(error1);
                            }
                           
                        }
                   
                   
                }

               
            }
            HttpContext.Session.SetString("Objeto", "");


            HttpContext.Session.SetString("Objeto", JsonSerializer.Serialize(error));



            return View(error);


        }

        public FileStreamResult DescargarErrores()
        {

            CapaModelo.Result result = new CapaModelo.Result();
            string temp = HttpContext.Session.GetString("Objeto");
             
            CapaModelo.Error errores = JsonSerializer.Deserialize<CapaModelo.Error>(temp);

            
            string cadena = "";
           
            cadena = cadena + "IdRow" + ","+ "MensajeDeError" + "\r\n";
                foreach (var errorString in errores.Errores)
                {
                CapaModelo.Error error = JsonSerializer.Deserialize<CapaModelo.Error>(errorString.ToString());
                
                cadena = cadena + error.IdRow + "," + error.MensajeDeError + "\r\n";

                }
          
            var stream = new MemoryStream(Encoding.ASCII.GetBytes(cadena));
            return new FileStreamResult(stream, "text/plain")
            {
                FileDownloadName = "Errores" + DateTime.Now.ToString() + ".csv"
            };
        }


        public FileStreamResult DescargarReporte()
        {

            CapaModelo.Result result = new CapaModelo.Result();
            string temp = HttpContext.Session.GetString("Reporte");
            
             result = JsonSerializer.Deserialize<CapaModelo.Result>(temp);

            
            string cadena = "";
       
            cadena = cadena + "company_id" + "," + "company_name" + "," + "created_at" + "," + "Total" + "\r\n";
            foreach (var reporteString in result.Objects)
            {
                CapaModelo.VistaTotal vista = JsonSerializer.Deserialize<CapaModelo.VistaTotal>(reporteString.ToString());
                
                cadena = cadena + vista.Cargo.CompanyId + "," + vista.Cargo.Name + ","+ vista.Cargo.CreatedAt + "," + vista.Total + "\r\n";

            }
            
            var stream = new MemoryStream(Encoding.ASCII.GetBytes(cadena));
            return new FileStreamResult(stream, "text/plain")
            {
                FileDownloadName = "Reporte" + DateTime.Now.ToString() + ".csv"
            };
        }



        [HttpGet]
        public IActionResult Reporte()
        {
            CapaModelo.Result result = new CapaModelo.Result();
            result = CapaNegocio.VistaTotal.GetAllStoredProcedure();
            HttpContext.Session.SetString("Reporte", "");


            HttpContext.Session.SetString("Reporte", JsonSerializer.Serialize(result));


            return View(result);
        }





    }
}
