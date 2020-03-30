using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Data.Entity;
using ItemMenu;
using System.Data.Entity.Core.Objects;
using PizzaAPI.Models;

namespace PizzaAPI.Controllers
{
    public class AdminController : ApiController
    {
        public IEnumerable<GetUnDoneOrders_Result> Get()
        {
            using (MenuDBEntities entities = new MenuDBEntities())
            {
                ObjectResult<GetUnDoneOrders_Result> temp = entities.GetUnDoneOrders();
                var list = new List<GetUnDoneOrders_Result>();
                list = (from a in temp select a).ToList();
                
                return list;
            }
        }

        public void Post([FromBody]int value)
        {
            using (MenuDBEntities entities = new MenuDBEntities())
            {
                entities.OrderInfo.Find(value).Status = "done";
                entities.SaveChanges();
            }
        }


    }
}
