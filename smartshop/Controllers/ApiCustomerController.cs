using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace VideokeRental.Controllers
{
    public class ApiCustomerController : ApiController
    {
        private Data.videokerentaldbDataContext db = new Data.videokerentaldbDataContext();

        [Route("api/customer/list")]
        public List<Models.tblCustomer> Get()
        {

            var customers = from d in db.tblCustomers
                           select new Models.tblCustomer
                           {
                               Id = d.Id,
                               CustomerNumber = d.CustomerNumber,
                               CustomerName = d.Customer,
                               ContactNumber = d.ContactNumber,
                               Street = d.Street,
                               Town = d.Town,
                               City = d.City
                           };
            return customers.ToList();
        }

        [Route("api/customer/listLastCustomerNumber")]
        public Models.tblCustomer GetLastCustomerNumber()
        {

            var customers = from d in db.tblCustomers.OrderByDescending(d => d.CustomerNumber)
                            select new Models.tblCustomer
                            {
                                Id = d.Id,
                                UserId = d.UserId,
                                CustomerNumber = d.CustomerNumber,
                                CustomerName = d.Customer,
                                ContactNumber = d.ContactNumber,
                                Street = d.Street,
                                Town = d.Town,
                                City = d.City
                            };
            return (Models.tblCustomer)customers.FirstOrDefault();
        }

        [Route("api/customer/listCustomerByUserId/{userId}")]
        public Models.tblCustomer GetCustomerByUserId(String userId)
        {

            var customers = from d in db.tblCustomers
                            where d.UserId == userId
                            select new Models.tblCustomer
                            {
                                Id = d.Id,
                                UserId = d.UserId,
                                CustomerNumber = d.CustomerNumber,
                                CustomerName = d.Customer,
                                ContactNumber = d.ContactNumber,
                                Street = d.Street,
                                Town = d.Town,
                                City = d.City
                            };
            return (Models.tblCustomer)customers.FirstOrDefault();
        }

        [Route("api/customer/listCustomerById/{id}")]
        public Models.tblCustomer GetCustomerById(String id)
        {
            var customerId = Convert.ToInt32(id);
            var customers = from d in db.tblCustomers
                            where d.Id == customerId
                            select new Models.tblCustomer
                            {
                                Id = d.Id,
                                UserId = d.UserId,
                                CustomerNumber = d.CustomerNumber,
                                CustomerName = d.Customer,
                                ContactNumber = d.ContactNumber,
                                Street = d.Street,
                                Town = d.Town,
                                City = d.City
                            };
            return (Models.tblCustomer)customers.FirstOrDefault();
        }

        // add
        [Route("api/customer/add")]
        public int Post(Models.tblCustomer customer)
        {

            try
            {
                Data.tblCustomer newCustomer = new Data.tblCustomer();

                newCustomer.CustomerNumber = customer.CustomerNumber;
                newCustomer.Customer = customer.CustomerName;
                newCustomer.ContactNumber = customer.ContactNumber;
                newCustomer.Street = customer.Street;
                newCustomer.Town = customer.Town;
                newCustomer.City = customer.City;
                newCustomer.Email = customer.Email;


                db.tblCustomers.InsertOnSubmit(newCustomer);
                db.SubmitChanges();

                return newCustomer.Id;
            }
            catch(Exception e)
            {
                Debug.WriteLine(e);
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

                var userId = (from d in db.tblCustomers where d.Id == customerId select d.UserId).SingleOrDefault();
                var ASPusers = from d in db.AspNetUsers where d.Id == userId select d;
                if (customers.Any())
                {
                    var updateCustomer = customers.FirstOrDefault();

                    updateCustomer.CustomerNumber = customer.CustomerNumber;
                    updateCustomer.Customer = customer.CustomerName;
                    updateCustomer.ContactNumber = customer.ContactNumber;
                    updateCustomer.Street = customer.Street;
                    updateCustomer.Town = customer.Town;
                    updateCustomer.City = customer.City;

                    db.SubmitChanges();

                    if (ASPusers.Any())
                    {
                        var updateUser = ASPusers.FirstOrDefault();
                        updateUser.FullName = customer.CustomerName;

                        db.SubmitChanges();
                    }

                    return Request.CreateResponse(HttpStatusCode.OK);
                }

                else
                {
                    return Request.CreateResponse(HttpStatusCode.NotFound);
                }
            }
            catch(Exception e)
            {
                Debug.WriteLine(e);
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
            catch(Exception e)
            {
                Debug.WriteLine(e);
                return Request.CreateResponse(HttpStatusCode.BadRequest);
            }
        }
    }
}
