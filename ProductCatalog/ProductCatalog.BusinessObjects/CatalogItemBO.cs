using ProductCatalog.Domain;
using ProductCatalog.Repositories;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ProductCatalog.BusinessObjects
{
    public class CatalogItemBO : ICatalogItemBO
    {
        ICatalogItemRepository _repository;
        public CatalogItemBO(ICatalogItemRepository repository)
        {
            _repository = repository;
        }
        public async Task<CatalogItem> Add(CatalogItem item)
        {
            //Saving image into Storage Account as blob storage and get the URL
            //item.PictureUrl = ""
            await _repository.Add(item);
            return item;
        }

        public async Task Delete(int id)
        {
            await _repository.Delete(id);
        }

        public async Task<CatalogItem> GetCatalogItemDetails(int id)
        {
            return await _repository.GetById(id);
        }

        public async Task<IEnumerable<CatalogItem>> GetCatalogItems()
        {
            return await _repository.GetAll();
        }

        public async Task Update(CatalogItem newCatalogItem)
        {
            var dbCatalogItem = await _repository.GetById(newCatalogItem.Id);

            bool isPriceChanged = dbCatalogItem.Price != newCatalogItem.Price;
            decimal oldPrice = dbCatalogItem.Price;

            foreach (var pi in newCatalogItem.GetType().GetProperties())
            {
                pi.SetValue(dbCatalogItem, pi.GetValue(newCatalogItem));
            }
            await _repository.Update(dbCatalogItem);
        }
    }

}
