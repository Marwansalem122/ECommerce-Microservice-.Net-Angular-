
using eCommerce.BusinessLogicLayer.DTO;
using eCommerce.BusinessLogicLayer.ServiceContract;
using FluentValidation;
using FluentValidation.Results;
using Microsoft.AspNetCore.Mvc;

namespace ProductMicroService.API.APIEndPoints
{
    public static class ProductAPIEndPoints
    {
        public static IEndpointRouteBuilder MapProductAPIEndPoints(
            this IEndpointRouteBuilder app)
        {
            var products = app.MapGroup("/api/products")
                .WithTags("Products");

            // GET /api/products
            products.MapGet("/", async (
                IProductService productService) =>
            {
                var result = await productService.GetProducts();

                return Results.Ok(result);
            })
            .WithName("GetProducts")
            .Produces(StatusCodes.Status200OK);

            // GET /api/products/search?category=Electronics
            products.MapGet("/search",
                async (
                    string? category,
                    double? minPrice,
                    IProductService productService) =>
                {
                    var products = await productService.GetProductsByCondition(
                        p => (string.IsNullOrEmpty(category)
                                || p.Category == category)
                            && (!minPrice.HasValue
                                || p.UnitPrice >= minPrice.Value));

                    return Results.Ok(products);
                });
            // GET /api/products/{productID}
            products.MapGet("/{productID:guid}",
                async (
                    Guid productID,
                    IProductService productService) =>
                {
                    var product = await productService.GetProductByCondition(
                        p => p.ProductID == productID);

                    if (product is null)
                        return Results.NotFound();

                    return Results.Ok(product);
                })
            .WithName("GetProductById")
            .Produces<ProductResponse>(StatusCodes.Status200OK)
            .Produces(StatusCodes.Status404NotFound);

            // POST /api/products
            app.MapPost("/", async (
                ProductAddRequest productAddRequest,
                IProductService productService,
                IValidator<ProductAddRequest> productAddRequestValidator) =>
            {
                ValidationResult validationResult =
                    await productAddRequestValidator
                        .ValidateAsync(productAddRequest);

                if (!validationResult.IsValid)
                {
                    var errors = validationResult.Errors
                        .Select(e => new
                        {
                            e.PropertyName,
                            e.ErrorMessage
                        });

                    return Results.BadRequest(errors);
                }

                var product =
                    await productService.AddProduct(productAddRequest);

                if (product == null)
                    return Results.Problem("Failed in Add Product");
                return Results.Created(
                    $"/api/products/{product!.ProductID}",
                    product);
            });

            // PUT /api/products/{productID}
            products.MapPut("/{productID:guid}",
                async (
                    Guid productID,
                    [FromBody] ProductUpdateRequest request,
                    IProductService productService) =>
                {
                    if (productID != request.ProductID)
                    {
                        return Results.BadRequest(
                            "Route ProductID does not match request ProductID.");
                    }

                    var product = await productService.UpdateProduct(request);

                    if (product is null)
                        return Results.NotFound();

                    return Results.Ok(product);
                })
            .WithName("UpdateProduct")
            .Produces<ProductResponse>(StatusCodes.Status200OK)
            .Produces(StatusCodes.Status400BadRequest)
            .Produces(StatusCodes.Status404NotFound);

            // DELETE /api/products/{productID}
            products.MapDelete("/{productID:guid}",
                async (
                    Guid productID,
                    IProductService productService) =>
                {
                    bool deleted = await productService.DeleteProduct(productID);

                    if (!deleted)
                        return Results.NotFound();

                    return Results.NoContent();
                })
            .WithName("DeleteProduct")
            .Produces(StatusCodes.Status204NoContent)
            .Produces(StatusCodes.Status404NotFound);

            return app;
        }
    }
}