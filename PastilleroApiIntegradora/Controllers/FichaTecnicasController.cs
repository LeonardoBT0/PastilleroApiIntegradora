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
    public class FichaTecnicasController : ApiController
    {
        private salinas_SampleDBEntities1 db = new salinas_SampleDBEntities1();

        // GET: api/FichaTecnicas
        public IQueryable<FichaTecnica> GetFichaTecnica()
        {
            return db.FichaTecnica;
        }

        // GET: api/FichaTecnicas/5
        [ResponseType(typeof(FichaTecnica))]
        public IHttpActionResult GetFichaTecnica(int id)
        {
            FichaTecnica fichaTecnica = db.FichaTecnica.Find(id);
            if (fichaTecnica == null)
            {
                return NotFound();
            }

            return Ok(fichaTecnica);
        }

        // PUT: api/FichaTecnicas/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutFichaTecnica(int id, FichaTecnica fichaTecnica)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != fichaTecnica.Id)
            {
                return BadRequest();
            }

            db.Entry(fichaTecnica).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!FichaTecnicaExists(id))
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

        // POST: api/FichaTecnicas
        [ResponseType(typeof(FichaTecnica))]
        public IHttpActionResult PostFichaTecnica(FichaTecnica fichaTecnica)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.FichaTecnica.Add(fichaTecnica);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = fichaTecnica.Id }, fichaTecnica);
        }

        // DELETE: api/FichaTecnicas/5
        [ResponseType(typeof(FichaTecnica))]
        public IHttpActionResult DeleteFichaTecnica(int id)
        {
            FichaTecnica fichaTecnica = db.FichaTecnica.Find(id);
            if (fichaTecnica == null)
            {
                return NotFound();
            }

            db.FichaTecnica.Remove(fichaTecnica);
            db.SaveChanges();

            return Ok(fichaTecnica);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool FichaTecnicaExists(int id)
        {
            return db.FichaTecnica.Count(e => e.Id == id) > 0;
        }
    }
}