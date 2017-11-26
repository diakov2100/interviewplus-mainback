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
using interviewplus;
using System.Web.Http.Cors;

namespace interviewplus.Controllers
{
    //[EnableCors(origins: "http://interviewplus.azurewebsites.net/", headers: "*", methods: "*")]
    public class hrsController : ApiController
    {
        private interviewplusEntities1 db = new interviewplusEntities1();

        // POST: api/hrs
        [EnableCors(origins: "*", headers: "*", methods: "*", SupportsCredentials = true)]
        [ResponseType(typeof(hr))]
        public IHttpActionResult Posthr(hr hr)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.hr.Add(hr);

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateException)
            {
                if (hrExists(hr.login))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return Ok();
        }

        // DELETE: api/hrs/5
        [ResponseType(typeof(hr))]
        public IHttpActionResult Deletehr(string id)
        {
            hr hr = db.hr.Find(id);
            if (hr == null)
            {
                return NotFound();
            }

            db.hr.Remove(hr);
            db.SaveChanges();

            return Ok(hr);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool hrExists(string id)
        {
            return db.hr.Count(e => e.login == id) > 0;
        }
    }
}