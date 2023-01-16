using Microsoft.AspNetCore.Mvc;

namespace PL.Controllers
{
    public class UsuarioController : Controller
    {
        [HttpGet]
        public IActionResult GetAll()
        {
            return View();
        }

        [HttpGet]
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(ML.Login login)
        {
            ML.Result result = BL.Usuario.GetByUserName(login.UserName);

            if (result.Correct)
            {
                ML.Usuario usuario = (ML.Usuario)result.Object;
                if (usuario.Password == login.Password)
                {
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    ViewBag.Message = "El nombre de usuario y/o contraseña son incorrectos.";
                    ViewBag.Login = false;

                    return PartialView("Modal");
                }
            }
            else
            {
                ViewBag.Message = "El nombre de usuario y/o contraseña son incorrectos.";
                ViewBag.Login = false;

                return PartialView("Modal");
            }

            return View();
        }
    }
}
