using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace VideokeRental.Controllers
{
    public class ApiVideokeIncomeController : ApiController
    {
        private Data.videokerentalDataContext db = new Data.videokerentalDataContext();

        [Route("api/videokeIncome/list")]
        public List<Models.tblVideokeIncome> Get()
        {

            var videokeIncomes = from d in db.tblVideokeIncomes
                            select new Models.tblVideokeIncome
                            {
                                Id = d.Id,
                                ProductId = d.ProductId,
                                Product = d.tblProduct.ProductName,
                                CustomerId = d.CustomerId,
                                Customer = d.tblCustomer.Customer,
                                Price = d.Price,
                                DateRent =  d.DateRent.ToShortDateString(),
                            };
            return videokeIncomes.ToList();
        }


        //  date 
        [Route("api/videokeIncome/{date}")]
        public List<Models.tblVideokeIncome> GetVideokeIncome(String date)
        {
            var videokeIncome_date = Convert.ToDateTime(date);
            var videokeIncomes = from d in db.tblVideokeIncomes
                                 where d.DateRent == videokeIncome_date
                                  select new Models.tblVideokeIncome
                                  {
                                      Id = d.Id,
                                      ProductId = d.ProductId,
                                      Product = d.tblProduct.ProductName,
                                      CustomerId = d.CustomerId,
                                      Customer = d.tblCustomer.Customer,
                                      Price = d.Price,
                                      DateRent = d.DateRent.ToShortDateString(),
                                  };
            return videokeIncomes.ToList();
        }
    }
}
