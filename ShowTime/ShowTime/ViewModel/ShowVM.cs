using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ShowTime.Models;

namespace ShowTime.ViewModel
{
    public class ShowVM
    {
        public Show shows { get; set; }

        public List<Show> showlist { get; set; }
    }
}