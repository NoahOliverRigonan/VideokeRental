using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace VideokeRental.Controllers
{
    public class ApiCustomerDetailsReservedController : ApiController
    {
        private Data.videokerentalDataContext db = new Data.videokerentalDataContext();

        [Route("api/customerDetailsReserved/list")]
        public List<Models.tblCustomerDetailsReserved> Get()
        {

            var ustomerDetailsReserveds = from d in db.tblCustomerDetailsReserveds
                            select new Models.tblCustomerDetailsReserved
                            {
                                Id = d.Id,
                                CustomerId = d.CustomerId,
                                Customer = d.tblCustomer.Customer,
                                ReservedVideokeId = d.ReservedVideokeId,
                                ReservedVideoke = d.tblCalendarReservation.tblProduct.ProductName,
                                DateReserved = d.tblCalendarReservation.ReservedDate.ToShortDateString(),
                                PriceReserved = d.tblCalendarReservation.tblProduct.Price
                            };
            return ustomerDetailsReserveds.ToList();
        }
    }
}
