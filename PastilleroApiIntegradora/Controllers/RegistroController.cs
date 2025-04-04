﻿using System.Linq;
using System.Web.Http;
using PastilleroApiIntegradora.Models;
using System.Net;
using System.Net.Http;
using System.Data.Entity;
using System;
using PastilleroApiIntegradora.Modelos;

namespace PastilleroApiIntegradora.Controllers
{
    public class RegistroController : ApiController
    {
        private salinas_SampleDBEntities1 db = new salinas_SampleDBEntities1();

        [HttpPost]
        [Route("api/Registro")]
        public IHttpActionResult RegistrarUsuario([FromBody] PersonasDto registroData)
        {
            if (registroData == null)
            {
                return BadRequest("Datos incompletos.");
            }

            // Verificar si el email ya está registrado
            var usuarioExistente = db.Usuarios.FirstOrDefault(u => u.Email == registroData.Usuarios.Email);
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
                        FotoPerfil = registroData.FotoPerfil
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
                    db.SaveChanges(); // Guardamos la dirección

                    // Actualizamos Persona con la Dirección
                    persona.IdDirecciones = direccion.Id;
                    db.SaveChanges(); // Guardamos la relación

                    // Crear Usuario
                    var usuario = new Usuarios
                    {
                        Email = registroData.Usuarios.Email,
                        Contrasena = registroData.Usuarios.Contrasena,
                        IdRol = registroData.Usuarios.IdRol
                    };

                    db.Usuarios.Add(usuario);
                    db.SaveChanges(); // Guardamos el usuario

                    // Actualizamos el IdUsuario en la Persona
                    persona.IdUsuarios = usuario.Id;
                    db.SaveChanges(); // Guardamos la relación Persona-Usuario

                    // Si el rol es de Usuario Normal (IdRol = 2), se guarda la ficha técnica
                    if (registroData.Usuarios.IdRol == 2)
                    {
                        if (registroData.FichaTecnica != null)
                        {
                            var fichaTecnica = new FichaTecnica
                            {
                                Estatura = registroData.FichaTecnica.Estatura,
                                TipoSangre = registroData.FichaTecnica.TipoSangre
                            };
                            db.FichaTecnica.Add(fichaTecnica);
                            db.SaveChanges(); // Guardamos la ficha técnica

                            // Actualizamos la relación con la persona
                            persona.IdFichaTecnica = fichaTecnica.Id;
                            db.SaveChanges(); // Guardamos la relación Persona-FichaTecnica
                        }
                        else
                        {
                            return BadRequest("Ficha técnica es requerida para este usuario.");
                        }
                    }

                    // Confirmamos la transacción
                    transaction.Commit();

                    return Ok(new { mensaje = "Usuario registrado correctamente" });
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
