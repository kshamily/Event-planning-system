using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace EEPS.WebApp.Controllers
{
    public class BaseController : Controller
    {
       public Uri APIAddress = new Uri("https://localhost:44338/");
    }
}