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
        private Data.videokerentalDataContext db = new Data.videokerentalDataContext();

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
                 
                db.tblVideokeReservations.InsertOnSubmit(newVideokeReservation);
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
    }
}
