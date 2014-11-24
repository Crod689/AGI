using AGIMaster.Models;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AGIMaster.Controllers
{
    public class OrdersController : Controller
    {
        private UserTableEntities1 db = new UserTableEntities1(); 
        //
        // GET: /Orders/
        [Authorize]
        public ActionResult Orders(AGIMaster.Models.OrderProduct u)
        { 
            //return View();

            if (ModelState.IsValid)
            {
                using (Models.UserTableEntities1 db = new Models.UserTableEntities1())
                {
                    var company = db.Tables.FirstOrDefault(x => x.Username == User.Identity.Name).CompanyName;
                    if (company != null){
                        IEnumerable listOfNames = db.Orders.Where(x => x.Comapny == company).ToList();
                        return View(listOfNames);
                    }
                    else
                    {
                        return View();
                    }
                    
                }
            }
            return View();
        }
        [Authorize]
        public ActionResult MakeOrder()
        {
            ViewBag.Message = "Make an Order";
            if (ModelState.IsValid)
            {
                using (Models.UserTableEntities1 db = new Models.UserTableEntities1())
                {
                    IEnumerable listOfNames = db.Products.ToList();
                    return View(listOfNames);
                }
            }
            return View();
        }
        [Authorize]
        [HttpPost]
        public void CreateOrder(string products, string quantities)
        {
            using (Models.UserTableEntities1 db = new Models.UserTableEntities1())
            {   
                var company = db.Tables.FirstOrDefault(x => x.Username == User.Identity.Name).CompanyName;
                var newOrder = new Order { Comapny = company, Pending = "Pending" };
                var productArray = products.Split(',');
                var quantityArray = quantities.Split(',');
                db.Orders.Add(newOrder);
                db.SaveChanges();
                for (var i = 0; i < productArray.Length; i++)
                {
                    var newOrderProduct = new OrderProduct { Order_Id = newOrder.Id, Product_Id = Convert.ToInt32(productArray[i]), Quantity = Convert.ToInt32(quantityArray[i]) };
                    db.OrderProducts.Add(newOrderProduct);
                    db.SaveChanges();
                }
            }
        }
        public JsonResult ViewOrder(int id)
        {
            //using (Models.UserTableEntities1 db = new Models.UserTableEntities1())
            //{
                var list = db.OrderProducts.Where(x => x.Order_Id == id).ToList();
                Dictionary<string, int> map=new Dictionary<string,int>();
                foreach (var item in list)
                {
                    map.Add(item.Product.Name, item.Quantity);
                }
                return Json(map,JsonRequestBehavior.AllowGet);
           // }
        }
     
    }
}
