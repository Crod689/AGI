﻿using AGIMaster.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using WebMatrix.WebData;

namespace AGIMaster.Controllers
{
   // [InitializeSimpleMembership]
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
                    u.Roles = "User";
                    db.Tables.Add(u);
                    db.SaveChanges();
                }
               // Roles.CreateRole("User");
                WebSecurity.CreateUserAndAccount(u.Username, u.Password);
                Roles.AddUserToRole(u.Username, "User");
                return RedirectToAction("Login", "Account");
            }
            else
            {
                return View(u);
            }

        }
    }
}
