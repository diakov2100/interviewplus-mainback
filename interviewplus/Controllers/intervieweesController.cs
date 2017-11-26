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
    public class intervieweesController : ApiController
    {
        private interviewplusEntities1 db = new interviewplusEntities1();

        // GET: api/interviewees
        [EnableCors(origins: "*", headers: "*", methods: "*", SupportsCredentials = true)]
        public IQueryable<interviewee> Getinterviewee()
        {
            return db.interviewee;
        }
        // GET: api/interviewees/5
        [EnableCors(origins: "*", headers: "*", methods: "*", SupportsCredentials = true)]
        [ResponseType(typeof(interviewee))]
        public IHttpActionResult Getinterviewee(string id)
        {
            interviewee interviewee = db.interviewee.Where(t1=>t1.uid==id).FirstOrDefault();
            if (interviewee == null)
            {
                return NotFound();
            }

            return Ok(interviewee);
        }

        // GET: api/interviewees/5
        [EnableCors(origins: "*", headers: "*", methods: "*", SupportsCredentials = true)]
        [ResponseType(typeof(interviewee))]
        public IHttpActionResult Getinterviewee(int id)
        {
            interviewee interviewee = db.interviewee.Find(id);
            if (interviewee == null)
            {
                return NotFound();
            }

            return Ok(interviewee);
        }

        // POST: api/interviewees
        [EnableCors(origins: "*", headers: "*", methods: "*", SupportsCredentials = true)]
        [ResponseType(typeof(interviewee))]
        public IHttpActionResult Postinterviewee(string name)
        {
            interviewee interviewee = new interviewee() { name = name};
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            
            db.interviewee.Add(interviewee);
            db.SaveChanges();

            return Ok(interviewee.id);
        }
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool intervieweeExists(int id)
        {
            return db.interviewee.Count(e => e.id == id) > 0;
        }
    }
}