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
        //
        // GET: /Orders/

        public ActionResult Orders(AGIMaster.Models.OrderProduct u)
        { 
            //return View();

            if (ModelState.IsValid)
            {
                using (Models.UserTableEntities1 db = new Models.UserTableEntities1())
                {
                    IEnumerable listOfNames = db.Orders.ToList();
                    return View(listOfNames);
                }
            }
            return View();
        }
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
        [HttpPost]
        public void CreateOrder(string products, string quantities)
        {
            using (Models.UserTableEntities1 db = new Models.UserTableEntities1())
            {
                var newOrder = new Order { Comapny = "Cerberus" };
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
     
    }
}
