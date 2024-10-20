using AbuOdeh_Electromechanical.Models;
using AbuOdeh_Electromechanical.Repository.Entities;

namespace AbuOdeh_Electromechanical.Repository.Services
{
    public interface IProductSvc
    {
        Task<int> Add(Product category);
        Task<int> Update(Product category);
        Task<int> Delete(Guid objectKey);
        Task<List<Product>> GetAll(ProductPagenation productPagenation);
        Task<List<Product>> GetAll();
        Task<Product> GetById(Guid objectKey);
    }
}
