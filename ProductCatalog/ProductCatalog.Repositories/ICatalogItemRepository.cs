using ProductCatalog.Domain;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ProductCatalog.Repositories
{

    public interface IGenericRepository<T>
    {
        Task<IEnumerable<T>> GetAll();
        Task<T> GetById(int id);
        Task<T> Add(T item);
        Task Update(T item);
        Task Delete(int id);
    }
    public interface ICatalogItemRepository : IGenericRepository<CatalogItem>
    {    
    
    }

    public interface ICatalogTypeRepository : IGenericRepository<CatalogType>
    {     }

    public interface ICatalogBrandRepository : IGenericRepository<CatalogBrand>
    {     }
}
