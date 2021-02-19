using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using SkiProductsWebApp.Models;

namespace SkiProductsWebApp.Controllers
{
    public class ProductsController : Controller
    {
        private SkiProductsEntities1 db = new SkiProductsEntities1();
        MvcApplication mvc = new MvcApplication();

        // GET: Products
        //[CustomFilter]
        public ActionResult Index()
        {
            return View(db.Products.ToList());
        }

        // GET: Products
        public ActionResult Skiing()
        {
            return View(db.Products.ToList());
        }


        // GET: Products
        public ActionResult Undercap()
        {
            var temp = db.Products.Where(i => i.Category_id == 4).Select(i => i).ToList();
            return View("Skiing", temp);
        }


        // GET: Products
        public ActionResult Skiis()
        {
            var temp = db.Products.Where(i => i.Category_id == 1).Select(i => i).ToList();
            return View("Skiing", temp);
        }

        // GET: Products
        public ActionResult SkiJacket()
        {
            var temp = db.Products.Where(i => i.Category_id == 2).Select(i => i).ToList();
            return View("Skiing", temp);
        }

        // POST: Order
        [HttpPost]
        public ActionResult Order(int product_id)
        {
            if (Session["role"]==null)
            {
                return View("Message");
            }
            //pooveriti da li postoji vec neka porudzbina za korisnika, ako postoji uzeti isti order_num
            //int? onum1 = db.Orders.Select(i => i.Order_num).Last();
            //int? onum2 = db.Orders.Select(i => i.Order_num).Last();
            //dozvoljavam svima koji imaju neku rolu da naprave porudzbinu
            Order order = new Order();
            //string prm = Session["userid"].ToString();
            int userid = Convert.ToInt32(Session["userid"]);
            order.Person_id = userid;
            order.Product_id = product_id;
            order.Order_num = 1;
            try
            {
                db.Orders.Add(order);
                db.SaveChanges();
            }
            catch (Exception)
            {

                throw;
            }
            string url = string.Format("/Products/Cart", userid);
            return Redirect(url);
            //return View("Cart");
        }


        // GET: BrandFilter
        [HttpPost]
        public ActionResult Brand(int brand_id)
        {
            var temp = db.Products.Where(i => i.Brand_id == brand_id).Select(i => i).ToList();
            return View("Skiing", temp);
        }


        // GET: Cart
        //int user_id
        [HttpGet]
        public ActionResult Cart()
        {
            List<Product2> products = new List<Product2>();
            if (Session["userid"] == null)
            {
                return View("Message");
            }
            int uid = Convert.ToInt32(Session["userid"]);
            var chart = db.Orders.Where(i => i.Person_id == uid).Select(i=>i).ToList();

            decimal? price=0;
            foreach (var obj in chart)
            {
                Product2 p = new Product2();
                var temp= db.Products.Where(i => i.Id == obj.Product_id).Select(i => i).First();
                p.Id = temp.Id;
                p.Name = temp.Name;
                p.Price = temp.Price;
                p.Image = temp.Image;
                p.Order_id = obj.Order_id;
                products.Add(p);
                price = price + p.Price;
            }
            ViewBag.TotalPrice=price;
            return View(products);
        }


        // POST: Products/Delete/5
        //[HttpPost, ActionName("Delete")]
        //[ValidateAntiForgeryToken]
        public ActionResult DeleteOrder(int order_id)
        {
            Order order = db.Orders.Find(order_id);
            db.Orders.Remove(order);
            db.SaveChanges();
            return RedirectToAction("Cart");
        }



        // GET: Products/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Product product = db.Products.Find(id);
            if (product == null)
            {
                return HttpNotFound();
            }
            return View(product);
        }

        // GET: Products/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Products/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Name,Price,Image,Description")] Product product)
        {
            if (ModelState.IsValid)
            {
                db.Products.Add(product);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(product);
        }

        // GET: Products/Edit/5
        //[Authorize(Roles = "Admin")]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Product product = db.Products.Find(id);
            if (product == null)
            {
                return HttpNotFound();
            }
            return View(product);
        }

        // POST: Products/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Name,Price,Image,Description")] Product product)
        {
            if (ModelState.IsValid)
            {
                db.Entry(product).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(product);
        }

        // GET: Products/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Product product = db.Products.Find(id);
            if (product == null)
            {
                return HttpNotFound();
            }
            return View(product);
        }

        // POST: Products/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Product product = db.Products.Find(id);
            db.Products.Remove(product);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
