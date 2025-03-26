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
    public class ControlsController : ApiController
    {
        private salinas_SampleDBEntities1 db = new salinas_SampleDBEntities1();

        // GET: api/Controls
        public IQueryable<Control> GetControl()
        {
            return db.Control;
        }

        // GET: api/Controls/5
        [ResponseType(typeof(Control))]
        public IHttpActionResult GetControl(int id)
        {
            Control control = db.Control.Find(id);
            if (control == null)
            {
                return NotFound();
            }

            return Ok(control);
        }

        // PUT: api/Controls/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutControl(int id, Control control)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != control.Id)
            {
                return BadRequest();
            }

            db.Entry(control).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ControlExists(id))
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

        // POST: api/Controls
        [ResponseType(typeof(Control))]
        public IHttpActionResult PostControl(Control control)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Control.Add(control);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = control.Id }, control);
        }

        // DELETE: api/Controls/5
        [ResponseType(typeof(Control))]
        public IHttpActionResult DeleteControl(int id)
        {
            Control control = db.Control.Find(id);
            if (control == null)
            {
                return NotFound();
            }

            db.Control.Remove(control);
            db.SaveChanges();

            return Ok(control);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool ControlExists(int id)
        {
            return db.Control.Count(e => e.Id == id) > 0;
        }
    }
}