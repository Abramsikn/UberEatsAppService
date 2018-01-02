using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using UberEatsAppService.Models;

namespace MyAppServices.Controllers
{
    public class ProductController : ApiController
    {
        static DataAccess dataA = new DataAccess();

        //Get all the restaurants information stored in the database
        [System.Web.Http.HttpGet]
        [System.Web.Http.Route("api/Products")]
        public IEnumerable<Product> GetAllProds()
        {
            return dataA.GetProduct();
        }
    }
}