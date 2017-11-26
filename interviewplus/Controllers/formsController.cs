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
 //   [EnableCors(origins: "http://interviewplus.azurewebsites.net/", headers: "*", methods: "*")]
    public class formsController : ApiController
    {
        private interviewplusEntities1 db = new interviewplusEntities1();


        // GET: api/forms/5
        //  [EnableCors(origins: "*", headers: "*", methods: "*")]
        [EnableCors(origins: "*", headers: "*", methods: "*", SupportsCredentials = true)]
        [ResponseType(typeof(List<form>))]
        public IHttpActionResult Getform(string hr)
        {
            
            IEnumerable<Form> forms = db.form.Where(t1=>t1.hr_login==hr).AsEnumerable().Select(t1 => new Form(t1)).OrderByDescending(t1=>t1.id).AsEnumerable();
            if (forms == null)
            {
                return NotFound();
            }
            return Ok(forms);
        }

        // PUT: api/forms/5
        [EnableCors(origins: "*", headers: "*", methods: "*", SupportsCredentials = true)]
        [ResponseType(typeof(void))]
        public IHttpActionResult Putform(int id, form form)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != form.id)
            {
                return BadRequest();
            }

            db.Entry(form).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!formExists(id))
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

        // POST: api/forms
        [EnableCors(origins: "*", headers: "*", methods: "*", SupportsCredentials = true)]
        [ResponseType(typeof(form))]
        public IHttpActionResult Postform(form form)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (db.hr.Where(t1 => t1.login==form.hr_login).FirstOrDefault() == null) return NotFound();

            form Form = new form() {hr_login=form.hr_login, name=form.name};
            
            db.form.Add(Form);
            db.SaveChanges();

            return Ok(Form.id);
        }

        // DELETE: api/forms/5
        [EnableCors(origins: "*", headers: "*", methods: "*", SupportsCredentials = true)]
        [ResponseType(typeof(form))]
        public IHttpActionResult Deleteform(int id)
        {
            form form = db.form.Find(id);
            if (form == null)
            {
                return NotFound();
            }

            db.form.Remove(form);
            db.SaveChanges();

            return Ok(form);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool formExists(int id)
        {
            return db.form.Count(e => e.id == id) > 0;
        }
    }
}