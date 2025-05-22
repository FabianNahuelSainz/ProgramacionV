using Microsoft.AspNetCore.Mvc;
using MvcMovie.Models;

namespace MvcMovie.Controllers
{
    public class UsuarioController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult ForMetodo()
        {
            Usuario usuario;
            List<Usuario> listaUsuario = new List<Usuario>();

            for (int i = 0; i <= 4; i++)
            {
                usuario = new Usuario();
                usuario.Id = i;
                usuario.Nombre = "Fabian";
                usuario.Apellido = "Sainz";
                usuario.Email = "FabianSainz@gmail.com";
                usuario.Username = "fsainz";
                usuario.Password = "1234";

                listaUsuario.Add(usuario);

            }
            ViewBag.ListaUsuario=listaUsuario;

            return View("Index");
        }
    }
}
