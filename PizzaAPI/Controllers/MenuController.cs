using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Data.Entity;
using ItemMenu;
using System.Data.Entity.Core.Objects;

namespace PizzaAPI.Controllers
{
    public class MenuController : ApiController
    {
        public IEnumerable<vwItems> Get()
        {
            using(MenuDBEntities entities = new MenuDBEntities())
            {
                List<vwItems> returnList = entities.vwItems.ToList();
                for(int i = 0; i < returnList.Count(); i++)
                {
                    returnList[i].Name = returnList[i].Name.Trim();
                    returnList[i].Type = returnList[i].Type.Trim();
                }
                return returnList;
            }
        }

        public IEnumerable<string> Get(int id)
        {
            using (MenuDBEntities entities = new MenuDBEntities())
            {
                ObjectResult<GetIngrediens_Result> temp = entities.GetIngrediens(id);
                var list = new List<GetIngrediens_Result>();
                list = (from a in temp select a).ToList();
                List<string> returnlist = new List<string>();
                for (int i = 0; i < list.Count(); i++)
                {
                    returnlist.Add(list[i].Ingrediens.Trim());
                }
                return returnlist;
            }
        }
    }
}
