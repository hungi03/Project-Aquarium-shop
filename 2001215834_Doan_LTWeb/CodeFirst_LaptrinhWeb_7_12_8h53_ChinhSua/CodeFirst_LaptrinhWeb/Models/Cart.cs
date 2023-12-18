using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
namespace CodeFirst_LaptrinhWeb.Models
{
    public class Cart
    {
        [Key]
        public int CartID { get; set; }
        public int SanPhamID { get; set; }
        public int Quantity { get; set; }
        public virtual SANPHAMCACANH SANPHAMCACANH { get; set; }
    }
}