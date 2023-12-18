using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CodeFirst_LaptrinhWeb.Models;
namespace CodeFirst_LaptrinhWeb.Controllers
{
    public class CartController : Controller
    {
        // GET: Cart
        public ActionResult Index()
        {
            MyDBContext db = new MyDBContext();
            List<Cart> carts = db.Carts.ToList();
            return View(carts);
        }
        public ActionResult Add(int id = 0)
        {
            if (id > 0)
            {
                MyDBContext db = new MyDBContext();
                Cart cartItem = db.Carts.Where(cart => cart.SanPhamID == id).FirstOrDefault();
                if (cartItem != null)
                {
                    cartItem.Quantity += 1;
                }
                else
                {
                    Cart cart = new Cart();
                    cart.SanPhamID = id;
                    cart.Quantity = 1;
                    db.Carts.Add(cart);
                }
                db.SaveChanges();
            }

            return RedirectToAction("Index");
        }

        public ActionResult UpdateQuantity(int quan = 0, int proid = 0)
        {
            MyDBContext db = new MyDBContext();
            if (quan > 0)
            {
                Cart cartItem = db.Carts.Where(cart => cart.SanPhamID == proid).FirstOrDefault();
                if (cartItem != null)
                {
                    cartItem.Quantity = quan;
                    db.SaveChanges();
                }
            }

            return RedirectToAction("Index");
        }
        public ActionResult Delete(Cart cart, int id)
        {
            MyDBContext db = new MyDBContext();
            Cart pro = db.Carts.Where(x => x.CartID == id).FirstOrDefault();
            db.Carts.Remove(pro);
            db.SaveChanges();

            return RedirectToAction("index");
        }
       

    }
}