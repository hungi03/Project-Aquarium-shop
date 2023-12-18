using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CodeFirst_LaptrinhWeb.Models;
using Diacritics;
using Diacritics.Extensions;

namespace LapTrinhWeb.Controllers
{
    public class ProductsController : Controller
    {
        // GET: Products
        public ActionResult Index(string search = "", string sortOrder = "", int page = 1)
        {
            MyDBContext db = new MyDBContext();

            List<SANPHAMCACANH> sp = db.SANPHAMCACANHs.Where(x => x.TenSanPham.Contains(search)).ToList();


            string searchWithoutDiacritics = search.RemoveDiacritics();


            ViewBag.Search = search;

            //Liệt kê loại
            List<LOAICA> loai = db.LOAICAs.ToList();
            ViewBag.Loai = loai;

            //Cá nước măn id=1
            List<SANPHAMCACANH> canuocman = db.SANPHAMCACANHs.Where(x => x.LoaiCaID == 1).ToList();
            ViewBag.Canuocman = canuocman;
            //Cá nước măn id=1
            List<SANPHAMCACANH> canuocngot = db.SANPHAMCACANHs.Where(x => x.LoaiCaID == 2).ToList();
            ViewBag.Canuocngot = canuocngot;
            //Cá nước măn id=1
            List<SANPHAMCACANH> tomcanh = db.SANPHAMCACANHs.Where(x => x.LoaiCaID == 3).ToList();
            ViewBag.Tomcanh = tomcanh;
            //Cá nước măn id=1
            List<SANPHAMCACANH> cuacanh = db.SANPHAMCACANHs.Where(x => x.LoaiCaID == 4).ToList();
            ViewBag.Cuacanh = cuacanh;
            //Cá nước măn id=1
            List<SANPHAMCACANH> sanho = db.SANPHAMCACANHs.Where(x => x.LoaiCaID == 5).ToList();
            ViewBag.Sanho = sanho;
            //Cá nước măn id=1
            List<SANPHAMCACANH> haiquy = db.SANPHAMCACANHs.Where(x => x.LoaiCaID == 6).ToList();
            ViewBag.Haiquy = haiquy;
            //Cá nước măn id=1
            List<SANPHAMCACANH> beca = db.SANPHAMCACANHs.Where(x => x.LoaiCaID == 7).ToList();
            ViewBag.Beca = beca;
            //Cá nước măn id=1
            List<SANPHAMCACANH> phukien = db.SANPHAMCACANHs.Where(x => x.LoaiCaID == 8).ToList();
            ViewBag.Phukien = phukien;

            if (!string.IsNullOrEmpty(sortOrder))
            {
                //Sắp xếp
                switch (sortOrder)
                {
                    case "a-z":
                        sp = db.SANPHAMCACANHs.OrderBy(x => x.TenSanPham).ToList();
                        break;

                    case "z-a":
                        sp = db.SANPHAMCACANHs.OrderByDescending(x => x.TenSanPham).ToList();
                        break;

                    case "thap-cao":
                        sp = db.SANPHAMCACANHs.OrderBy(x => x.Gia).ToList();
                        break;

                    case "cao-thap":
                        sp = db.SANPHAMCACANHs.OrderByDescending(x => x.Gia).ToList();
                        break;

                    default:
                        break;
                }
            }

            ViewBag.SortOrder = sortOrder;

            //Paging
            int NoOfRecordPerPage = 8;
            int NoOfPages = Convert.ToInt32(Math.Ceiling
                (Convert.ToDouble(sp.Count) / Convert.ToDouble(NoOfRecordPerPage)));
            int NoOfRecordToSkip = (page - 1) * NoOfRecordPerPage;
            ViewBag.Page = page;
            ViewBag.NoOfPages = NoOfPages;
            sp = sp.Skip(NoOfRecordToSkip).Take(NoOfRecordPerPage).ToList();



            return View(sp);
        }

        public ActionResult Details(int? id)
        {
            MyDBContext db = new MyDBContext();
            SANPHAMCACANH sp = db.SANPHAMCACANHs.Where(x => x.SanPhamID == id).FirstOrDefault();

            List<SANPHAMCACANH> listsp = db.SANPHAMCACANHs.Where(x => x.LoaiCaID == sp.LoaiCaID)
                                                            .Take(4)
                                                            .ToList();
            ViewBag.listsp = listsp;

            return View(sp);
        }
        public ActionResult Info()
        {
            return View();
        }
        public ActionResult Login()
        {
            return View();
        }
        public ActionResult Register()
        {
            return View();
        }
        public ActionResult Cart()
        {
            return View();
        }


    }

}