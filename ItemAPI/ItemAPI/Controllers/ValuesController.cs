using ItemAPI.Entities;
using ItemAPI.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace ItemAPI.Controllers
{
    // To consider: refactor out the Add/Delete/Get items into their own class and inject/unit-test
    public class ValuesController : ApiController
    {
        const string AddCommand = "add";
        const string DeleteCommand = "delete";

        //// GET api/values
        [HttpGet]
        public HttpResponseMessage Get()
        {
            var apiItemModels = GetItems();
            return Request.CreateResponse(
                HttpStatusCode.OK,
                apiItemModels);
        }

        // POST api/values
        [HttpPost]
        public void Post(HttpRequestMessage request)
        {
            string jsonContent = request.Content.ReadAsStringAsync().Result;
            var model = JsonConvert.DeserializeObject<ItemAPIModel>(jsonContent);

            // Because the react front end app is on a different server I cannot use REST delete
            // unless I implement CORS authentication. So instead I hacked deletes through POST...
            // The front end does not support update.
            if (model.operation == "add")
            {
                AddItem(model);
            }
            else if (model.operation == "delete")
            {
                DeleteItem(model);
            }
        }

        void AddItem(ItemAPIModel model)
        {
            using (var context = new ItemContext())
            {
                var items = context.Items.ToList();
                context.Items.Add(
                    new Item
                    {
                        Id = Guid.NewGuid(),
                        Name = model.name,
                        Value = Convert.ToDouble(model.value),
                        Category = model.category,
                        DateAdded = DateTime.Now
                    });
                context.SaveChanges();
            }
        }

        void DeleteItem(ItemAPIModel model)
        {
            using (var context = new ItemContext())
            {
                // Note: if there are duplicate items entered in the UI
                // then we can not know when item it relates to in the DB without an ID.
                // So we just delete the first one found.
                // To resolve this, we could add a non-visible identifier in the UI and
                // add it to the REST methods.
                var valueAsDouble = Convert.ToDouble(model.value);
                var item = context.Items.FirstOrDefault(i => i.Name == model.name && //Delete the First one found
                                                    i.Value == valueAsDouble &&
                                                    i.Category == model.category);
                if (item != null)
                {
                    context.Items.Remove(item);
                    context.SaveChanges();
                }
            }
        }

        IEnumerable<ItemAPIModel> GetItems()
        {
            using (var context = new ItemContext())
            {
                var items = context.Items.OrderBy(i => i.DateAdded).ToList();
                var apiItemModels = items.Select(i => new ItemAPIModel
                {
                    value = i.Value.ToString(),
                    name = i.Name,
                    category = i.Category
                }).ToList();
                return apiItemModels;
            }
        }
    }
}
