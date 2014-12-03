using AGIMaster.Models;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;

namespace AGIMaster.Controllers
{
    public class AdminController : Controller
    {
        private UserTableEntities1 db = new UserTableEntities1();
        //
        // GET: /Admin/
        [Authorize(Roles = "Admin")]
        public ActionResult Dashboard()
        {
            return View();
        }
        [Authorize(Roles = "Admin")]
        public ActionResult Orders(/*AGIMaster.Models.OrderProduct u*/)
        {
            if (ModelState.IsValid)
            {
                //  using (Models.UserTableEntities1 db = new Models.UserTableEntities1())
                //  {
                IEnumerable listOfNames = db.Orders.ToList();
                return View(listOfNames);
                // }
            }
            return View();

        }
        [Authorize(Roles = "Admin")]
        public void Approve(string orders)
        {
            var orderArr = orders.Split(',');
            var id = 0;
            for (var i = 0; i < orderArr.Length; i++)
            {
                id = Convert.ToInt32(orderArr[i]);
                var singleOrder = db.Orders.FirstOrDefault(x => x.Id == id);
                if (singleOrder != null)
                {
                    singleOrder.Pending = "Approved";
                }
                db.SaveChanges();
            }

        }
        [Authorize(Roles = "Admin")]
        public ActionResult UserApproval()
        {
            return View();
        }
        [Authorize(Roles = "Admin")]
        public void ApproveUsers(string users)
        {

        }
        [Authorize(Roles = "Admin")]
        public ActionResult Edit(string order)
        {
            var id = Convert.ToInt32(order);
            IEnumerable list = db.OrderProducts.Where(x => x.Order_Id == id).ToList();
            //Dictionary<string, int> map = new Dictionary<string, int>();
            //foreach (var item in list)
            //{
            //    map.Add(item.Product.Name, item.Quantity);
            //}
            // return View(Json(map, JsonRequestBehavior.AllowGet));
            return View(list);

        }
        [Authorize(Roles = "Admin")]
        public void Deny(string orders)
        {
            var orderArr = orders.Split(',');
            var id = 0;
            for (var i = 0; i < orderArr.Length; i++)
            {
                id = Convert.ToInt32(orderArr[i]);
                var singleOrder = db.Orders.FirstOrDefault(x => x.Id == id);
                if (singleOrder != null)
                {
                    singleOrder.Pending = "Denied";
                }
                db.SaveChanges();
            }
        }
        [Authorize(Roles = "Admin")]
        public string getProducts()
        {
            var productList = from a in db.Products select new { Id = a.Id, Name = a.Name, Description = a.Description };
            // var productList = db.Products..ToList();
            var jsonSerialiser = new JavaScriptSerializer();
            var json = jsonSerialiser.Serialize(productList.ToList());
            return json;
        }
        [Authorize(Roles = "Admin")]
        public void UpdateOrder(int order, string products, string quantities)
        {
            using (Models.UserTableEntities1 db = new Models.UserTableEntities1())
            {
                //var company = db.Tables.FirstOrDefault(x => x.Username == User.Identity.Name).CompanyName;
                var company = "Edited by: " + User.Identity.Name + " original order ID is: " + order;
                var newOrder = new Order { Comapny = company, Pending = "Pending" };
                var productArray = products.Split(',');
                var quantityArray = quantities.Split(',');
                db.Orders.Add(newOrder);
                db.SaveChanges();
                db.AdminOrders.Add(new AdminOrder { AdminOrder_Id = newOrder.Id, Order_Id = order });
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
