using VideokeRental.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace VideokeRental.Controllers
{
    public class SoftwareController : UserAccountController {
        
        [Authorize]
        // GET: Software
        public ActionResult Index()
        {
            return View();
        }
        [Authorize]
        // GET: Software
        public ActionResult Product()
        {
            return View();
        }
        [Authorize]
        // GET: Software
        public ActionResult Customer()
        {
            return View();
        }
        [Authorize]
        // GET: Software
        public ActionResult Users()
        {
            return View();
        }
        [Authorize]
        // GET: Software
        public ActionResult Message()
        {
            return View();
        }
        [Authorize]
        // GET: Software
        public ActionResult Calendar()
        {
            return View();
        }
        [Authorize]
        // GET: Software
        public ActionResult Reports()
        {
            return View();
        }
        [Authorize]
        // GET: Software
        public ActionResult VideokeDetails()
        {
            return View();
        }
    }
}