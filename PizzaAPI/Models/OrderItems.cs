using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PizzaAPI.Models
{
    public class OrderItems
    {
        public string Items { get; set; }
        public int Order_ID { get; set; }
    }
}