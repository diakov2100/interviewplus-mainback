using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace interviewplus.Models
{
    public class Question
    {
        public int id { get; set; }
        public int form_id { get; set; }
        public string settings { get; set; }
        public string app_id { get; set; }
        public Question(question form)
        {
            id = form.id;
            form_id = form.form_id;
            settings = form.settings;
            app_id = form.app_id;
        }
    }
}