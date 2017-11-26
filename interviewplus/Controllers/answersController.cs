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
    public class answersController : ApiController
    {
        private interviewplusEntities1 db = new interviewplusEntities1();

        // GET: api/answers
        [EnableCors(origins: "*", headers: "*", methods: "*", SupportsCredentials = true)]
        public IQueryable<answer> Getanswer()
        {
            return db.answer;
        }

        // GET: api/answers/5
        [EnableCors(origins: "*", headers: "*", methods: "*", SupportsCredentials = true)]
        [ResponseType(typeof(answer))]
        public IHttpActionResult Getanswer(int interview_id)
        {
            IEnumerable<Answer> answers = db.answer.Where(t1 => t1.interview_id == interview_id).AsEnumerable().Select(t1=>new Answer(t1)).AsEnumerable();
            if (answers == null)
            {
                return NotFound();
            }

            return Ok(answers);
        }

        // GET: api/answers/5
        [EnableCors(origins: "*", headers: "*", methods: "*", SupportsCredentials = true)]
        [ResponseType(typeof(answer))]
        public IHttpActionResult Getanswer(int interview_id, int question_id)
        {
            answer answer = db.answer.Where(t1 => t1.interview_id == interview_id && t1.question_id == question_id).FirstOrDefault();
            if (answer == null)
            {
                return NotFound();
            }

            return Ok(new Answer(answer));
        }

        // PUT: api/answers/5
        [EnableCors(origins: "*", headers: "*", methods: "*", SupportsCredentials = true)]
        [ResponseType(typeof(void))]
        public IHttpActionResult Putanswer(int id, answer answer)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != answer.question_id)
            {
                return BadRequest();
            }

            db.Entry(answer).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!answerExists(id))
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

        // POST: api/answers
        [EnableCors(origins: "*", headers: "*", methods: "*", SupportsCredentials = true)]
        [ResponseType(typeof(answer))]
        public IHttpActionResult Postanswer(answer answer)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.answer.Add(answer);
            db.SaveChanges();

            return Ok();
        }

        // DELETE: api/answers/5
        [EnableCors(origins: "*", headers: "*", methods: "*", SupportsCredentials = true)]
        [ResponseType(typeof(answer))]
        public IHttpActionResult Deleteanswer(int id)
        {
            answer answer = db.answer.Find(id);
            if (answer == null)
            {
                return NotFound();
            }

            db.answer.Remove(answer);
            db.SaveChanges();

            return Ok(answer);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool answerExists(int id)
        {
            return db.answer.Count(e => e.question_id == id) > 0;
        }
    }
}