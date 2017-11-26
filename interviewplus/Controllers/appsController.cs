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
using interviewplus.Models;
using System.Web.Http.Cors;


namespace interviewplus.Controllers
{
    //[EnableCors(origins: "http://interviewplus.azurewebsites.net/", headers: "*", methods: "*")]
    public class appsController : ApiController
    {
        private interviewplusEntities1 db = new interviewplusEntities1();

        [EnableCors(origins: "*", headers: "*", methods: "*", SupportsCredentials = true)]
        // GET: api/apps
        public IEnumerable<App> Getapp()
        {
            var test = db.app.Where(t1=>t1!=null).AsEnumerable().Select(t2 => new App(t2)).AsEnumerable();
            return test;
        }

        // GET: api/apps/5
        [EnableCors(origins: "*", headers: "*", methods: "*", SupportsCredentials = true)]
        [ResponseType(typeof(app))]
        public IHttpActionResult Getapp(int id)
        {
            app app = db.app.Find(id);
            if (app == null)
            {
                return NotFound();
            }

            return Ok(app);
        }

        // PUT: api/apps/5
        [EnableCors(origins: "*", headers: "*", methods: "*", SupportsCredentials = true)]
        [ResponseType(typeof(void))]
        public IHttpActionResult Putapp(string name, app app)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (name != app.name)
            {
                return BadRequest();
            }

            db.Entry(app).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!appExists(name))
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

        // POST: api/apps
        [EnableCors(origins: "*", headers: "*", methods: "*", SupportsCredentials = true)]
        [ResponseType(typeof(app))]
        public IHttpActionResult Postapp(app app)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.app.Add(app);
            db.SaveChanges();

            return Ok();
        }

        // DELETE: api/apps/5
        [EnableCors(origins: "*", headers: "*", methods: "*", SupportsCredentials = true)]
        [ResponseType(typeof(app))]
        public IHttpActionResult Deleteapp(int id)
        {
            app app = db.app.Find(id);
            if (app == null)
            {
                return NotFound();
            }

            db.app.Remove(app);
            db.SaveChanges();

            return Ok(app);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool appExists(string name)
        {
            return db.app.Count(e => e.name == name) > 0;
        }
    }
}