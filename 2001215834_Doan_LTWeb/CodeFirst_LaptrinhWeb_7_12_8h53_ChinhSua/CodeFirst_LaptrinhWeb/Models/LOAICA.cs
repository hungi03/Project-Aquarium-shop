using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace CodeFirst_LaptrinhWeb.Models
{
    public class LOAICA
    {
        [Key]
        public int  LoaiCaID { get; set; }
        public string TenLoaiCa { get; set; }
        
        public virtual ICollection<SANPHAMCACANH> SANPHAMCACANHs { get; set; }

    }
}