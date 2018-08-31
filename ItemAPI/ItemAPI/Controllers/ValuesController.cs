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
        // GET api/values
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/values/5
        public string Get(int id)
        {
            return "value";
        }

        // POST api/values
        //TODO: asnyc and await;
        [HttpPost]
        public void Post(HttpRequestMessage request)
        {
            string jsonContent = request.Content.ReadAsStringAsync().Result;
            var model = JsonConvert.DeserializeObject<ItemAPIModel>(jsonContent);
            using (var context = new ItemContext())
            {
                var items = context.Items.ToList();
                context.Items.Add(new Item { Id = Guid.NewGuid(), name = model.name, value = model.value, category = model.category });
                context.SaveChanges(); //Todo async?
            }
        }

        //Either add Id or just delete first found
        // DELETE api/values/5
        public void Delete(int id)
        {
        }
    }
}
