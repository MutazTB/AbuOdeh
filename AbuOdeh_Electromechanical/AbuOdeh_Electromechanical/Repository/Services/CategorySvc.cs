using AbuOdeh_Electromechanical.Repository.Data;
using AbuOdeh_Electromechanical.Repository.Entities;
using AutoMapper;
using Microsoft.EntityFrameworkCore;

namespace AbuOdeh_Electromechanical.Repository.Services
{
    public class CategorySvc : ICategorySvc
    {
        private readonly AbuOdehDBContext _repository;
        public CategorySvc(AbuOdehDBContext repository) 
        {
            _repository = repository;
        }
        public async Task<int> Add(Category category)
        {
            await _repository.AddAsync(category);
            await _repository.SaveChangesAsync();
            return category.Id;
        }

        public async Task<int> Delete(Guid objectKey)
        {
            var employee = await _repository.Categories.FirstOrDefaultAsync(x => x.ObjectKey == objectKey && x.IsDeleted == false);
            if (employee == null)
            {
                return 0;
            }
            employee.IsDeleted = true;
            var result = await _repository.SaveChangesAsync();
            if (result > 0)
            {
                return employee.Id;
            }
            return 0;
        }

        public async Task<List<Category>> GetAll()
        {
            var categories = await _repository.Categories.AsQueryable().Where(x => x.IsDeleted == false).ToListAsync();
            return categories;
        }

        public async Task<Category> GetById(Guid objectKey)
        {
            var category = await _repository.Categories.FirstOrDefaultAsync(x => x.ObjectKey == objectKey && x.IsDeleted == false);
            return category;
        }

        public async Task<int> Update(Category category)
        {
            var categoryToBeUpdadte = await GetById(category.ObjectKey);
            if (categoryToBeUpdadte == null)
            {
                return -1;
            }
            categoryToBeUpdadte.NameEn = category.NameEn;
            categoryToBeUpdadte.NameAr = category.NameAr;
            var result = await _repository.SaveChangesAsync();
            return result;
        }
    }
}
