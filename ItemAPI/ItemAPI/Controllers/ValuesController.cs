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
    public class ValuesController : ApiController
    {
        //// GET api/values
        //public IEnumerable<string> Get()
        //{
        //    return new string[] { "value1", "value2" };
        //}

        [HttpGet]
        public HttpResponseMessage Get()
        {
            using (var context = new ItemContext())
            {
                var items = context.Items.ToList();
                var apiItemModels = items
                    .OrderByDescending(i => i.Name)
                    .Select(i => new ItemAPIModel {value = i.Value.ToString(), name = i.Name, category = i.Category })
                    .ToList();
                return Request.CreateResponse(
                    HttpStatusCode.OK,
                    apiItemModels);
            }
        }

        // POST api/values
        //TODO: asnyc and await;
        [HttpPost]
        public void Post(HttpRequestMessage request)
        {
            string jsonContent = request.Content.ReadAsStringAsync().Result;
            var model = JsonConvert.DeserializeObject<ItemAPIModel>(jsonContent);
            if(model.operation == "add") //todo enum?
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
                        });
                    context.SaveChanges();
                }
            }
            else if (model.operation == "delete")
            {
                using (var context = new ItemContext())
                {
                    var valueAsDouble = Convert.ToDouble(model.value);
                    var item = context.Items.FirstOrDefault(i => i.Name == model.name &&
                                                        i.Value == valueAsDouble &&
                                                        i.Category == model.category);
                    if(item != null)
                    {
                        context.Items.Remove(item);
                        context.SaveChanges();
                    }
                }
            }
        }

        //Either add Id or just delete first found
        // DELETE api/values/5
        public void Delete(HttpRequestMessage request)
        {
            string jsonContent = request.Content.ReadAsStringAsync().Result;
            var model = JsonConvert.DeserializeObject<ItemAPIModel>(jsonContent);
        }
    }
}
