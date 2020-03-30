using ItemMenu;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PizzaAPI.Models
{
    public class CompleteOrder
    {
        public OrderInfo NewOrderInfo { get; set; }
        public List<OrderInfoItem> OrderList { get; set; }
    }
}