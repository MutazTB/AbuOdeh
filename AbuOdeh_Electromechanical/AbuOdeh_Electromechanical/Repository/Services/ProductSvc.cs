using AbuOdeh_Electromechanical.Models;
using AbuOdeh_Electromechanical.Repository.Data;
using AbuOdeh_Electromechanical.Repository.Entities;
using AutoMapper;
using Microsoft.EntityFrameworkCore;

namespace AbuOdeh_Electromechanical.Repository.Services
{
    public class ProductSvc : IProductSvc
    {
        private readonly AbuOdehDBContext _repository;
        public ProductSvc(AbuOdehDBContext repository)
        {
            _repository = repository;
        }
        public async Task<int> Add(Product product)
        {
            await _repository.AddAsync(product);
            await _repository.SaveChangesAsync();
            return product.Id;
        }

        public async Task<int> Delete(Guid objectKey)
        {
            var product = await _repository.Products.FirstOrDefaultAsync(x => x.ObjectKey == objectKey);
            if (product == null)
            {
                return 0;
            }
            product.IsDeleted = true;
            var result = await _repository.SaveChangesAsync();
            if (result > 0)
            {
                return product.Id;
            }
            return 0;
        }

        public async Task<List<Product>> GetAll()
        {
            var products = await _repository.Products.AsQueryable().ToListAsync();
            return products;
        }

        public async Task<List<Product>> GetAll(ProductPagenation productPagenation)
        {
            int pageSize = 12;
            int pageIndex = productPagenation.PageIndex ?? 0;

            var query = _repository.Products.AsQueryable();

            // Apply filters
            if (productPagenation.CategoryId.HasValue)
            {
                query = query.Where(x => x.CategoryId == productPagenation.CategoryId.Value);
            }
            if (productPagenation.FromPrice.HasValue)
            {
                query = query.Where(x => x.Price >= productPagenation.FromPrice.Value);
            }
            if (productPagenation.ToPrice.HasValue)
            {
                query = query.Where(x => x.Price <= productPagenation.ToPrice.Value);
            }

            // Apply pagination
            query = query.Skip(pageIndex * 12).Take(12);

            var products = await query.ToListAsync();
            return products;            
        }

        public async Task<Product> GetById(Guid objectKey)
        {
            var product = await _repository.Products.FirstOrDefaultAsync(x => x.ObjectKey == objectKey);
            return product;
        }

        public async Task<int> Update(Product product)
        {
            var productToBeUpdadte = await GetById(product.ObjectKey);
            if (productToBeUpdadte == null)
            {
                return -1;
            }
            productToBeUpdadte.Price = product.Price;
            productToBeUpdadte.DescriptionAr = product.DescriptionAr;
            productToBeUpdadte.DescriptionEn = product.DescriptionEn;
            productToBeUpdadte.NameEn = product.NameEn;
            productToBeUpdadte.NameAr = product.NameAr;
            productToBeUpdadte.ImageName = product.ImageName;

            var result = await _repository.SaveChangesAsync();
            return result;
        }
    }
}
