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
    [Authorize]
    public class CustomersController : BaseController
    {
        public async Task<ActionResult> Index()
        {
            SetUser();
            return View();
        }

        public async Task<ActionResult> Customer_Read([DataSourceRequest] DataSourceRequest request)
        {
            IEnumerable<CustomerModel> customers = new List<CustomerModel>();

            using (var client = new HttpClient())
            {
                client.BaseAddress = APIAddress;
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                HttpResponseMessage response = await client.GetAsync("api/customerDetails");
                if (response.IsSuccessStatusCode)
                {
                    customers = response.Content.ReadAsAsync<List<CustomerModel>>().Result;

                }
            }

            return Json(customers.ToDataSourceResult(request));
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public async Task<ActionResult> Customer_Create([DataSourceRequest] DataSourceRequest request, CustomerModel customermodel)
        {
            SetUser();
            if (customermodel != null && ModelState.IsValid)
            {
                using (var client = new HttpClient())
                {
                    customermodel.UserID = CurrentUser.UserId;
                    client.BaseAddress = APIAddress;
                    client.DefaultRequestHeaders.Accept.Clear();
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                    HttpResponseMessage response = await client.PostAsJsonAsync<CustomerModel>("api/CustomerDetails", customermodel);
                    if (response.IsSuccessStatusCode)
                    {
                        customermodel = response.Content.ReadAsAsync<CustomerModel>().Result;
                    }
                    else
                    {
                        ModelState.AddModelError("", "Error");
                    }
                }
            }

            return Json(new[] { customermodel }.ToDataSourceResult(request, ModelState));
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public async Task<ActionResult> Customer_Update([DataSourceRequest] DataSourceRequest request, CustomerModel customermodel)
        {
            SetUser();
            if (customermodel != null && ModelState.IsValid)
            {
                using (var client = new HttpClient())
                {
                    client.BaseAddress = APIAddress;
                    client.DefaultRequestHeaders.Accept.Clear();
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                    HttpResponseMessage response = await client.PutAsJsonAsync<CustomerModel>($"api/CustomerDetails/{customermodel.CustomerId}", customermodel);
                    if (!response.IsSuccessStatusCode)
                    {
                        ModelState.AddModelError("", "Error");
                    }
                }
            }

            return Json(new[] { customermodel }.ToDataSourceResult(request, ModelState));
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public async Task<ActionResult> Customer_Delete([DataSourceRequest] DataSourceRequest request, CustomerModel customermodel)
        {
            SetUser();
            if (customermodel != null && ModelState.IsValid)
            {
                using (var client = new HttpClient())
                {
                    client.BaseAddress = APIAddress;
                    client.DefaultRequestHeaders.Accept.Clear();
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                    HttpResponseMessage response = await client.DeleteAsync($"api/CustomerDetails/{customermodel.CustomerId}");
                    if (!response.IsSuccessStatusCode)
                    {
                        ModelState.AddModelError("", "Error");
                    }
                }
            }

            return Json(new[] { customermodel }.ToDataSourceResult(request, ModelState));
        }


    }
}