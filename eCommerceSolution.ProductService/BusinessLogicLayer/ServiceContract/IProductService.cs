
using eCommerce.BusinessLogicLayer.DTO;
using eCommerce.DataAccessLayer.Entities;
using System.Linq.Expressions;

namespace eCommerce.BusinessLogicLayer.ServiceContract
{
    public interface IProductService
    {
        Task<List<ProductResponse?>> GetProducts();

        Task<List<ProductResponse?>> GetProductsByCondition(Expression<Func<Product, bool>> expression);
        Task<ProductResponse?> GetProductByCondition(Expression<Func<Product, bool>> expression);
        Task<ProductResponse?> AddProduct(ProductAddRequest product);
        Task<ProductResponse?> UpdateProduct(ProductUpdateRequest product);
        Task<bool> DeleteProduct(Guid productID);
    }
}
