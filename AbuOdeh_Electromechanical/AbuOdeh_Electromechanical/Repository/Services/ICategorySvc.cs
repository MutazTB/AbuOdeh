using AbuOdeh_Electromechanical.Repository.Entities;

namespace AbuOdeh_Electromechanical.Repository.Services
{
    public interface ICategorySvc
    {
        Task<int> Add(Category category);
        Task<int> Update(Category category);
        Task<int> Delete(Guid objectKey);
        Task<List<Category>> GetAll();
        Task<Category> GetById(Guid objectKey);
    }
}
