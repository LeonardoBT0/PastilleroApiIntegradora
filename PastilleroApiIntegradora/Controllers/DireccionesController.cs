using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using PastilleroApiIntegradora.Models;

namespace PastilleroApiIntegradora.Controllers
{
    public class DireccionesController : ApiController
    {
        private salinas_SampleDBEntities1 db = new salinas_SampleDBEntities1();

        // GET: api/Direcciones
        public IQueryable<Direcciones> GetDirecciones()
        {
            return db.Direcciones;
        }

        // GET: api/Direcciones/5
        [ResponseType(typeof(Direcciones))]
        public IHttpActionResult GetDirecciones(int id)
        {
            Direcciones direcciones = db.Direcciones.Find(id);
            if (direcciones == null)
            {
                return NotFound();
            }

            return Ok(direcciones);
        }

        // PUT: api/Direcciones/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutDirecciones(int id, Direcciones direcciones)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != direcciones.Id)
            {
                return BadRequest();
            }

            db.Entry(direcciones).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!DireccionesExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return StatusCode(HttpStatusCode.NoContent);
        }

        // POST: api/Direcciones
        [ResponseType(typeof(Direcciones))]
        public IHttpActionResult PostDirecciones(Direcciones direcciones)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Direcciones.Add(direcciones);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = direcciones.Id }, direcciones);
        }

        // DELETE: api/Direcciones/5
        [ResponseType(typeof(Direcciones))]
        public IHttpActionResult DeleteDirecciones(int id)
        {
            Direcciones direcciones = db.Direcciones.Find(id);
            if (direcciones == null)
            {
                return NotFound();
            }

            db.Direcciones.Remove(direcciones);
            db.SaveChanges();

            return Ok(direcciones);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool DireccionesExists(int id)
        {
            return db.Direcciones.Count(e => e.Id == id) > 0;
        }
    }
}