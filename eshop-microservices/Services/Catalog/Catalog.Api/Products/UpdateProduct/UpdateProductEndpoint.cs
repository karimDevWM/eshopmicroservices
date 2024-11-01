
using Catalog.Api.Products.CreateProduct;

namespace Catalog.Api.Products.UpdateProduct
{
    public record UpdateProductRequest(Guid Id, string Name, List<string> Category, string Description, string ImageFile, decimal Price);
    public record UpdateProductResponse(bool IsSuccess);

    public class UpdateProductEndpoint : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapPut("/products", async(UpdateProductRequest request, ISender sender) =>
            {
                // convert request to a update product command object
                var command = request.Adapt<UpdateProductCommand>();

                // mediatr send object to command handler
                var result = await sender.Send(command);

                // convert result to create product response object
                var response = result.Adapt<UpdateProductResponse>();

                // return result to the products with id route and the reponse object 
                return Results.Ok(response);
            })
            .WithName("UpdateProduct")
            .Produces<UpdateProductResponse>(StatusCodes.Status201Created)
            .ProducesProblem(StatusCodes.Status400BadRequest)
            .ProducesProblem(StatusCodes.Status404NotFound)
            .WithSummary("Update Product")
            .WithDescription("Update Product");
        }
    }
}
