

using eCommerce.DataAccessLayer.Entities;
using eCommerce.DataAccessLayer.Context;
using eCommerce.DataAccessLayer.RepositoryContracts;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace eCommerce.DataAccessLayer.Repositories
{
    public class ProductsRepository(ApplicationDbContext dbContext) : IProductsRepository
    {
        private readonly ApplicationDbContext _dbContext=dbContext;
        public async Task<Product?> AddProduct(Product product)
        {
             _dbContext.Products.Add(product);
            await _dbContext.SaveChangesAsync();
            return product;
           
        }

        public async Task<bool> DeleteProduct(Guid productID)
        {
            int affectedRows = await _dbContext.Products
        .Where(p => p.ProductID == productID)
        .ExecuteDeleteAsync();

            return affectedRows > 0;
        }

        public async Task<Product?> GetProductByCondition(Expression<Func<Product, bool>> expression)
        {
            return await _dbContext.Products.FirstOrDefaultAsync(expression);
        }

        public async Task<IEnumerable<Product>> GetProducts()
        {
            return await _dbContext.Products.ToListAsync();
        }

        public async Task<IEnumerable<Product?>> GetProductsByCondition(Expression<Func<Product, bool>> expression)
        {
            return await _dbContext.Products.Where(expression).ToListAsync();
        }
        
        public async Task<Product?> UpdateProduct(Product product)
        {
            Product?existingProduct=await _dbContext.Products.FirstOrDefaultAsync(temp => temp.ProductID == product.ProductID);
            if (existingProduct == null)
                return null;

            //existingProduct.ProductID = product.ProductID;
            //existingProduct.ProductName = product.ProductName;
            //existingProduct.UnitPrice = product.UnitPrice;
            //existingProduct.QuantityInStock = product.QuantityInStock;
            //existingProduct.Category = product.Category;
            _dbContext.Entry(existingProduct)
                      .CurrentValues.SetValues(product);

            await _dbContext.SaveChangesAsync();
            return existingProduct;
        }
    }
}
