using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace VideokeRental.Controllers
{
    public class ApiVideokeReservationController : ApiController
    {
        private Data.videokerentaldbDataContext db = new Data.videokerentaldbDataContext();

        [Route("api/videokeReservation/list")]
        public List<Models.tblVideokeReservation> Get()
        {
            var videokeReservations = from d in db.tblVideokeReservations
                                      select new Models.tblVideokeReservation
                                      {
                                          Id = d.Id,
                                          ProductId = d.ProductId,
                                          Product = d.tblProduct.ProductName,
                                          ReserveByCustomerId = d.ReserveByCustomerId,
                                          ReserveByCustomer = d.tblCustomer.Customer,
                                          ReserveDateStart = d.ReserveDateStart.ToShortDateString(),
                                          ReserveDateEnd = d.ReserveDateEnd.ToShortDateString(),
                                          IsReserved = d.IsReserved,
                                          Price = d.tblProduct.Price,
                                          Street = d.tblCustomer.Street,
                                          Town = d.tblCustomer.Town,
                                          City = d.tblCustomer.City
                                      };
            return videokeReservations.ToList();
        }

        [Route("api/videokeReservation/listByDocumentId/{productId}")]
        public Models.tblVideokeReservation GetByDocumentId(String productId)
        {
            var vr_productId = Convert.ToInt32(productId);
            var videokeReservations = from d in db.tblVideokeReservations
                                      where d.ProductId == vr_productId
                                      select new Models.tblVideokeReservation
                                      {
                                          Id = d.Id,
                                          ProductId = d.ProductId,
                                          Product = d.tblProduct.ProductName,
                                          ReserveByCustomerId = d.ReserveByCustomerId,
                                          ReserveByCustomer = d.tblCustomer.Customer,
                                          ReserveDateStart = d.ReserveDateStart.ToShortDateString(),
                                          ReserveDateEnd = d.ReserveDateEnd.ToShortDateString(),
                                          IsReserved = d.IsReserved,
                                      };
            return (Models.tblVideokeReservation)videokeReservations.FirstOrDefault();
        }

        // add
        [Route("api/videokeReservation/add")]
        public int Post(Models.tblVideokeReservation videokeReservation)
        {
            try
            {
                Data.tblVideokeReservation newVideokeReservation = new Data.tblVideokeReservation();

                newVideokeReservation.ProductId = videokeReservation.ProductId;
                newVideokeReservation.ReserveByCustomerId = videokeReservation.ReserveByCustomerId;
                newVideokeReservation.ReserveDateStart = Convert.ToDateTime(videokeReservation.ReserveDateStart);
                newVideokeReservation.ReserveDateEnd = Convert.ToDateTime(videokeReservation.ReserveDateEnd);
                newVideokeReservation.IsReserved = true;
                newVideokeReservation.Opened = false;

                db.tblVideokeReservations.InsertOnSubmit(newVideokeReservation);
                db.SubmitChanges();

                Data.tblCalendarReservation newCalendarReservation = new Data.tblCalendarReservation();

                newCalendarReservation.CustomersIdReserved = videokeReservation.ReserveByCustomerId;
                newCalendarReservation.ReservedDate = Convert.ToDateTime(videokeReservation.ReserveDateStart);
                newCalendarReservation.ProductId = videokeReservation.ProductId;

                db.tblCalendarReservations.InsertOnSubmit(newCalendarReservation);
                db.SubmitChanges();

                return newVideokeReservation.Id;
            }
            catch (Exception e)
            {
                Debug.WriteLine(e);
                return 0;
            }
        }

        // delete
        [Route("api/deleteVideokeReservation/{productId}/{customerId}")]
        public HttpResponseMessage Delete(String productId, String customerId)
        {
            try
            {
                var vr_productId = Convert.ToInt32(productId);
                var vr_customerId = Convert.ToInt32(customerId);
                var videokeReservations = from d in db.tblVideokeReservations where d.ProductId == vr_productId && d.ReserveByCustomerId == vr_customerId select d;

                if (videokeReservations.Any())
                {
                    db.tblVideokeReservations.DeleteAllOnSubmit(videokeReservations);
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

        // rented
        [Route("api/rentalVideoke/{productId}")]
        public HttpResponseMessage PutProductRent(String productId, Models.tblCalendarRental calendarRental)
        {
            try
            {
                var vr_productId = Convert.ToInt32(productId);
                var videokeReservations = from d in db.tblProducts where d.Id == vr_productId select d;

                if (videokeReservations.Any())
                {
                    var updateVideokeReservation = videokeReservations.FirstOrDefault();

                    updateVideokeReservation.IsRented = true;
                    updateVideokeReservation.IsReserved = false;

                    db.SubmitChanges();

                    Data.tblCalendarRental newcalendarRental = new Data.tblCalendarRental();

                    newcalendarRental.RentedDate = Convert.ToDateTime(calendarRental.RentedDate);
                    newcalendarRental.ProductId = calendarRental.ProductId;
                    newcalendarRental.CustomersIdRented = calendarRental.CustomersIdRented;

                    db.tblCalendarRentals.InsertOnSubmit(newcalendarRental);
                    db.SubmitChanges();

                    Data.tblVideokeIncome newVideokeIncome = new Data.tblVideokeIncome();

                    var productPrice = (from d in db.tblProducts where d.Id == vr_productId select d.Price).SingleOrDefault();

                    newVideokeIncome.DateRent = Convert.ToDateTime(calendarRental.RentedDate);
                    newVideokeIncome.ProductId = calendarRental.ProductId;
                    newVideokeIncome.CustomerId = calendarRental.CustomersIdRented;
                    newVideokeIncome.Price = productPrice;

                    db.tblVideokeIncomes.InsertOnSubmit(newVideokeIncome);
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

        // rented
        [Route("api/returnVideoke/{productId}")]
        public HttpResponseMessage PutProductReturned(String productId)
        {
            try
            {
                var vr_productId = Convert.ToInt32(productId);
                var videokeReservations = from d in db.tblProducts where d.Id == vr_productId select d;

                if (videokeReservations.Any())
                {
                    var updateVideokeReservation = videokeReservations.FirstOrDefault();

                    updateVideokeReservation.IsRented = false;
                    updateVideokeReservation.IsReserved = false;

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
