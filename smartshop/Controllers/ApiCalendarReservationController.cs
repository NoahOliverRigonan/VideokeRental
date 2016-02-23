using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace VideokeRental.Controllers
{
    public class ApiCalendarReservationController : ApiController
    {
        private Data.videokerentalDataContext db = new Data.videokerentalDataContext();

        // list
        [Route("api/calendarReservation/list")]
        public List<Models.tblCalendarReservation> Get()
        {

            var calendarResevations = from d in db.tblCalendarReservations
                                      select new Models.tblCalendarReservation
                                      {
                                          Id = d.Id,
                                          CustomersIdReserved = d.CustomersIdReserved,
                                          ProductId = d.ProductId,
                                          Product = d.tblProduct.ProductName,
                                          CustomersReserved = d.tblCustomer.Customer,
                                          ReservedDate = d.ReservedDate.ToShortDateString(),
                                      };
            return calendarResevations.ToList();
        }

        // date
        [Route("api/getCalendarResevation/{date}")]
        public List<Models.tblCalendarReservation> GetCalendarReservation(String date)
        {
            var calendarResevation_date = Convert.ToDateTime(date);
            var calendarResevations = from d in db.tblCalendarReservations
                                      where d.ReservedDate == calendarResevation_date
                                      select new Models.tblCalendarReservation
                                      {
                                          Id = d.Id,
                                          CustomersIdReserved = d.CustomersIdReserved,
                                          ProductId = d.ProductId,
                                          Product = d.tblProduct.ProductName,
                                          CustomersReserved = d.tblCustomer.Customer,
                                          ReservedDate = d.ReservedDate.ToShortDateString(),
                                      };
            return calendarResevations.ToList();
        }

        // customer Id
        [Route("api/getCalendarResevationByCustomerId/{customerId}")]
        public List<Models.tblCalendarReservation> GetCalendarReservationByCustomerId(String customerId)
        {
            var calendarResevation_customerId = Convert.ToInt32(customerId);
            var calendarResevations = from d in db.tblCalendarReservations
                                      where d.CustomersIdReserved == calendarResevation_customerId
                                      group d by new
                                      {
                                          ProductId = d.ProductId,
                                          Product = d.tblProduct.ProductName,
                                          VideokeDescription = d.tblProduct.ProductDescription
                                      } into g
                                      select new Models.tblCalendarReservation
                                      {
                                          ProductId = g.Key.ProductId,
                                          Product = g.Key.Product,
                                          VideokeDescription = g.Key.VideokeDescription
                                      };
            return calendarResevations.ToList();
        }

    }
}
