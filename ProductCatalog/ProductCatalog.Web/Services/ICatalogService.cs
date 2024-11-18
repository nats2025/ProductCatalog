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
   public  interface ICatalogService
    {
        Task<IEnumerable<CatalogItem>> GetCatalogItems();
        Task<CatalogItem> GetById(int Id);

        Task Update(CatalogItem item);
    }
}
