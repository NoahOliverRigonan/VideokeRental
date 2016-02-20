using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace VideokeRental.Models
{
    public class AspNetUsers
    {
        [Key]
        public Int32 Id { get; set; }
        public String FullName { get; set; }
        public String Email { get; set; }
        public String UserName { get; set; }
        public String Address { get; set; }
    }
}