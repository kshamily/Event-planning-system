using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace EEPS.WebApp.Controllers
{
    [Authorize]
    public class UsersController : BaseController
    {
        public ActionResult Index()
        {
            return View();
        }

    }
}