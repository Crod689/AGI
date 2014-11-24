using AGIMaster.Models;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AGIMaster.Controllers
{
    public class AdminController : Controller
    {
        private UserTableEntities1 db = new UserTableEntities1(); 
        //
        // GET: /Admin/
       [Authorize(Roles="Admin")]
        public ActionResult Dashboard()
        {
            return View();
        }
        [Authorize(Roles="Admin")]
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
        [Authorize(Roles="Admin")]
        public void Approve(string orders){
            var orderArr = orders.Split(',');
            var id = 0;
            for (var i = 0; i < orderArr.Length;i++ )
            {
                id= Convert.ToInt32(orderArr[i]);
               var singleOrder = db.Orders.FirstOrDefault(x => x.Id == id);
               if (singleOrder != null)
               {
                   singleOrder.Pending = "Approved";
               }
               db.SaveChanges();
            }

        }
        [Authorize(Roles="Admin")]
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
    }
}
