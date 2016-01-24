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
                               CustomerName = d.Customer,
                               CustomerAddress = d.MainAddress
                           };
            return customers.ToList();
        }

        // add
        [Route("api/customer/add")]
        public int Post(Models.tblCustomer customer)
        {

            try
            {
                Data.tblCustomer newnCustomer = new Data.tblCustomer();

                newnCustomer.CustomerNumber = customer.CustomerNumber;
                newnCustomer.Customer = customer.CustomerName;
                newnCustomer.MainAddress = customer.CustomerAddress;

                db.tblCustomers.InsertOnSubmit(newnCustomer);
                db.SubmitChanges();

                return newnCustomer.Id;
            }
            catch
            {
                return 0;
            }
        }

        // update
        [Route("api/updateCustomer/{id}")]
        public HttpResponseMessage Put(String id, Models.tblCustomer customer)
        {
            try
            {
                var customerId = Convert.ToInt32(id);
                var customers = from d in db.tblCustomers where d.Id == customerId select d;

                if (customers.Any())
                {
                    var updateCustomer = customers.FirstOrDefault();

                    updateCustomer.CustomerNumber = customer.CustomerNumber;
                    updateCustomer.Customer = customer.CustomerName;
                    updateCustomer.MainAddress = customer.CustomerAddress;

                    db.SubmitChanges();

                    return Request.CreateResponse(HttpStatusCode.OK);
                }

                else
                {
                    return Request.CreateResponse(HttpStatusCode.NotFound);
                }
            }
            catch
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest);
            }
        }

        // delete product
        [Route("api/deleteCustomer/{id}")]
        public HttpResponseMessage Delete(String id)
        {
            try
            {
                var customerId = Convert.ToInt32(id);
                var customers = from d in db.tblCustomers where d.Id == customerId select d;

                if (customers.Any())
                {
                    db.tblCustomers.DeleteOnSubmit(customers.First());
                    db.SubmitChanges();

                    return Request.CreateResponse(HttpStatusCode.OK);
                }
                else
                {
                    return Request.CreateResponse(HttpStatusCode.NotFound);
                }

            }
            catch
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest);
            }
        }
    }
}
