using Microsoft.Extensions.Configuration;
using ProductCatalog.Web.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace ProductCatalog.Web.Services
{
   public class CatalogSevice : ICatalogService
    {
        private readonly IConfiguration _config;
        string _remoteServiceBaseUrl;
        public CatalogSevice(IConfiguration config)
        {
            _config = config;
            _remoteServiceBaseUrl = _config["CatalogUrl"];
        }
        public async Task<CatalogItem> GetById(int id)
        {
            HttpClient client = new HttpClient();
            string strjson = await client.GetStringAsync(_remoteServiceBaseUrl + "/CatalogItems/" + id);
            CatalogItem items = JsonConvert.DeserializeObject<CatalogItem>(strjson);
            return items;
        }

        public async Task<IEnumerable<CatalogItem>> GetCatalogItems()
        {
            var client = new HttpClient();
            var result = await client.GetAsync(_remoteServiceBaseUrl + "/CatalogItems/");
            var dataString = await result.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<IEnumerable<CatalogItem>>(dataString);
        }

        public async Task Update(CatalogItem item)
        {
            var client = new HttpClient();
            HttpContent content = new StringContent(JsonConvert.SerializeObject(item), System.Text.Encoding.UTF8, "application/json");
            var result = await client.PutAsync(_remoteServiceBaseUrl + "/CatalogItems/" + item.Id, content);
            var dataString = await result.Content.ReadAsStringAsync();
        }
    }

}
