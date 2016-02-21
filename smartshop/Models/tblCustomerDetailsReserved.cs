using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace VideokeRental.Models
{
    public class tblCustomerDetailsReserved
    {
        [Key]
        public Int32 Id { get; set; }
        public Int32 CustomerId { get; set; }
        public String Customer { get; set; }
        public Int32 ReservedVideokeId { get; set; }
        public String ReservedVideoke { get; set; }
        public String DateReserved { get; set; }
        public Decimal PriceReserved { get; set; }

    }
}