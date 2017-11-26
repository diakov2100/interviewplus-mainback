using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace interviewplus.Models
{
    public class Form
    {
        public int id { get; set; }
        public string name { get; set; }
        public string hr_login { get; set; }
        public IEnumerable<Question> questions { get; set; }
        public Form(form form)
        {
            id = form.id;
            name = form.name;
            hr_login = form.hr_login;
            questions = form.question.AsEnumerable().Select(t1 => new Question(t1)).AsEnumerable();
        }
    }
}