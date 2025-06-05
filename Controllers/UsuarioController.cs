using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using MvcMovie.Models;
using Microsoft.Data.SqlClient;
using Microsoft.AspNetCore.Authorization;

namespace MvcMovie.Controllers
{
    public class UsuarioController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        string connectionString = @"Server=localhost\SQLEXPRESS;Database=Programacion5;Trusted_Connection=True;TrustServerCertificate=True;";

        //MÉTODO PARA EL ALTA DE LOS USUARIOS
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

        //MÉTODO PARA ELIMINAR UN USUARIO
        public ActionResult Eliminar(int Id)
        {
            Usuario usuario = new Usuario();
            usuario.Id = Id;

            try
            {
                using (SqlConnection conexion = new SqlConnection(connectionString))
                {
                    string query = "DELETE FROM usuarios WHERE Id=@Id";
                    SqlCommand comando = new SqlCommand(query, conexion);
                    comando.Parameters.AddWithValue("@Id", usuario.Id);

                    conexion.Open();
                    int rowsAffected = comando.ExecuteNonQuery();

                    if (rowsAffected > 0)
                    {
                        TempData["Mensaje"] = "El usuario fue eliminado correctamente";
                    }
                    else
                    {
                        TempData["Mensaje"] = "ERROR en la eliminación del usuario";
                    }
                }
            }
            catch (Exception ex)
            {
                TempData["Mensaje"] = "ERROR en la eliminación del usuario, " + ex.Message;
            }

            return RedirectToAction("Listado");
        }
        //MÉTODO PARA LISTAR A LOS USUARIOS DE BASE DE DATOS
        public ActionResult Listado()
        {

            List<Usuario> usuarios = new List<Usuario>();

            using (SqlConnection connection = new SqlConnection(connectionString))
            {

                string query = "SELECT * FROM usuarios";

                SqlCommand command = new SqlCommand(query, connection);
                connection.Open();

                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    usuarios.Add(new Usuario
                    {
                        Id = reader.GetInt32(0),
                        Nombre = reader.GetString(1),
                        Apellido = reader.GetString(2),
                        Email = reader.GetString(3),
                        Username = reader.GetString(4),
                        Password = reader.GetString(5)

                    });
                }
                return View(usuarios);

            }
        }

        //MÉTODOS PARA MODIFICAR USUARIOS

        [HttpPost]
        public IActionResult Editar(Usuario usuario)
        {

            try
            {
                using (SqlConnection conexion = new SqlConnection(connectionString))
                {
                    string query = @"UPDATE usuarios SET
                               Nombre = @Nombre,
                               Apellido = @Apellido,
                               Email = @Email,
                               Usuario = @Username,
                               Contrasenia = @Password
                               WHERE Id = @Id";

                    SqlCommand comando = new SqlCommand(query, conexion);
                    comando.Parameters.AddWithValue("@Nombre", usuario.Nombre);
                    comando.Parameters.AddWithValue("@Apellido", usuario.Apellido);
                    comando.Parameters.AddWithValue("@Email", usuario.Email);
                    comando.Parameters.AddWithValue("@Username", usuario.Username);
                    comando.Parameters.AddWithValue("@Password", usuario.Password);
                    comando.Parameters.AddWithValue("@Id", usuario.Id);

                    conexion.Open();

                    int rowsAffected = comando.ExecuteNonQuery();

                    if (rowsAffected > 0)
                    {
                        TempData["Mensaje"] = "El usuario fue modificado correctamente";
                    }
                    else
                    {
                        TempData["Mensaje"] = "ERROR en la modificación del usuario";
                    }

                }
            }

            catch (Exception ex)
            {
                TempData["Mensaje"] = "ERROR en la modificación del usuario, " + ex.Message;
            }

            return RedirectToAction("Listado");

        }



        public IActionResult Editar(int Id)
        {

            Usuario usuario = new Usuario();

            try
            {
                using (SqlConnection conexion = new SqlConnection(connectionString))
                {
                    string query = "SELECT Id, Nombre, Apellido, Email, Usuario, Contrasenia from usuarios WHERE Id = " + Id.ToString();

                    SqlCommand comando = new SqlCommand(query, conexion);
                    conexion.Open();

                    SqlDataReader reader = comando.ExecuteReader();
                    while (reader.Read())
                    {
                        usuario.Id = reader.GetInt32(0);
                        usuario.Nombre = reader.GetString(1);
                        usuario.Apellido = reader.GetString(2);
                        usuario.Email = reader.GetString(3);
                        usuario.Username = reader.GetString(4);
                        usuario.Password = reader.GetString(5);
                    }
                }
            }

            catch (Exception ex)
            {
                TempData["Mensaje"] = "ERROR en la modificación del usuario, " + ex.Message;
            }

            return View(usuario);
        }
    }
}

