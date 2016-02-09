using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace VideokeRental.Models
{
    public class tblVideokeReservation
    {

        [Key]
        public Int32 Id { get; set; }
        public Int32 ProductId { get; set; }
        public String Product { get; set; }
        public Int32 ReserveByCustomerId { get; set; }
        public String ReserveByCustomer { get; set; }
        public String ReserveDateStart { get; set; }
        public String ReserveDateEnd { get; set; }
        public Boolean IsReserved { get; set; }
    }
}