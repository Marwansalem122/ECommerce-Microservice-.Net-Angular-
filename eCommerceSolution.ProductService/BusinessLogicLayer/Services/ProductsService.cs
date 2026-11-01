
using AutoMapper;
using eCommerce.BusinessLogicLayer.DTO;
using eCommerce.BusinessLogicLayer.ServiceContract;
using eCommerce.DataAccessLayer.Entities;
using eCommerce.DataAccessLayer.RepositoryContracts;
using FluentValidation;
using FluentValidation.Results;
using System.Linq.Expressions;

namespace eCommerce.BusinessLogicLayer.Services
{
    public class ProductsService(
        IProductsRepository productsRepository,
        IMapper mapper,
         IValidator<ProductAddRequest> productAddRequest,
        IValidator<ProductUpdateRequest> productUpdateRequest
        ) : IProductService
    {
        private readonly IProductsRepository _productsRepository = productsRepository;
        private readonly IMapper _mapper = mapper;
        private readonly IValidator<ProductAddRequest> _productAddRequest
                                           = productAddRequest;
        private readonly IValidator<ProductUpdateRequest> _productUpdateRequest
                                           = productUpdateRequest;


        public async Task<ProductResponse?> AddProduct(ProductAddRequest request)
        {
            if (request != null)
            {
                ValidationResult validationResult = await _productAddRequest.ValidateAsync(request);
                if (!validationResult.IsValid)
                {
                    string errors = string.Join(", ",
                        validationResult.Errors.Select(temp => temp.ErrorMessage));
                    throw new ArgumentException(errors);
                }
                Product productInput = _mapper.Map<Product>(request);

                Product? product =
                    await _productsRepository.AddProduct(productInput);
                if (product == null)
                    return null;
                return _mapper.Map<ProductResponse>(product);
            }
            throw new ArgumentNullException(nameof(request));
        }

        public async Task<bool> DeleteProduct(Guid productID)
        {
            return await _productsRepository.DeleteProduct(productID);
        }

        public async Task<ProductResponse?> GetProductByCondition(Expression<Func<Product, bool>> expression)
        {
            Product? product = await _productsRepository
      .GetProductByCondition(expression);

            if (product is null)
                return null;

            return _mapper.Map<ProductResponse>(product);
        }

        public async Task<List<ProductResponse?>> GetProducts()
        {
            var products = await _productsRepository.GetProducts();
            return _mapper.Map<List<ProductResponse?>>(products);
        }

        public async Task<List<ProductResponse?>> GetProductsByCondition(Expression<Func<Product, bool>> expression)
        {
            var products = await _productsRepository
        .GetProductsByCondition(expression);

            return _mapper.Map<List<ProductResponse?>>(products);
        }

        public async Task<ProductResponse?> UpdateProduct(ProductUpdateRequest productRequest)
        {
            if (productRequest != null)
            {
                ValidationResult validationResult = await _productUpdateRequest.ValidateAsync(productRequest);
                if (!validationResult.IsValid)
                {
                    string errors = string.Join(", ",
                        validationResult.Errors.Select(temp => temp.ErrorMessage));
                    throw new ArgumentException(errors);
                }
                Product? product = await _productsRepository.UpdateProduct(_mapper.Map<Product>(productRequest));
                if (product == null)
                    return null;
                return _mapper.Map<ProductResponse>(product);
            }

            throw new ArgumentNullException(nameof(productRequest));


        }
    }
}
