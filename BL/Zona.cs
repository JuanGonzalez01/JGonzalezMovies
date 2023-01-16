using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL
{
    public class Zona
    {
        public static ML.Result GetAll()
        {
            ML.Result result = new ML.Result();

            try
            {
                using (DL.JgonzalezMoviesContext context = new DL.JgonzalezMoviesContext())
                {
                    var query = context.Zonas.FromSqlRaw("ZonaGetAll").ToList();

                    if (query != null | query.Count > 0)
                    {
                        result.Objects = new List<object>();
                        foreach (var item in query)
                        {
                            ML.Zona zona = new ML.Zona();

                            zona.IdZona = item.IdZona;
                            zona.Descripcion = item.Descripcion;

                            result.Objects.Add(zona);
                        }

                        result.Correct = true;
                    }
                    else
                    {
                        result.Correct = false;
                        result.Message = "No se encontraron registros";
                    }
                }
            }
            catch (Exception ex)
            {
                result.Correct = false;
                result.Ex = ex;
                result.Message = ex.Message;
            }

            return result;
        }
    }
}
