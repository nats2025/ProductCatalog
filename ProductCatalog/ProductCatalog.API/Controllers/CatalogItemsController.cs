using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProductCatalog.BusinessObjects;
using ProductCatalog.Domain;
using ProductCatalog.EFRepositories;

namespace ProductCatalog.API.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class CatalogItemsController : ControllerBase
    {
        ICatalogItemBO _boCatalogItem;
        public CatalogItemsController(ICatalogItemBO boCatalogItem)
        {
            // _context = context;
            _boCatalogItem = boCatalogItem;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<CatalogItem>>> GetCatalogItem()
        {
            var items = await _boCatalogItem.GetCatalogItems(); ;
            return Ok(items);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<CatalogItem>> GetCatalogItem(int id)
        {
            var catalogItem = await _boCatalogItem.GetCatalogItemDetails(id);
            if (catalogItem == null)
            {
                return NotFound();
            }
            return catalogItem;
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutCatalogItem(int id, CatalogItem catalogItem)
        {
            if (id != catalogItem.Id)
            {
                return BadRequest();
            }
            try
            {
                await _boCatalogItem.Update(catalogItem);
            }
            catch (ApplicationException ex)
            {
                if (ex.Message == "Not Found")
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }
            return NoContent();
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        public async Task<ActionResult<CatalogItem>> PostCatalogItem(CatalogItem catalogItem)
        {
            await _boCatalogItem.Add(catalogItem);
            return CreatedAtAction("GetCatalogItem", new { id = catalogItem.Id }, catalogItem);
        }

        [HttpDelete("{id}")]
        public async Task DeleteCatalogItem(int id)
        {
            await _boCatalogItem.Delete(id);
        }
    }
}
