using System.Linq;
using System.Web.Http;
using PastilleroApiIntegradora.Models;
using System.Net;
using System.Net.Http;

namespace PastilleroApiIntegradora.Controllers
{
    public class LoginController : ApiController
    {
        private salinas_SampleDBEntities1 db = new salinas_SampleDBEntities1();

        // GET: api/Login?email=usuario@example.com&password=miContraseña
        [HttpGet]
        [Route("api/Login")]
        public IHttpActionResult Login([FromUri] string email, [FromUri] string password)
        {
            // Validar que las credenciales no están vacías
            if (string.IsNullOrEmpty(email) || string.IsNullOrEmpty(password))
            {
                return BadRequest("Email y/o contraseña no pueden estar vacíos.");
            }

            // Buscar el usuario en la base de datos utilizando el email y la contraseña
            var usuario = db.Usuarios.SingleOrDefault(u => u.Email == email && u.Contrasena == password);

            if (usuario == null)
            {
                return Unauthorized(); // Si las credenciales no son correctas, retornamos 401 (No autorizado)
            }

            // Si las credenciales son correctas, devolvemos los detalles del usuario
            return Ok(new
            {
                Id = usuario.Id,
                Email = usuario.Email,
                IdRol = usuario.IdRol
            });
        }
    }
}