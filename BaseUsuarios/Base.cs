using MvcMovie.Models;

namespace MvcMovie.BaseUsuarios
{
    public class Base
    {
        public static List<Login> Usuarios = new List<Login>
        {
            new Login
            {
                Id=1,
                Usuario="admin",
                Contrasenia="1234"
            }
        };
    }
}
