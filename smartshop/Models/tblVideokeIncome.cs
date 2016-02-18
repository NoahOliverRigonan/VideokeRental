using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace VideokeRental.Models
{
    public class tblVideokeIncome
    {
        [Key]
        public Int32 Id { get; set; }
        public Int32 ProductId { get; set; }
        public String Product { get; set; }
        public Int32 CustomerId { get; set; }
        public String Customer { get; set; }
        public Decimal Price { get; set; }
        public String DateRent { get; set; }
    }
}