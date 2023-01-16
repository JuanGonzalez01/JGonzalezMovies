using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ML
{
    public class Movie
    {
        public int IdMovie { get; set; }
        public string Nombre { get; set; }
        public string Fecha { get; set; }
        public string Descripcion { get; set; }
        public string Poster { get; set; }
        public string Fondo { get; set; }
        public List<Object> Peliculas { get; set; }
    }
}
