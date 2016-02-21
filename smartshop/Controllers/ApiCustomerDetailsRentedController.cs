using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace VideokeRental.Controllers
{
    public class ApiCustomerDetailsRentedController : ApiController
    {
        private Data.videokerentalDataContext db = new Data.videokerentalDataContext();

        [Route("api/customerDetailsRented/list")]
        public List<Models.tblCustomerDetailsRented> Get()
        {

            var customerDetailsRenteds = from d in db.tblCustomerDetailsRenteds
                                         select new Models.tblCustomerDetailsRented
                                         {
                                             Id = d.Id,
                                             CustomerId = d.CustomerId,
                                             Customer = d.tblCustomer.Customer,
                                             RentedVideokeId = d.RentedVideokeId,
                                             RentedVideoke = d.tblCalendarRental.tblProduct.ProductName,
                                             DateRented = d.tblCalendarRental.RentedDate.ToShortDateString(),
                                             PriceRented = d.tblCalendarRental.tblProduct.Price
                                         };
            return customerDetailsRenteds.ToList();
        }
    }
}
