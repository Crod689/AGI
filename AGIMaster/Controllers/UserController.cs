using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AGIMaster.Controllers
{
    public class UserController : Controller
    {
        //
        // GET: /User/

        public ActionResult Register()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Register(AGIMaster.Models.Table u)
        {
            if (ModelState.IsValid)
            {
                using (Models.UserTable db = new Models.UserTable())
                {
                    db.Tables.Add(u);
                    db.SaveChanges();
                }
            }
            return View(u);
        }
    }
}
