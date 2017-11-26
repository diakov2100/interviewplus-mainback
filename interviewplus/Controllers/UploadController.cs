using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using System.Net.Http;
using MediaToolkit.Model;
using MediaToolkit;
using MediaToolkit.Options;
using System.Web.Http.Cors;


namespace interviewplus.Controllers
{
    public class UploadController : ApiController
    {
        private interviewplusEntities1 db = new interviewplusEntities1();
        public async Task<IHttpActionResult>PostFormData()
        {

            if (!Request.Content.IsMimeMultipartContent())
            {
                return BadRequest();
            }

            var filesReadToProvider = await Request.Content.ReadAsMultipartAsync();

            foreach (var stream in filesReadToProvider.Contents)
            {
                var fileBytes = await stream.ReadAsByteArrayAsync();
                var file = new file() { file1 = fileBytes };
                db.file.Add(file);
                db.SaveChanges();
                return Ok(file.id);
            }
            return Ok();
        }


        public HttpResponseMessage GetFormData(int id)
        {
            HttpResponseMessage response = new HttpResponseMessage();
            response.Content = new StreamContent(new MemoryStream(db.file.Find(id).file1));
            return response;
        }
        // GET: api/answers/5
        [EnableCors(origins: "*", headers: "*", methods: "*", SupportsCredentials = true)]
        public IHttpActionResult Getanswer(int interview_id, int question_id)
        {
            answer answer = db.answer.Where(t1 => t1.interview_id == interview_id && t1.question_id == question_id).FirstOrDefault();
            if (answer == null)
            {
                return NotFound();
            }
            
            return Ok();
        }


        [HttpGet]
        public HttpResponseMessage GetFile(int interview_id, int question_id, string type)
        {
            HttpResponseMessage response = new HttpResponseMessage();
            byte[] b;
            if (type == "text")
            {
                b = Convert.FromBase64String(db.answer.Where(t1 => t1.interview_id == interview_id && t1.question_id == question_id).FirstOrDefault().video);
                response.Content.Headers.ContentType = new MediaTypeHeaderValue("music/ogg");// You can use your own method over here.         
            }
            else if (type == "audio")
            {
                b = Convert.FromBase64String(JsonConvert.DeserializeObject<dynamic>(db.answer.Where(t1 => t1.interview_id == interview_id && t1.question_id == question_id).FirstOrDefault().audio).result);

                response.Content.Headers.ContentType = new MediaTypeHeaderValue("video/webm");
            }
            else
            {
                string text = JsonConvert.DeserializeObject<dynamic>(db.answer.Where(t1 => t1.interview_id == interview_id && t1.question_id == question_id).FirstOrDefault().answer1).result;
                b = Convert.FromBase64String(text);
                response.Content.Headers.ContentType = new MediaTypeHeaderValue("image/png");
            }
            response.Content = new StreamContent(new MemoryStream(b));

            return response;
        }
        /*   private static readonly HttpClient client = new HttpClient();
           public async Task<IHttpActionResult> TestAPI()
           {

               var values = new Dictionary<string, string> {
                   { "thing1", "hello" },
                   { "thing2", "world" }
               };

               var content = new FormUrlEncodedContent(values);
               var stringContent = new StringContent(JsonConvert.SerializeObject(model), Encoding.UTF8, "application/json");
               var response = await client.PostAsync("https://asr.yandex.net//asr_xml?uuid=qwerewrtwtw123&key=da85dfb5-795b-4507-85a9-303f2638a58a&topic=queries", content);

               var responseString = await response.Content.ReadAsStringAsync();

               da85dfb5 - 795b - 4507 - 85a9 - 303f2638a58a
               return Ok();
           }*/

          /*
        [HttpPost]
        public IHttpActionResult PostI(HttpPostedFileBase file)
        {

            if (file.ContentLength > 0)
            {
                var fileName = Path.GetFileName(file.FileName);
                var path = Path.Combine(System.Web.HttpContext.Current.Server.MapPath("~/App_Data/uploads"), fileName);
                file.SaveAs(path);
            }

            return Ok();
        }
        */
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