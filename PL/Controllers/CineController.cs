using Microsoft.AspNetCore.Mvc;
using ML;
using System.Collections;
using System.Collections.Generic;

namespace PL.Controllers
{
    public class CineController : Controller
    {
        [HttpGet]
        public IActionResult GetAll()
        {
            //GetAll
            ML.Result result = BL.Cine.GetAll();

            return View(result);
        }

        [HttpGet]
        public IActionResult Form(int IdCine)
        {
            ML.Cine cine = new ML.Cine();
            ML.Result resultZona = BL.Zona.GetAll();

            if (IdCine != 0)
            {
                //GetById
                ML.Result result = BL.Cine.GetById(IdCine);

                if (!result.Correct)
                {
                    ViewBag.Message = result.Message;
                    return PartialView("Modal");
                }
                else
                {
                    cine = (ML.Cine)result.Object;
                }
            }
            else
            {
                cine.Zona = new ML.Zona();
            }
            
            cine.Zona.Zonas = resultZona.Objects;
            return View(cine);
        }

        [HttpPost]
        public IActionResult Form(ML.Cine cine)
        {
            if (cine.IdCine == 0)
            {
                //Add
                ML.Result result = BL.Cine.Add(cine);

                if (result.Correct)
                {
                    ViewBag.Message = "Cine añadido con éxito.";

                }
                else
                {
                    ViewBag.Message = $"Error al añadir el cine: {result.Message}";
                }
            }
            else
            {
                //Update
                ML.Result result = BL.Cine.Update(cine);

                if (result.Correct)
                {
                    ViewBag.Message = "Cine modificado con éxito.";

                }
                else
                {
                    ViewBag.Message = $"Error al modificar el cine: {result.Message}";
                }
            }

            return PartialView("Modal");
        }

        [HttpGet]
        public IActionResult Delete(int IdCine)
        {
            ML.Result result = BL.Cine.Delete(IdCine);

            if (result.Correct)
            {
                ViewBag.Message = "Cine eliminado con éxito.";

            }
            else
            {
                ViewBag.Message = $"Error al eliminar el cine: {result.Message}";
            }

            return PartialView("Modal");
        }

        [HttpGet]
        public IActionResult Estadisticas()
        {
            List<ML.Cine> cines = new List<Cine>();

            //Recorremos los cines del result y los agregamos a la nueva lista haciendo unboxing
            foreach (ML.Cine cine in BL.Cine.GetAll().Objects)
            {
                cines.Add(cine);
            }

            //Obtenemos la sumatoria de todas las ventas en general
            int totalVentas = cines.Sum(cine => cine.Venta);

            //Diccionario para guardar el total de ventas por zona. Por expresiones lambda filtramos la lista de los cines por zona, y de la lista resultante obtenemos la sumatoria de las ventas
            Dictionary<string, float> ventasZona= new Dictionary<string, float>() { { "Norte", cines.Where(cine => cine.Zona.Descripcion == "Norte").ToList().Sum(cine => cine.Venta) },
                                                                                 { "Sur", cines.Where(cine => cine.Zona.Descripcion == "Sur").ToList().Sum(cine => cine.Venta) },
                                                                                { "Este", cines.Where(cine => cine.Zona.Descripcion == "Este").ToList().Sum(cine => cine.Venta) },
                                                                                 { "Oeste", cines.Where(cine => cine.Zona.Descripcion == "Oeste").ToList().Sum(cine => cine.Venta) },};

            //Diccionario para guardar el promedio de ventas por zona
            Dictionary<string, float> promedioVentasZona = new Dictionary<string, float>() { {"Norte", (ventasZona["Norte"] * 100) / totalVentas },
                                                                                          {"Sur", (ventasZona["Sur"] * 100) / totalVentas },
                                                                                            {"Este", (ventasZona["Este"] * 100) / totalVentas },
                                                                                            {"Oeste", (ventasZona["Oeste"] * 100) / totalVentas }};

            //Retornamos a la vista el diccionario de los promedios
            return View(promedioVentasZona);
        }
    }
}
