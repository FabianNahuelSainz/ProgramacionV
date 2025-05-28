using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using MvcMovie.BaseUsuarios;

namespace MvcMovie.Controllers
{
    public class LoginController : Controller
    {
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(string usuario, string contrasenia)
        {
            var user = Base.Usuarios.FirstOrDefault(u => u.Usuario == usuario && u.Contrasenia == contrasenia);
            if (user != null)
            {
                //HttpContext.Session.SetString("User", user.Usuario);
                return RedirectToAction("Index", "Usuario");
            }
            else
            {
                ViewBag.Error = "Credenciales incorrectas.";
            }

            return View();
        }

        public IActionResult Logout()
        {
            HttpContext.SignOutAsync("Cookies");
            return RedirectToAction("Login");
        }
    }
}
