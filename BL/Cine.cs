using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL
{
    public class Cine
    {
        public static ML.Result GetAll()
        {
            ML.Result result = new ML.Result();

            try
            {
                using (DL.JgonzalezMoviesContext context = new DL.JgonzalezMoviesContext())
                {
                    var query = context.Cines.FromSqlRaw("CineGetAll").ToList();

                    if (query != null | query.Count > 0)
                    {
                        result.Objects = new List<object>();
                        foreach (var item in query)
                        {
                            ML.Cine cine = new ML.Cine();

                            cine.IdCine = item.IdCine;
                            cine.Nombre = item.Nombre;
                            cine.Direccion = item.Direccion;
                            cine.Venta = item.Venta.Value;

                            cine.Zona = new ML.Zona();
                            cine.Zona.IdZona = item.IdZona.Value;
                            cine.Zona.Descripcion = item.Descripcion;

                            result.Objects.Add(cine);
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
            catch (Exception Ex)
            {
                result.Correct = false;
                result.Ex = Ex;
                result.Message = Ex.Message;
            }

            return result;
        }

        public static ML.Result GetById(int idCine)
        {
            ML.Result result = new ML.Result();

            try
            {
                using (DL.JgonzalezMoviesContext context = new DL.JgonzalezMoviesContext())
                {
                    var query = context.Cines.FromSqlRaw($"CineGetById {idCine}").AsEnumerable().FirstOrDefault();

                    if (query != null)
                    {
                        ML.Cine cine = new ML.Cine();

                        cine.IdCine = query.IdCine;
                        cine.Nombre = query.Nombre;
                        cine.Direccion = query.Direccion;
                        cine.Venta = query.Venta.Value;

                        cine.Zona = new ML.Zona();
                        cine.Zona.IdZona = query.IdZona.Value;
                        cine.Zona.Descripcion = query.Descripcion;

                        result.Object = cine;

                        result.Correct = true;
                    }
                    else
                    {
                        result.Correct = false;
                        result.Message = "No se encontraron registros";
                    }
                }
            }
            catch (Exception Ex)
            {
                result.Correct = false;
                result.Ex = Ex;
                result.Message = Ex.Message;
            }

            return result;
        }

        public static ML.Result Add(ML.Cine cine)
        {
            ML.Result result = new ML.Result();

            try
            {
                using (DL.JgonzalezMoviesContext context = new DL.JgonzalezMoviesContext())
                {
                    var query = context.Database.ExecuteSqlRaw($"CineAdd '{cine.Nombre}', '{cine.Direccion}', {cine.Venta}, {cine.Zona.IdZona}");

                    if (query != null | query > 0)
                    {
                        result.Correct = true;
                    }
                    else
                    {
                        result.Correct = false;
                        result.Message = "No se añadireron registros";
                    }
                }
            }
            catch (Exception Ex)
            {
                result.Correct = false;
                result.Ex = Ex;
                result.Message = Ex.Message;
            }

            return result;
        }

        public static ML.Result Update(ML.Cine cine)
        {
            ML.Result result = new ML.Result();

            try
            {
                using (DL.JgonzalezMoviesContext context = new DL.JgonzalezMoviesContext())
                {
                    var query = context.Database.ExecuteSqlRaw($"CineUpdate {cine.IdCine}, '{cine.Nombre}', '{cine.Direccion}', {cine.Venta}, {cine.Zona.IdZona}");

                    if (query != null | query > 0)
                    {
                        result.Correct = true;
                    }
                    else
                    {
                        result.Correct = false;
                        result.Message = "No se modificaron registros";
                    }
                }
            }
            catch (Exception Ex)
            {
                result.Correct = false;
                result.Ex = Ex;
                result.Message = Ex.Message;
            }

            return result;
        }

        public static ML.Result Delete(int idCine)
        {
            ML.Result result = new ML.Result();

            try
            {
                using (DL.JgonzalezMoviesContext context = new DL.JgonzalezMoviesContext())
                {
                    var query = context.Database.ExecuteSqlRaw($"CineDelete {idCine}");

                    if (query != null | query > 0)
                    {
                        result.Correct = true;
                    }
                    else
                    {
                        result.Correct = false;
                        result.Message = "No se eliminaron registros";
                    }
                }
            }
            catch (Exception Ex)
            {
                result.Correct = false;
                result.Ex = Ex;
                result.Message = Ex.Message;
            }

            return result;
        }
    }
}
