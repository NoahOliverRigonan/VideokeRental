using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace VideokeRental.Models
{
    public class tblCustomerDetailsRented
    {
        [Key]
        public Int32 Id { get; set; }
        public Int32 CustomerId { get; set; }
        public String Customer { get; set; }
        public Int32 RentedVideokeId { get; set; }
        public String RentedVideoke { get; set; }
        public String DateRented { get; set; }
        public Decimal PriceRented { get; set; }
    }
}