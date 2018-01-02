using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using UberEatsAppService.Models;

namespace UberEatsAppService.Controllers
{
    public class CustomerController : System.Web.Http.ApiController
    {
        static DataAccess dataA = new DataAccess();

        //Registering customers on the mobile application
        [System.Web.Http.HttpPost]
        [System.Web.Http.Route("api/Register")]
        public string PostCust(Customer cust)
        {
            if (cust != null)
            {
                return dataA.RegisterCustomer(cust);
            }
            return "Unable to add";
        }

        //Login into the system
        [System.Web.Http.HttpGet]
        [System.Web.Http.Route("api/CustomerLogginIn")]
        public Customer GetCust(string email, string password)
        {
            return dataA.CustomerLogin(email, password);
        }

        //Get all Customers information stored in the database
        [HttpGet]
        [Route("api/GetCustomers")]
        public IEnumerable<Customer> GetAllCustomers()
        {
            return dataA.GetAllCust();
        }

        //Updating the customer's information
        [System.Web.Http.HttpPut]
        [System.Web.Http.Route("api/UpdateCustomer")]
        public Customer UpdateCust(Customer cust, int id)
        {
            return dataA.CustomerUpdate(cust, id);
        }
    }
}
