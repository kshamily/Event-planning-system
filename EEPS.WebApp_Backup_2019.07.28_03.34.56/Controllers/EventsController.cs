using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using EEPS.DAL.Data;
using Kendo.Mvc.Extensions;
using Kendo.Mvc.UI;

namespace EEPS.WebApp.Controllers
{
    [Authorize]
    public class EventsController : BaseController
    {
        public ActionResult Index()
        {
            return View();
        }

        public async Task<ActionResult> EditingPopup_Read([DataSourceRequest] DataSourceRequest request)
        {
            IEnumerable<EventModel> events = new List<EventModel>();

            using (var client = new HttpClient())
            {
                client.BaseAddress = APIAddress;
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                //  var parameters = new Dictionary<string, string> { { "username", username }, { "password", password } };
                //  var encodedContent = new FormUrlEncodedContent(parameters);
                HttpResponseMessage response = await client.GetAsync("api/eventDetails");
                if (response.IsSuccessStatusCode)
                {

                    events = response.Content.ReadAsAsync<List<EventModel>>().Result;

                }
            }

            return Json(events.ToDataSourceResult(request));
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult EditingPopup_Create([DataSourceRequest] DataSourceRequest request, EventModel product)
        {
            if (product != null && ModelState.IsValid)
            {
                //productService.Create(product);
            }

            return Json(new[] { product }.ToDataSourceResult(request, ModelState));
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult EditingPopup_Update([DataSourceRequest] DataSourceRequest request, EventModel product)
        {
            if (product != null && ModelState.IsValid)
            {
                //productService.Update(product);
            }

            return Json(new[] { product }.ToDataSourceResult(request, ModelState));
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult EditingPopup_Destroy([DataSourceRequest] DataSourceRequest request, EventModel product)
        {
            if (product != null)
            {
                //productService.Destroy(product);
            }

            return Json(new[] { product }.ToDataSourceResult(request, ModelState));
        }


    }
}