using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace VideokeRental.Models
{
    public class tblCalendarRental
    {
        [Key]
        public Int32 Id { get; set; }
        public Int32 CustomersIdRented { get; set; }
        public Int32 ProductId { get; set; }
        public String Product { get; set; }
        public String CustomersRent { get; set; }
        public String RentedDate { get; set; }
        public String Address { get; set; }
        public Decimal Price { get; set; }
        public String VideokeDescription { get; set; }
    }
}