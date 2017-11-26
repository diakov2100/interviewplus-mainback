using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace interviewplus.Models
{
    public class Answer
    {
        public int interview_id { get; set; }
        public int question_id { get; set; }
        public string answer1 { get; set; }
        public string type { get; set; }
        public string video { get; set; }
        public string audio { get; set; }

        public Answer(answer answer)
        {
            interview_id = answer.interview_id;
            question_id = answer.question_id;
            answer1 = answer.answer1;
            type = answer.type;
            video = answer.video;
            audio = answer.audio;
        }
    }
}