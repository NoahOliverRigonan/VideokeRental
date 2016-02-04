using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace VideokeRental.Controllers
{
    public class ApiMainUserController : ApiController
    {
        private Data.videokerentalDataContext db = new Data.videokerentalDataContext();

        [Route("api/mainUser/list")]
        public List<Models.ApplicationUser> Get()
        {

            var mainUsers = from d in db.AspNetUsers
                            select new Models.ApplicationUser
                            {
                                Id = d.Id,
                                FullName = d.FullName,
                                Email = d.Email,
                               // EmailConfirmed = d.EmailConfirmed,
                                UserName = d.UserName,
                                PasswordHash = d.PasswordHash,
                                //SecurityStamp = d.SecurityStamp,
                                //PhoneNumber = d.PhoneNumber,
                                //PhoneNumberConfirmed = d.PhoneNumberConfirmed,
                                //TwoFactorEnabled = d.TwoFactorEnabled,
                                //LockoutEndDateUtc = d.LockoutEndDateUtc,
                                //LockoutEnabled = d.LockoutEnabled,
                                //AccessFailedCount = d.AccessFailedCount,
                            };
            return mainUsers.ToList();
        }

        // add
        [Route("api/mainUser/add")]
        public string Post(Models.AspNetUsers mainUser)
        {

            try
            {
                Data.AspNetUser newUser = new Data.AspNetUser();

                newUser.FullName = mainUser.FullName;
                newUser.Email = mainUser.Email;
                newUser.UserName = mainUser.UserName;

                db.AspNetUsers.InsertOnSubmit(newUser);
                db.SubmitChanges();

                return newUser.Id;
            }
            catch
            {
                return "";
            }
        }

        // update
        [Route("api/updateMainUser/{userId}")]
        public HttpResponseMessage Put(String userId, Models.ApplicationUser mainUser)
        {
            try
            {
                var mainUsers = from d in db.AspNetUsers where d.Id == userId select d;

                if (mainUsers.Any())
                {
                    var updateCustomer = mainUsers.FirstOrDefault();

                    updateCustomer.FullName = mainUser.FullName;
                    updateCustomer.Email = mainUser.Email;
                    updateCustomer.UserName = mainUser.UserName;
                    //updateCustomer.PasswordHash = mainUser.PasswordHash;

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


        // delete product
        [Route("api/deleteMainUser/{userId}")]
        public HttpResponseMessage Delete(String userId)
        {
            try
            {
                var mainUsers = from d in db.AspNetUsers where d.Id == userId select d;

                if (mainUsers.Any())
                {
                    db.AspNetUsers.DeleteOnSubmit(mainUsers.First());
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
