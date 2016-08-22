using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ShowTime.Models;

namespace ShowTime.ViewModel
{
    public class PastShowVM
    {
        public PastShow pastshow { get; set; }

        public List<PastShow> pastshowlist { get; set; }
    }
}