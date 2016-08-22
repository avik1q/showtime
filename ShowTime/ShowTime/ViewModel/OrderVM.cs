using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ShowTime.Models;

namespace ShowTime.ViewModel
{
    public class OrderVM
    {
        Order order { get; set; }

        public List<Order> orderlist { get; set; }
    }
}