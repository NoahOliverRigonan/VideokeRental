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
    }
}
