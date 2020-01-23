using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Kendo.Mvc.Extensions;
using Kendo.Mvc.UI;
using EEPS.DAL.Data;

namespace EEPS.WebApp.Controllers
{
    [Authorize(Roles = "admin")]
    public class UsersController : BaseController
    {
        public async Task<ActionResult> Index()
        {
            SetUser();

            return View();
        }

        public async Task<ActionResult> User_Read([DataSourceRequest] DataSourceRequest request)
        {
            SetUser();
            IEnumerable<UserDetail> users = new List<UserDetail>();

            using (var client = new HttpClient())
            {
                client.BaseAddress = APIAddress;
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                HttpResponseMessage response = await client.GetAsync("api/userDetails");
                if (response.IsSuccessStatusCode)
                {
                    users = response.Content.ReadAsAsync<List<UserDetail>>().Result;

                }
            }

            return Json(users.ToDataSourceResult(request));
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public async Task<ActionResult> User_Create([DataSourceRequest] DataSourceRequest request, UserDetail UserDetail)
        {
            SetUser();
            if (UserDetail != null && ModelState.IsValid)
            {
                using (var client = new HttpClient())
                {
                    client.BaseAddress = APIAddress;
                    client.DefaultRequestHeaders.Accept.Clear();
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                    HttpResponseMessage response = await client.PostAsJsonAsync<UserDetail>("api/userDetails", UserDetail);
                    if (response.IsSuccessStatusCode)
                    {
                        UserDetail = response.Content.ReadAsAsync<UserDetail>().Result;
                    }
                    else
                    {
                        ModelState.AddModelError("", "Error");
                    }
                }
            }

            return Json(new[] { UserDetail }.ToDataSourceResult(request, ModelState));
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public async Task<ActionResult> User_Update([DataSourceRequest] DataSourceRequest request, UserDetail UserDetail)
        {
            SetUser();
            if (UserDetail != null && ModelState.IsValid)
            {
                using (var client = new HttpClient())
                {
                    client.BaseAddress = APIAddress;
                    client.DefaultRequestHeaders.Accept.Clear();
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                    HttpResponseMessage response = await client.PutAsJsonAsync<UserDetail>($"api/userDetails/{UserDetail.UserId}", UserDetail);
                    if (!response.IsSuccessStatusCode)
                    {
                        ModelState.AddModelError("", "Error");
                    }
                }
            }

            return Json(new[] { UserDetail }.ToDataSourceResult(request, ModelState));
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public async Task<ActionResult> User_Delete([DataSourceRequest] DataSourceRequest request, UserDetail UserDetail)
        {
            SetUser();
            if (UserDetail != null && ModelState.IsValid)
            {
                using (var client = new HttpClient())
                {
                    client.BaseAddress = APIAddress;
                    client.DefaultRequestHeaders.Accept.Clear();
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                    HttpResponseMessage response = await client.DeleteAsync($"api/userDetails/{UserDetail.UserId}");
                    if (!response.IsSuccessStatusCode)
                    {
                        ModelState.AddModelError("", "Error");
                    }
                }
            }

            return Json(new[] { UserDetail }.ToDataSourceResult(request, ModelState));
        }


    }
}