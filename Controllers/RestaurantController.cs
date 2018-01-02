using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using UberEatsAppService.Models;

namespace UberEatsAppService.Controllers
{
    public class RestaurantController : System.Web.Http.ApiController
    {
        static DataAccess dataA = new DataAccess();

        //Get all the restaurants information stored in the database
        [HttpGet]
        [Route("api/GetRestaurant")]
        public IEnumerable<Restaurant> GetAllRestaurants()
        {
            return dataA.GetAllRest();
        }
    }
}
