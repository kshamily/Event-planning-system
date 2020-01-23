using EEPS.DAL.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace EEPS.WebApp.Controllers
{
    public class AccountController : BaseController
    {

        // GET: Account
        [HttpGet]
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<ActionResult> Login(string username, string password, string rememberme)
        {

            using (var client = new HttpClient())
            {
                client.BaseAddress = APIAddress;
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                var parameters = new Dictionary<string, string> { { "username", username }, { "password", password } };
                var encodedContent = new FormUrlEncodedContent(parameters);
                HttpResponseMessage response = await client.PostAsync("api/Authentication", encodedContent);
                if (response.IsSuccessStatusCode)
                {
                    UserDetail obj = new UserDetail();
                    var details = response.Content.ReadAsAsync<UserDetail>().Result;

                    FormsAuthentication.SetAuthCookie(details.FullName, !string.IsNullOrWhiteSpace(rememberme));

                    return RedirectToAction("Index", "Events");
                }
                else
                {
                    ModelState.AddModelError("", "Please enter valid username or password");
                    return View();
                }
            }
        }

        public ActionResult Logout()
        {
            if (Request.IsAuthenticated)
            {
                FormsAuthentication.SignOut();
                return RedirectToAction("Index", "Events");
            }
            return View();
        }

    }
}