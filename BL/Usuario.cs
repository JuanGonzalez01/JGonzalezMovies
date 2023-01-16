using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL
{
    public class Usuario
    {
        public static ML.Result GetAll()
        {
            ML.Result result = new ML.Result();

            try
            {
                using (DL.JgonzalezMoviesContext context = new DL.JgonzalezMoviesContext())
                {
                    var query = context.Usuarios.FromSqlRaw("UsuarioGetAll").AsEnumerable().ToList();

                    if (query != null)
                    {
                        result.Objects = new List<object>();

                        foreach (var row in query)
                        {
                            ML.Usuario usuario = new ML.Usuario();

                            usuario.IdUsuario = row.IdUsuario;
                            usuario.Nombre = row.Nombre;
                            usuario.Apellido = row.Apellido;
                            usuario.Nickname = row.Nickname;
                            usuario.Email = row.Email;
                            usuario.Password = row.Password;

                            result.Objects.Add(usuario);
                        }

                        result.Correct = true;
                    }
                    else
                    {
                        result.Correct = false;
                        result.Message = "No se encontraron registros.";
                    }
                }
            }
            catch (Exception ex)
            {
                result.Correct = false;
                result.Message = ex.Message;
                result.Ex = ex;
            }

            return result;
        }

        public static ML.Result GetById(int idUsuario)
        {
            ML.Result result = new ML.Result();

            try
            {
                using (DL.JgonzalezMoviesContext context = new DL.JgonzalezMoviesContext())
                {
                    var query = context.Usuarios.FromSqlRaw($"UsuarioGetById {idUsuario}").AsEnumerable().FirstOrDefault();

                    if (query != null)
                    {
                        ML.Usuario usuario = new ML.Usuario();

                        usuario.IdUsuario = query.IdUsuario;
                        usuario.Nombre = query.Nombre;
                        usuario.Apellido = query.Apellido;
                        usuario.Nickname = query.Nickname;
                        usuario.Email = query.Email;
                        usuario.Password = query.Password;

                        result.Object = usuario;
                        result.Correct = true;
                    }
                    else
                    {
                        result.Correct = false;
                        result.Message = "No se encontraron registros.";
                    }
                }
            }
            catch (Exception ex)
            {
                result.Correct = false;
                result.Message = ex.Message;
                result.Ex = ex;
            }

            return result;
        }

        public static ML.Result Add(ML.Usuario usuario)
        {
            ML.Result result = new ML.Result();

            try
            {
                using (DL.JgonzalezMoviesContext context = new DL.JgonzalezMoviesContext())
                {
                    var query = context.Database.ExecuteSqlRaw($"UsuarioAdd '{usuario.Nombre}', '{usuario.Apellido}', '{usuario.Nickname}', '{usuario.Email}', '{usuario.Password}'");

                    if (query > 0)
                    {
                        result.Correct = true;
                    }
                    else
                    {
                        result.Correct = false;
                        result.Message = "No se insertaron registros.";
                    }
                }
            }
            catch (Exception ex)
            {
                result.Correct = false;
                result.Message = ex.Message;
                result.Ex = ex;
            }

            return result;
        }

        public static ML.Result Update(ML.Usuario usuario)
        {
            ML.Result result = new ML.Result();

            try
            {
                using (DL.JgonzalezMoviesContext context = new DL.JgonzalezMoviesContext())
                {
                    var query = context.Database.ExecuteSqlRaw($"UsuarioUpdate {usuario.IdUsuario}, '{usuario.Nombre}', '{usuario.Apellido}', '{usuario.Nickname}', '{usuario.Email}', '{usuario.Password}'");

                    if (query > 0)
                    {
                        result.Correct = true;
                    }
                    else
                    {
                        result.Correct = false;
                        result.Message = "No se modificaron registros.";
                    }
                }
            }
            catch (Exception ex)
            {
                result.Correct = false;
                result.Message = ex.Message;
                result.Ex = ex;
            }

            return result;
        }

        public static ML.Result Delete(int idUsuario)
        {
            ML.Result result = new ML.Result();

            try
            {
                using (DL.JgonzalezMoviesContext context = new DL.JgonzalezMoviesContext())
                {
                    var query = context.Database.ExecuteSqlRaw($"UsuarioDelete {idUsuario}");

                    if (query > 0)
                    {
                        result.Correct = true;
                    }
                    else
                    {
                        result.Correct = false;
                        result.Message = "No se eliminaron registros.";
                    }
                }
            }
            catch (Exception ex)
            {
                result.Correct = false;
                result.Message = ex.Message;
                result.Ex = ex;
            }

            return result;
        }

        public static ML.Result GetByUserName(string userName)
        {
            ML.Result result = new ML.Result();

            try
            {
                using (DL.JgonzalezMoviesContext context = new DL.JgonzalezMoviesContext())
                {
                    var query = context.Usuarios.FromSqlRaw($"UsuarioGetByUserName '{userName}'").AsEnumerable().FirstOrDefault();

                    if (query != null)
                    {
                        ML.Usuario usuario = new ML.Usuario();

                        usuario.Nickname = query.Nickname;
                        usuario.Password = query.Password;

                        result.Object = usuario;
                        result.Correct = true;
                    }
                    else
                    {
                        result.Correct = false;
                        result.Message = "No se encontró el registro.";
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
