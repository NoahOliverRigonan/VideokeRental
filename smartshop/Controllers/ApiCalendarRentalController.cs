using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace VideokeRental.Controllers
{
    public class ApiCalendarRentalController : ApiController
    {
        private Data.videokerentalDataContext db = new Data.videokerentalDataContext();

        // list
        [Route("api/calendarRental/list")]
        public List<Models.tblCalendarRental> Get()
        {

            var calendarRentals = from d in db.tblCalendarRentals
                                  select new Models.tblCalendarRental
                                  {
                                      Id = d.Id,
                                      CustomersIdRented = d.CustomersIdRented,
                                      CustomersRent = d.tblCustomer.Customer,
                                      ProductId = d.ProductId,
                                      Product = d.tblProduct.ProductName,
                                      Address = d.tblCustomer.MainAddress,
                                      RentedDate = d.RentedDate.ToShortDateString(),
                                  };
            return calendarRentals.ToList();
        }

        //  date 
        [Route("api/getCalendarRental/{date}")]
        public List<Models.tblCalendarRental> GetCalendarRental(String date)
        {
            var calendarRental_date = Convert.ToDateTime(date);
            var calendarRentals = from d in db.tblCalendarRentals
                                  where d.RentedDate == calendarRental_date
                                  select new Models.tblCalendarRental
                                      {
                                          Id = d.Id,
                                          CustomersIdRented = d.CustomersIdRented,
                                          CustomersRent = d.tblCustomer.Customer,
                                          ProductId = d.ProductId,
                                          Product = d.tblProduct.ProductName,
                                          RentedDate = d.RentedDate.ToShortDateString(),
                                      };
            return calendarRentals.ToList();
        }

        //  customer Id 
        [Route("api/getCalendarRentalByCustomerId/{customerId}")]
        public List<Models.tblCalendarRental> GetCalendarRentalByCustomerId(String customerId)
        {
            var calendarRental_customerId = Convert.ToInt32(customerId);
            var calendarRentals = from d in db.tblCalendarRentals
                                  where d.CustomersIdRented == calendarRental_customerId
                                  group d by new 
                                  {
                                      ProductId = d.ProductId,
                                      Product = d.tblProduct.ProductName,
                                      VideokeDescription = d.tblProduct.ProductDescription
                                  } into g
                                  select new Models.tblCalendarRental
                                  {
                                      ProductId = g.Key.ProductId,
                                      Product = g.Key.Product,
                                      VideokeDescription = g.Key.VideokeDescription
                                  };
            return calendarRentals.ToList();
        }
    }
}
