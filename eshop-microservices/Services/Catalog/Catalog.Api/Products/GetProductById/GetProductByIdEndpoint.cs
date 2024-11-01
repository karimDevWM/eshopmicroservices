
using Catalog.Api.Products.GetProducts;

namespace Catalog.Api.Products.GetProductById
{
    //public record GetProductsRequest();

    public record GetProductByIdResponse(Product Product);

    public class GetProductByIdEndpoint : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapGet("/products/{id}", async (Guid id, ISender sender) =>
            {
                // mediatr send object to command handler and having the response
                var result = await sender.Send(new GetProductByIdQuery(id));

                // convert the mediatr result object to product response
                var response = result.Adapt<GetProductByIdResponse>();

                return Results.Ok(response);
            })
                .WithName("GetProductById")
                .Produces<GetProductByIdResponse>(StatusCodes.Status200OK)
                .ProducesProblem(StatusCodes.Status400BadRequest)
                .WithSummary("Get Product By Id")
                .WithDescription("Get Product By Id");
        }
    }
}
