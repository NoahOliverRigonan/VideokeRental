using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace VideokeRental.Models
{
    public class tblProduct
    {
        [Key]
        public Int32 Id { get; set; }
        public String ProductNumber { get; set; }
        public String ProductName { get; set; }
        public String ProductDescription { get; set; }
        public String Images { get; set; }
        public Byte ImagesByte { get; set; }
    }
}