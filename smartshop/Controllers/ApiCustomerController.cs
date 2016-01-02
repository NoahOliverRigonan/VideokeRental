using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace VideokeRental.Controllers
{
    public class ApiCustomerController : ApiController
    {
        private Data.videokerentalDataContext db = new Data.videokerentalDataContext();

        [Route("api/customer/list")]
        public List<Models.tblCustomer> Get()
        {

            var customers = from d in db.tblCustomers
                           select new Models.tblCustomer
                           {
                               Id = d.Id,
                               CustomerNumber = d.CustomerNumber,
                               CustomerName = d.CustomerName,
                               CustomerAddress = d.CustomerAddress
                           };
            return customers.ToList();
        }
    }
}
