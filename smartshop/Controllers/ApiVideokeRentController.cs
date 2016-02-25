using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace VideokeRental.Controllers
{
    public class ApiVideokeRentController : ApiController
    {
        private Data.videokerentalDataContext db = new Data.videokerentalDataContext();

        [Route("api/videokeRent/list")]
        public List<Models.tblVideokeRent> Get()
        {

            var videokeRents = from d in db.tblVideokeRents
                                      select new Models.tblVideokeRent
                                      {
                                          Id = d.Id,
                                          ProductId = d.ProductId,
                                          Product = d.tblProduct.ProductName,
                                          RentByCustomerId = d.RentByCustomerId,
                                          RentByCustomer = d.tblCustomer.Customer,
                                          RentDateStart = d.RentDateStart.ToShortDateString(),
                                          RentDateEnd = d.RentDateEnd.ToShortDateString(),
                                          IsRented = d.IsRented,
                                          Price = d.tblProduct.Price,
                                          Street = d.tblCustomer.Street,
                                          Town = d.tblCustomer.Town,
                                          City = d.tblCustomer.City
                                      };
            return videokeRents.ToList();
        }

        [Route("api/videokeRent/listByDocumentId/{productId}")]
        public Models.tblVideokeRent GetByDocumentId(String productId)
        {
            var vr_productId = Convert.ToInt32(productId);
            var videokeRents = from d in db.tblVideokeRents
                                      where d.ProductId == vr_productId
                                      select new Models.tblVideokeRent
                                      {
                                          Id = d.Id,
                                          ProductId = d.ProductId,
                                          Product = d.tblProduct.ProductName,
                                          RentByCustomerId = d.RentByCustomerId,
                                          RentByCustomer = d.tblCustomer.Customer,
                                          RentDateStart = d.RentDateStart.ToShortDateString(),
                                          RentDateEnd = d.RentDateEnd.ToShortDateString(),
                                          IsRented = d.IsRented,
                                          Price = d.tblProduct.Price,
                                          Street = d.tblCustomer.Street,
                                          Town = d.tblCustomer.Town,
                                          City = d.tblCustomer.City
                                      };
            return (Models.tblVideokeRent)videokeRents.FirstOrDefault();
        }
    }
}
