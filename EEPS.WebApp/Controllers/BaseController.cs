using EEPS.DAL.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace EEPS.WebApp.Controllers
{
    public class BaseController : Controller
    {
        public Uri APIAddress = new Uri("https://localhost:44338/");
        public UserDetail CurrentUser { get; set; }
        public BaseController()
        {

        }
        public async Task SetUser()
        {
            if (Session != null && Session["CurrentUser"] == null && Request.IsAuthenticated && CurrentUser == null)
            {
                using (var client = new HttpClient())
                {
                    client.BaseAddress = APIAddress;
                    client.DefaultRequestHeaders.Accept.Clear();
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                    HttpResponseMessage response = await client.GetAsync($"api/Authentication/{User.Identity.Name}");
                    if (response.IsSuccessStatusCode)
                    {
                        var details = response.Content.ReadAsAsync<UserDetail>().Result;
                        if (details.IsActive)
                        {
                            Session["CurrentUser"] = details;
                            CurrentUser = details;
                        }
                    }

                }

            }
            else if (Session != null && Session["CurrentUser"] != null && CurrentUser == null)
            {
                CurrentUser = (UserDetail)Session["CurrentUser"];
            }

        }




    }
}