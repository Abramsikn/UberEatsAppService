using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using UberEatsAppService.Models;

namespace UberEatsAppService.Controllers
{
    public class OrderController : System.Web.Http.ApiController
    {
        static DataAccess dataA = new DataAccess();

        //Add order to the database
        [System.Web.Http.HttpPost]
        [System.Web.Http.Route("api/Order")]
        public string Order(Order ord)
        {
            return dataA.Orders(ord);
        }
    }
}
