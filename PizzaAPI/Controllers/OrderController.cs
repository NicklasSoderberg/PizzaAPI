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
    public class OrderController : ApiController
    {
        public string Get()
        {
            return "error";
        }

        public string Get(int orderID)
        {
            using (MenuDBEntities entities = new MenuDBEntities())
            {
                List<string> temp = entities.GetOrdersStatus(orderID).ToList();
                if(temp.Count == 0)
                {
                    return "error";
                }
                else
                {
                  return temp[0].Trim();
                }
            }
        }

        public OrderItems Get(string PhoneNumber)
        {
            using (MenuDBEntities entities = new MenuDBEntities())
            {
                ObjectResult<GetOrdersByPhone_Result> temp = entities.GetOrdersByPhone(PhoneNumber);
                var list = new List<GetOrdersByPhone_Result>();
                List<OrderItems> returnList = new List<OrderItems>();
                int curIndex = -1;
                int curID = 0;
                list = (from a in temp select a).ToList();
                if (list.Count == 0)
                {
                    return new OrderItems { Items = "", Order_ID = 0 };
                }
                for (int i = 0; i < list.Count(); i++)
                {
                    if(curID != list[i].ID_OrderInfo)
                    {
                        curID = list[i].ID_OrderInfo;
                        returnList.Add(new OrderItems { Order_ID = curID, Items = "" });
                        curIndex++;
                    }
                    if (returnList[curIndex].Items.Length > 0)
                    {
                        returnList[curIndex].Items += ", " + list[i].Name.Trim();
                    }
                    else
                    {
                        returnList[curIndex].Items += list[i].Name.Trim();
                    }

                }
                return returnList.Last();
            }
        }

        public void Post([FromBody]CompleteOrder value)
        {
            using (MenuDBEntities entities = new MenuDBEntities())
            {
                entities.OrderInfo.Add(value.NewOrderInfo);
                foreach(OrderInfoItem element in value.OrderList)
                {
                    entities.OrderInfoItem.Add(new OrderInfoItem {ID_Item = element.ID_Item, ID_OrderInfo = entities.OrderInfo.Count() + 1});
                }
                entities.SaveChanges();
            }
        }
    }
}
