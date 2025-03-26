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
    public class MedicamentosController : ApiController
    {
        private salinas_SampleDBEntities1 db = new salinas_SampleDBEntities1();

        // GET: api/Medicamentos
        public IQueryable<Medicamentos> GetMedicamentos()
        {
            return db.Medicamentos;
        }

        // GET: api/Medicamentos/5
        [ResponseType(typeof(Medicamentos))]
        public IHttpActionResult GetMedicamentos(int id)
        {
            Medicamentos medicamentos = db.Medicamentos.Find(id);
            if (medicamentos == null)
            {
                return NotFound();
            }

            return Ok(medicamentos);
        }

        // PUT: api/Medicamentos/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutMedicamentos(int id, Medicamentos medicamentos)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != medicamentos.Id)
            {
                return BadRequest();
            }

            db.Entry(medicamentos).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!MedicamentosExists(id))
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

        // POST: api/Medicamentos
        [ResponseType(typeof(Medicamentos))]
        public IHttpActionResult PostMedicamentos(Medicamentos medicamentos)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Medicamentos.Add(medicamentos);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = medicamentos.Id }, medicamentos);
        }

        // DELETE: api/Medicamentos/5
        [ResponseType(typeof(Medicamentos))]
        public IHttpActionResult DeleteMedicamentos(int id)
        {
            Medicamentos medicamentos = db.Medicamentos.Find(id);
            if (medicamentos == null)
            {
                return NotFound();
            }

            db.Medicamentos.Remove(medicamentos);
            db.SaveChanges();

            return Ok(medicamentos);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool MedicamentosExists(int id)
        {
            return db.Medicamentos.Count(e => e.Id == id) > 0;
        }
    }
}