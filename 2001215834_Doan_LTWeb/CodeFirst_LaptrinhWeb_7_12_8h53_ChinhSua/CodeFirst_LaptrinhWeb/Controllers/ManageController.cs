using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CodeFirst_LaptrinhWeb.Models;
using Diacritics;
using Diacritics.Extensions;
namespace CodeFirst_LaptrinhWeb.Controllers
{
    public class ManageController : Controller
    {
        // GET: Manage
        public ActionResult Index(string search = "", int page = 1)
        {
            MyDBContext db = new MyDBContext();
       

            List<SANPHAMCACANH> sp = db.SANPHAMCACANHs.Where(x => x.TenSanPham.Contains(search)).ToList();


            string searchWithoutDiacritics = search.RemoveDiacritics();


            ViewBag.Search = search;

            List<LOAICA> loaiCas = db.LOAICAs.ToList();
            ViewBag.Loai = loaiCas;

            //Paging
            int NoOfRecordPerPage = 10;
            int NoOfPages = Convert.ToInt32(Math.Ceiling
                (Convert.ToDouble(sp.Count) / Convert.ToDouble(NoOfRecordPerPage)));
            int NoOfRecordToSkip = (page - 1) * NoOfRecordPerPage;
            ViewBag.Page = page;
            ViewBag.NoOfPages = NoOfPages;
            sp = sp.Skip(NoOfRecordToSkip).Take(NoOfRecordPerPage).ToList();

            return View(sp);
        }
        public ActionResult Create()
        {

          
            MyDBContext db = new MyDBContext();
            List<LOAICA> loai = db.LOAICAs.ToList();
            ViewBag.LoaiCa = loai;
            return View();

        }
        [HttpPost]
        public ActionResult Create(SANPHAMCACANH product,HttpPostedFileBase imageFile)
        {
            if (ModelState.IsValid)
            {
                MyDBContext db = new MyDBContext();

                if (imageFile != null && imageFile.ContentLength > 0)
                {
                    if (imageFile.ContentLength > 2000000)
                    {
                        ModelState.AddModelError("Image", "Kích thước file phải nhỏ hơn 2MB.");
                        return View();
                    }

                    var allowEx = new[] { ".jpg", ".png" };
                    var fileEx = Path.GetExtension(imageFile.FileName).ToLower();
                    if (!allowEx.Contains(fileEx))
                    {
                        ModelState.AddModelError("Image", "Chỉ chấp nhận file ảnh jpg hoặc png.");
                        return View();
                    }

                    product.HinhAnh = "";
                    db.SANPHAMCACANHs.Add(product);
                    db.SaveChanges();

                    SANPHAMCACANH pro = db.SANPHAMCACANHs.ToList().Last();

                    var fileName = pro.SanPhamID.ToString() + fileEx;
                    var path = Path.Combine(Server.MapPath("~/Data/img"), fileName);
                    imageFile.SaveAs(path);

                    pro.HinhAnh = fileName;
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
                else
                {
                    product.HinhAnh = "";
                    db.SANPHAMCACANHs.Add(product);
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
            }
            else
            {
                return RedirectToAction("Create");
            }
      
        }
        public ActionResult Edit(int id)
        {
            MyDBContext db = new MyDBContext();
            SANPHAMCACANH sanPham = db.SANPHAMCACANHs.Where(x => x.SanPhamID == id).FirstOrDefault();
            List<LOAICA> loai = db.LOAICAs.ToList();
            ViewBag.LoaiCa = loai;
            return View(sanPham);
        }
        [HttpPost]
        public ActionResult Edit(SANPHAMCACANH sp)
        {
            MyDBContext db = new MyDBContext();
            SANPHAMCACANH sanPham = db.SANPHAMCACANHs.Where(x => x.SanPhamID == sp.SanPhamID).FirstOrDefault();
            sanPham.SanPhamID = sp.SanPhamID;
            sanPham.TenSanPham = sp.TenSanPham;
            sanPham.Gia = sp.Gia;
            sanPham.HinhAnh = sp.HinhAnh;
            sanPham.LoaiCaID = sp.LoaiCaID;
            sanPham.MoTa1 = sp.MoTa1;
            sanPham.MoTa2 = sp.MoTa2;
            sanPham.MoTa3 = sp.MoTa3;
            sanPham.MoTa = sp.MoTa;
            db.SaveChanges();
            return RedirectToAction("Index");

        }
        public ActionResult Delete(SANPHAMCACANH sp, int id)
        {
            MyDBContext db = new MyDBContext();
            SANPHAMCACANH sanPham = db.SANPHAMCACANHs.Where(x => x.SanPhamID == id).FirstOrDefault();
            db.SANPHAMCACANHs.Remove(sanPham);
            db.SaveChanges();

            return RedirectToAction("index");
        }
        public ActionResult Details(int? id)
        {
            MyDBContext db = new MyDBContext();
            SANPHAMCACANH sp = db.SANPHAMCACANHs.Where(x => x.SanPhamID == id).FirstOrDefault();




            return View(sp);
        }
    }
}