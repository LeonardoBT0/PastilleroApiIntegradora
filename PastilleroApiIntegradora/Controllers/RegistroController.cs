using System.Linq;
using System.Web.Http;
using PastilleroApiIntegradora.Models;
using System.Net;
using System.Net.Http;
using System.Data.Entity;
using System;
using PastilleroApiIntegradora.Modelos;
using System.Security.Cryptography;
using System.Text;

namespace PastilleroApiIntegradora.Controllers
{
    public class RegistroController : ApiController
    {
        private salinas_SampleDBEntities1 db = new salinas_SampleDBEntities1();

        // Enum para los roles
        public enum Roles
        {
            Administrador = 1,
            UsuarioNormal = 2
        }

        // Función para encriptar la contraseña
        private string EncriptarContrasena(string contrasena)
        {
            using (SHA256 sha256 = SHA256.Create())
            {
                byte[] bytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(contrasena));
                return BitConverter.ToString(bytes).Replace("-", "").ToLower();
            }
        }

        // Función para registrar usuario con un rol específico
        private IHttpActionResult RegistrarUsuarioConRol(Usuarios usuario, int rolId, Personas persona)
        {
            usuario.IdRol = rolId;
            persona.IdUsuarios = usuario.Id;
            db.Usuarios.Add(usuario);
            db.SaveChanges();
            return Ok(new { mensaje = "Usuario registrado correctamente" });
        }

        // POST: api/Registro
        [HttpPost]
        [Route("api/Registro")]
        public IHttpActionResult RegistrarUsuario([FromBody] PersonasDto registroData)
        {
            if (registroData == null)
            {
                return BadRequest("Datos incompletos.");
            }

            // Verificar si el email ya está registrado
            var usuarioExistente = db.Usuarios.SingleOrDefault(u => u.Email == registroData.Usuarios.Email);
            if (usuarioExistente != null)
            {
                return BadRequest("El correo electrónico ya está registrado.");
            }

            // Iniciar una transacción
            using (var transaction = db.Database.BeginTransaction())
            {
                try
                {
                    // Crear Persona
                    var persona = new Personas
                    {
                        Nombre = registroData.Nombre,
                        ApellidoPaterno = registroData.ApellidoPaterno,
                        ApellidoMaterno = registroData.ApellidoMaterno,
                        Edad = registroData.Edad,
                        FotoPerfil = registroData.FotoPerfil,
                    };

                    db.Personas.Add(persona);
                    db.SaveChanges(); // Guardamos primero la Persona para obtener su Id

                    // Crear Dirección asociada a la Persona
                    var direccion = new Direcciones
                    {
                        Estado = registroData.Direcciones.Estado,
                        Ciudad = registroData.Direcciones.Ciudad,
                        Calle = registroData.Direcciones.Calle,
                        CodigoPostal = registroData.Direcciones.CodigoPostal,
                        NumExterior = registroData.Direcciones.NumExterior,
                        NumInterior = registroData.Direcciones.NumInterior
                    };

                    db.Direcciones.Add(direccion);
                    db.SaveChanges(); // Guardamos la dirección para obtener su Id

                    // Ahora, actualizamos la Persona con la dirección asignada
                    persona.IdDirecciones = direccion.Id;
                    db.SaveChanges(); // Guardamos los cambios de la persona con la referencia a la dirección

                    // Crear Usuario y asignar roles
                    var usuario = new Usuarios
                    {
                        Email = registroData.Usuarios.Email,
                        Contrasena = EncriptarContrasena(registroData.Usuarios.Contrasena), // Encriptamos la contraseña
                    };

                    if (registroData.Usuarios.IdRol == (int)Roles.Administrador)
                    {
                        return RegistrarUsuarioConRol(usuario, (int)Roles.Administrador, persona);
                    }
                    else
                    {
                        // Verificar si se proporciona ficha técnica
                        if (registroData.FichaTecnica != null)
                        {
                            var fichaTecnica = new FichaTecnica
                            {
                                Estatura = registroData.FichaTecnica.Estatura,
                                TipoSangre = registroData.FichaTecnica.TipoSangre
                            };
                            db.FichaTecnica.Add(fichaTecnica);
                            db.SaveChanges(); // Guardamos la ficha técnica
                        }
                        else
                        {
                            return BadRequest("Ficha técnica es requerida para este usuario.");
                        }

                        return RegistrarUsuarioConRol(usuario, (int)Roles.UsuarioNormal, persona);
                    }
                }
                catch (Exception ex)
                {
                    // Si ocurre un error, revertimos la transacción
                    transaction.Rollback();
                    return InternalServerError(new Exception("Ocurrió un error al registrar el usuario: " + ex.Message));
                }
            }
        }
    }
}
