using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace interviewplus.Models
{
    public class App
    {
        public string name { get; set; }
        public string settings { get; set; }
        public string image { get; set; }
        public App(app app)
        {
            name = app.name;
            settings = app.settings;
            image = app.settings;
        }

    }
}