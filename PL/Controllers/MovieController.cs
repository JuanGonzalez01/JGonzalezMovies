using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;

namespace PL.Controllers
{
    public class MovieController : Controller
    {
        private readonly IConfiguration _configuration;

        private readonly Microsoft.AspNetCore.Hosting.IHostingEnvironment _hostingEnvironment;

        public MovieController(IConfiguration configuration, Microsoft.AspNetCore.Hosting.IHostingEnvironment hostingEnvironment)
        {
            _configuration = configuration;
            _hostingEnvironment = hostingEnvironment;
        }

        public IActionResult Index()
        {
            ML.Movie movie = new ML.Movie();

            string url = _configuration["UrlAPI"];

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(url);
                
                var responseTask = client.GetAsync("account/16836872/favorite/movies?api_key=13be94f63ff8afa8211e6e8dc5b0fdcb&session_id=1eb81b07ccbac934f96e10b54e1879bbaf62f919&language=es-MX&sort_by=created_at.asc&page=1");
                responseTask.Wait();

                var result = responseTask.Result;

                if (result.IsSuccessStatusCode)
                {
                    var readTask = result.Content.ReadAsStringAsync();

                    dynamic resultJSON = JObject.Parse(readTask.Result.ToString());
                    readTask.Wait();

                    movie.Peliculas = new List<object>();

                    foreach (var item in resultJSON.results)
                    {
                        ML.Movie peli = new ML.Movie();

                        peli.IdMovie = item.id;
                        peli.Nombre = item.title;
                        peli.Fecha = item.release_date;
                        peli.Descripcion = item.overview;

                        peli.Poster = "https://www.themoviedb.org/t/p/w1280/" + item.poster_path;
                        peli.Fondo = "https://www.themoviedb.org/t/p/original/" + item.backdrop_path;

                        movie.Peliculas.Add(peli);
                    }
                }
            }

            return View(movie);
        }

        public IActionResult MejorCalificadas()
        {
            ML.Movie movie = new ML.Movie();

            string url = _configuration["UrlAPI"];

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(url);

                var responseTask = client.GetAsync("movie/popular?api_key=13be94f63ff8afa8211e6e8dc5b0fdcb&language=es-MX&page=1");
                responseTask.Wait();

                var result = responseTask.Result;

                if (result.IsSuccessStatusCode)
                {
                    var readTask = result.Content.ReadAsStringAsync();

                    dynamic resultJSON = JObject.Parse(readTask.Result.ToString());
                    readTask.Wait();

                    movie.Peliculas = new List<object>();

                    foreach (var item in resultJSON.results)
                    {
                        ML.Movie peli = new ML.Movie();

                        peli.IdMovie = item.id;
                        peli.Nombre = item.title;
                        peli.Fecha = item.release_date;
                        peli.Descripcion = item.overview;

                        peli.Poster = "https://www.themoviedb.org/t/p/w1280/" + item.poster_path;
                        peli.Fondo = "https://www.themoviedb.org/t/p/original/" + item.backdrop_path;

                        movie.Peliculas.Add(peli);
                    }
                }
            }

            return View(movie);
        }

        [HttpGet]
        public IActionResult Favoritos(int IdMovie, bool FavPeticion)
        {
            string url = _configuration["UrlAPI"];

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(url);

                ML.Favorito peticion = new ML.Favorito() { media_type = "movie", media_id = IdMovie, favorite = FavPeticion };

                var responseTask = client.PostAsJsonAsync<ML.Favorito>("account/16836872/favorite?api_key=13be94f63ff8afa8211e6e8dc5b0fdcb&session_id=1eb81b07ccbac934f96e10b54e1879bbaf62f919", peticion);
                responseTask.Wait();

                var result = responseTask.Result;

                if (result.IsSuccessStatusCode )
                {
                    ViewBag.Message = "Operación con éxito.";
                }
                else
                {
                    ViewBag.Message = "Error en la operación.";
                }
            }
            return PartialView("Modal");
        }
    }
}
