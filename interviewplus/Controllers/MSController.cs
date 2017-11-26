using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace interviewplus.Controllers
{
    public class MSController : ApiController
    {
        // GET: api/MS
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET: api/MS/5
        public string Get(int id)
        {
            return "value";
        }

        // POST: api/MS
        public void Post([FromBody]string value)
        {
        }

        // PUT: api/MS/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE: api/MS/5
        public void Delete(int id)
        {
        }
    }
}
