using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace VideokeRental.Models
{
    public class tblVideokeRent
    {
        [Key]
        public Int32 Id { get; set; }
        public Int32 ProductId { get; set; }
        public String Product { get; set; }
        public Int32 RentByCustomerId { get; set; }
        public String RentByCustomer { get; set; }
        public String RentDateStart { get; set; }
        public String RentDateEnd { get; set; }
        public Boolean IsRented { get; set; }
        public Boolean IsOpened { get; set; }
        public Int32 ReferenceNumber { get; set; }
        public Decimal Price { get; set; }
        public String Street { get; set; }
        public String Town { get; set; }
        public String City { get; set; }

    }
}