using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AGIMaster.Controllers
{
    public class AdminController : Controller
    {
        //
        // GET: /Admin/
       [Authorize(Roles="Admin")]
        public ActionResult Dashboard()
        {
            return View();
        }

    }
}
