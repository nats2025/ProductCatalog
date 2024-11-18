using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using ProductCatalog.Web.Models;
using ProductCatalog.Web.Services;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace MVCWebApp.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ICatalogService _catalogService;
        IConfiguration _config;
        public HomeController(ILogger<HomeController> logger, ICatalogService catalogService, IConfiguration config)
        {
            _logger = logger;
            _config = config;
            _catalogService = catalogService;
        }

        public IActionResult Index()
        {
            int n=0;
            return View();
        }

        public async Task< IActionResult> Catalog()
        {
            ViewBag.StorageEndPoint = _config["storage-endpoint"];
            IEnumerable<CatalogItem> items = await _catalogService.GetCatalogItems();
            return View(items);
        }

        public async Task<IActionResult> Details(int id)
        {
            ViewBag.StorageEndPoint = _config["storage-endpoint"];
            CatalogItem item = await _catalogService.GetById(id);
            return View(item);
        }

        public async Task<IActionResult> Edit(int id)
        {
            var item = await _catalogService.GetById(id);
            return View(item);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(int id, CatalogItem item)
        {
            await _catalogService.Update(item);
            var items = await _catalogService.GetCatalogItems();
            return View("Index", items);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
