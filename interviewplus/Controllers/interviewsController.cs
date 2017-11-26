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
        [EnableCors(origins: "*", headers: "*", methods: "*", SupportsCredentials = true)]
    public class interviewsController : ApiController
    {
        private interviewplusEntities1 db = new interviewplusEntities1();

        // GET: api/interviews
        public IQueryable<interview> Getinterview()
        {
            return db.interview;
        }

        // GET: api/interviews/5
        [ResponseType(typeof(interview))]
        public IHttpActionResult Getinterview(int form_id)
        {
            IEnumerable<Interview>  interviews = db.interview.Where(t1=>t1.form_id==form_id).AsEnumerable().Select(t1=> new FullInterview(t1)).AsEnumerable() ;
            if (interviews == null)
            {
                return NotFound();
            }

            return Ok(interviews);
        }

        // GET: api/interviews/5
        [ResponseType(typeof(interview))]
        public IHttpActionResult GetInterview(int id)
        {
            var interviews = db.interview.Find(id);
            if (interviews == null)
            {
                return NotFound();
            }

            return Ok(new Interview(interviews));
        }

        // PUT: api/interviews/5
        [EnableCors(origins: "*", headers: "*", methods: "*", SupportsCredentials = true)]
        [ResponseType(typeof(void))]
        public IHttpActionResult Putinterview(int id, interview interview)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != interview.id)
            {
                return BadRequest();
            }

            db.Entry(interview).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!interviewExists(id))
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

        // POST: api/interviews
        [EnableCors(origins: "*", headers: "*", methods: "*", SupportsCredentials = true)]
        [ResponseType(typeof(interview))]
        public IHttpActionResult Postinterview(interview interview)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.interview.Add(interview);
            db.SaveChanges();

            return Ok(interview.id);
        }

        // DELETE: api/interviews/5
        [EnableCors(origins: "*", headers: "*", methods: "*", SupportsCredentials = true)]
        [ResponseType(typeof(interview))]
        public IHttpActionResult Deleteinterview(int id)
        {
            interview interview = db.interview.Find(id);
            if (interview == null)
            {
                return NotFound();
            }

            db.interview.Remove(interview);
            db.SaveChanges();

            return Ok(interview);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool interviewExists(int id)
        {
            return db.interview.Count(e => e.id == id) > 0;
        }
    }
}