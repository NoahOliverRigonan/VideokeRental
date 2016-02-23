using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace VideokeRental.Models
{
    public class tblCustomer
    {
        [Key]
        public Int32 Id { get; set; }
        public String UserId { get; set; }
        public String CustomerNumber { get; set; }
        public String CustomerName { get; set; }
        public String Street { get; set; }
        public String Town { get; set; }
        public String City { get; set; }
    }
}