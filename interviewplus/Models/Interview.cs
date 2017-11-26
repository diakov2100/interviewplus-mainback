using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace interviewplus.Models
{
    public class Interview
    {
        public int id { get; set; }
        public string uid { get; set; }

        public int interviewee_id { get; set; }
        public int form_id { get; set; }
        public Interview(interview interview)
        {
            uid = interview.uid;
            id = interview.id;
            interviewee_id = interview.interviewee_id;
            form_id = interview.form_id;
        }
    }
    public class FullInterview : Interview
    {
        public string name { get; set; }
        public FullInterview(interview interview) : base(interview)
        {
            name = interview.interviewee.name;
        }
    }
}