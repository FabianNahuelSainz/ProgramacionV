using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using MvcMovie.Models;
using Microsoft.Data.SqlClient;

namespace MvcMovie.Controllers
{
    public class UsuarioController : Controller
    {
        
        public ActionResult Index()
        {
            return View();
        }

        string connectionString = @"Server=localhost\SQLEXPRESS;Database=Programacion5;Trusted_Connection=True;TrustServerCertificate=True;";
        
        public ActionResult ForMetodo(string Nombre, string Apellido, string Email, string Usuario, string Contrasenia)
        {
            Usuario user = new Usuario();
            user.Nombre = Nombre;
            user.Apellido = Apellido;
            user.Email = Email;
            user.Username = Usuario;
            user.Password = Contrasenia;

            try
            {
                using (SqlConnection conexion = new SqlConnection(connectionString))
                {

                    string query = "INSERT INTO usuarios (Nombre, Apellido, Email, Usuario, Contrasenia) " +
                    "VALUES (@Nombre, @Apellido, @Email, @Usuario, @Contrasenia)";
                    SqlCommand command = new SqlCommand(query, conexion);
                    command.Parameters.AddWithValue("@Nombre", user.Nombre);
                    command.Parameters.AddWithValue("@Apellido", user.Apellido);
                    command.Parameters.AddWithValue("@Email", user.Email);
                    command.Parameters.AddWithValue("@Usuario", user.Username);
                    command.Parameters.AddWithValue("@Contrasenia", user.Password);

                    conexion.Open();
                    int rowsAffected = command.ExecuteNonQuery();

                    if (rowsAffected > 0)
                    {
                        ViewBag.Mensaje = "El usuario fue registrado correctamente";
                    }
                    else
                    {
                        ViewBag.Mensaje = "ERROR en el registro de usuario";
                    }
                }
            }

            catch (Exception ex)
            {
                ViewBag.Error = "Error al insertar el usuario: " + user.Username + "; " + ex.Message;
            }

            return View("Index");
        }

    }
}
