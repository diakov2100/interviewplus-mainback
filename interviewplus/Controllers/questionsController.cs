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
using Newtonsoft.Json;
using interviewplus.Models;
using System.Web.Http.Cors;

namespace interviewplus.Controllers
{
    //[EnableCors(origins: "http://interviewplus.azurewebsites.net/", headers: "*", methods: "*")]
    public class questionsController : ApiController
    {
        private interviewplusEntities1 db = new interviewplusEntities1();

        // GET: api/questions
        [EnableCors(origins: "*", headers: "*", methods: "*", SupportsCredentials = true)]
        public IEnumerable<Question> Getquestion()
        {
            var test = db.question.Where(t1 => t1 != null).AsEnumerable().Select(t2 => new Question(t2)).AsEnumerable();
            return test;
        }

        // GET: api/questions/5
        [EnableCors(origins: "*", headers: "*", methods: "*", SupportsCredentials = true)]
        [ResponseType(typeof(question))]
        public IHttpActionResult Getquestion(int form)
        {
            var questions = db.question.Where(t1 => t1.form_id == form).AsEnumerable().Select(t2 => new Question(t2)).AsEnumerable();
            if (questions == null)
            {
                return NotFound();
            }

            return Ok(questions);
        }

        public class CustomList
        {
            public IQueryable<question> data { get; set; }
        }
        // PUT: api/questions
        [EnableCors(origins: "*", headers: "*", methods: "*", SupportsCredentials = true)]
        [ResponseType(typeof(question))]
        public IHttpActionResult Putquestion([FromBody] dynamic data, [FromUri] int form_id)
        {
            var questions = JsonConvert.DeserializeObject<List<question>>(data.ToString());
            db.question.Where(p => p.form_id==form_id)
               .ToList().ForEach(p => db.question.Remove(p));
            db.SaveChanges();
            foreach (var tec in questions)
            {
                db.question.Add(tec);
            }
            db.SaveChanges();

            return Ok();
        }
       

        // POST: api/questions
        [EnableCors(origins: "*", headers: "*", methods: "*", SupportsCredentials = true)]
        [ResponseType(typeof(question))]
        public IHttpActionResult Postquestion([FromBody] dynamic data)
        {
            var questions = JsonConvert.DeserializeObject<List<question>>(data.ToString());
            foreach (var tec in questions)
            {
                db.question.Add(tec);
            }
            db.SaveChanges();

            return Ok();
        }

        // DELETE: api/questions/5
        [EnableCors(origins: "*", headers: "*", methods: "*", SupportsCredentials = true)]
        [ResponseType(typeof(question))]
        public IHttpActionResult Deletequestion(int id)
        {
            question question = db.question.Find(id);
            if (question == null)
            {
                return NotFound();
            }

            db.question.Remove(question);
            db.SaveChanges();

            return Ok(question);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool questionExists(int id)
        {
            return db.question.Count(e => e.id == id) > 0;
        }
    }
}