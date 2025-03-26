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
    public class PadecimientosController : ApiController
    {
        private salinas_SampleDBEntities1 db = new salinas_SampleDBEntities1();

        // GET: api/Padecimientos
        public IQueryable<Padecimientos> GetPadecimientos()
        {
            return db.Padecimientos;
        }

        // GET: api/Padecimientos/5
        [ResponseType(typeof(Padecimientos))]
        public IHttpActionResult GetPadecimientos(int id)
        {
            Padecimientos padecimientos = db.Padecimientos.Find(id);
            if (padecimientos == null)
            {
                return NotFound();
            }

            return Ok(padecimientos);
        }

        // PUT: api/Padecimientos/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutPadecimientos(int id, Padecimientos padecimientos)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != padecimientos.Id)
            {
                return BadRequest();
            }

            db.Entry(padecimientos).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PadecimientosExists(id))
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

        // POST: api/Padecimientos
        [ResponseType(typeof(Padecimientos))]
        public IHttpActionResult PostPadecimientos(Padecimientos padecimientos)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Padecimientos.Add(padecimientos);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = padecimientos.Id }, padecimientos);
        }

        // DELETE: api/Padecimientos/5
        [ResponseType(typeof(Padecimientos))]
        public IHttpActionResult DeletePadecimientos(int id)
        {
            Padecimientos padecimientos = db.Padecimientos.Find(id);
            if (padecimientos == null)
            {
                return NotFound();
            }

            db.Padecimientos.Remove(padecimientos);
            db.SaveChanges();

            return Ok(padecimientos);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool PadecimientosExists(int id)
        {
            return db.Padecimientos.Count(e => e.Id == id) > 0;
        }
    }
}