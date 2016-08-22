using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ShowTime.Models;

namespace ShowTime.BindModel
{
    public class ShowsOverView
    {
        public List<PastShow> pastshowlist { get; set; }

        public List<Show> showlist { get; set; }
    }
}