using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Formatting;
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
        public async Task<ActionResult> Index()
        {
            SetUser();

            using (var client = new HttpClient())
            {
                client.BaseAddress = APIAddress;
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                HttpResponseMessage response = await client.GetAsync("api/CustomerDetails");
                if (response.IsSuccessStatusCode)
                {

                    Session["CustomersSelectList"] = response.Content.ReadAsAsync<List<CustomerModel>>()
                        .Result
                        .Where(x => x.IsActive)
                        .Select(x => new SelectListItem()
                        {
                            Text = x.Name,
                            Value = x.CustomerId.ToString()
                        }).ToList();

                }
            }

            return View();
        }

        public async Task<ActionResult> Event_Read([DataSourceRequest] DataSourceRequest request)
        {
            SetUser();
            List<EventModel> events = new List<EventModel>();

            using (var client = new HttpClient())
            {
                client.BaseAddress = APIAddress;
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                HttpResponseMessage response = await client.GetAsync("api/eventDetails");
                if (response.IsSuccessStatusCode)
                {
                    events = response.Content.ReadAsAsync<List<EventModel>>().Result.ToList();

                }
            }

            if (!User.IsInRole("admin"))
            {
                events = events.Where(x => x.UserId == CurrentUser.UserId).ToList();
            }

            foreach (var item in events)
            {
                item.CustomerName = item.Customer.Name;
            }

            return Json(events.ToDataSourceResult(request));
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public async Task<ActionResult> Event_Create([DataSourceRequest] DataSourceRequest request, EventModel eventmodel)
        {
            SetUser();
            if (eventmodel != null && ModelState.IsValid)
            {
                using (var client = new HttpClient())
                {
                    eventmodel.UserId = CurrentUser.UserId;
                    if (Session["fileName"] != null)
                        eventmodel.FilePath = Session["fileName"].ToString();
                    client.BaseAddress = APIAddress;
                    client.DefaultRequestHeaders.Accept.Clear();
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                    HttpResponseMessage response = await client.PostAsJsonAsync<EventModel>("api/EventDetails", eventmodel);
                    if (response.IsSuccessStatusCode)
                    {
                        Session["fileName"] = null;
                        eventmodel = response.Content.ReadAsAsync<EventModel>().Result;
                    }
                    else
                    {
                        ModelState.AddModelError("", "Error");
                    }
                }
            }

            return Json(new[] { eventmodel }.ToDataSourceResult(request, ModelState));
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public async Task<ActionResult> Event_Update([DataSourceRequest] DataSourceRequest request, EventModel eventmodel)
        {
            SetUser();
            if (eventmodel != null && ModelState.IsValid)
            {
                using (var client = new HttpClient())
                {
                    client.BaseAddress = APIAddress;
                    client.DefaultRequestHeaders.Accept.Clear();
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                    if (Session["fileName"] != null)
                    {
                        eventmodel.FilePath = Session["fileName"].ToString();
                    }

                    HttpResponseMessage response = await client.PutAsJsonAsync<EventModel>($"api/EventDetails/{eventmodel.EventId}", eventmodel);
                    if (!response.IsSuccessStatusCode)
                    {
                        ModelState.AddModelError("", "Error");
                    }
                    Session["fileName"] = null;
                }
            }

            return Json(new[] { eventmodel }.ToDataSourceResult(request, ModelState));
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public async Task<ActionResult> Event_Delete([DataSourceRequest] DataSourceRequest request, EventModel eventmodel)
        {
            SetUser();
            if (eventmodel != null && ModelState.IsValid)
            {
                using (var client = new HttpClient())
                {
                    client.BaseAddress = APIAddress;
                    client.DefaultRequestHeaders.Accept.Clear();
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                    HttpResponseMessage response = await client.DeleteAsync($"api/EventDetails/{eventmodel.EventId}");
                    if (!response.IsSuccessStatusCode)
                    {
                        ModelState.AddModelError("", "Error");
                    }
                }
            }

            return Json(new[] { eventmodel }.ToDataSourceResult(request, ModelState));
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public async Task<ActionResult> EventUpdateDocument(int eventid, string filepath)
        {
            if (ModelState.IsValid)
            {
                using (var client = new HttpClient())
                {
                    client.BaseAddress = APIAddress;
                    client.DefaultRequestHeaders.Accept.Clear();
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                    var parameters = new Dictionary<string, string> { { "eventid", eventid.ToString() }, { "filepath", filepath } };
                    var encodedContent = new FormUrlEncodedContent(parameters);
                    HttpResponseMessage response = await client.PostAsync("api/EventDocument", encodedContent);
                    if (response.IsSuccessStatusCode)
                    {

                    }
                }
            }

            return Json(new { });
        }


        public ActionResult Async_Save(IEnumerable<HttpPostedFileBase> files)
        {
            SetUser();
            DirectoryInfo di = new DirectoryInfo(Server.MapPath("~/Files/" + CurrentUser.UserId));
            if (!di.Exists)
            {
                di.Create();
            }
            if (files != null)
            {
                foreach (var file in files)
                {
                    var fileName = Path.GetFileName(file.FileName);
                    var physicalPath = Path.Combine(Server.MapPath("~/Files/" + CurrentUser.UserId), fileName);

                    file.SaveAs(physicalPath);
                    Session["fileName"] = fileName;
                }
            }

            return Content("");
        }

        public ActionResult Async_Remove(string[] fileNames)
        {
            SetUser();
            if (fileNames != null)
            {
                foreach (var fullName in fileNames)
                {
                    var fileName = Path.GetFileName(fullName);
                    var physicalPath = Path.Combine(Server.MapPath("~/Files/" + CurrentUser.UserId), fileName);

                    if (System.IO.File.Exists(physicalPath))
                    {
                        System.IO.File.Delete(physicalPath);
                    }
                }
            }

            return Content("");
        }

    }
}