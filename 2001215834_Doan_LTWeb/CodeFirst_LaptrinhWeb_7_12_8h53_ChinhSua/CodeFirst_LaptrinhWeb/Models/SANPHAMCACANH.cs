using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace CodeFirst_LaptrinhWeb.Models
{
    public class SANPHAMCACANH
    {
        [Key]
        public int SanPhamID { get; set; }
        [Required]
        public string TenSanPham { get; set; }
        [Required]
        public float Gia { get; set; }

        [Required]
        public int LoaiCaID { get; set; }
        public string HinhAnh { get; set; }
        public string MoTa1 { get; set; }
        public string MoTa2 { get; set; }
        public string MoTa3 { get; set; }
        public string MoTa { get; set; }
        public virtual LOAICA LOAICA { get; set; }
    }
}