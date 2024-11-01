﻿
namespace Catalog.Api.Products.GetProducts
{

    public record GetProductsRequest(int? PageNumber = 1, int? PageSize = 10);

    public record GetProductsResponse(IEnumerable<Product> Products);

    public class GetProductsEndpoint : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapGet("/products", async ([AsParameters] GetProductsRequest request, ISender sender) =>
            {
                var query = request.Adapt<GetProductsQuery>();

                // mediatr send object to command handler and having the response
                var result = await sender.Send(query);

                // convert the mediatr result object to product response
                var response = result.Adapt<GetProductsResponse>();

                return Results.Ok(response);
            })
                .WithName("GetProducts")
                .Produces<GetProductsResponse>(StatusCodes.Status200OK)
                .ProducesProblem(StatusCodes.Status400BadRequest)
                .WithSummary("Get Products")
                .WithDescription("Get Products");
        }
    }
}