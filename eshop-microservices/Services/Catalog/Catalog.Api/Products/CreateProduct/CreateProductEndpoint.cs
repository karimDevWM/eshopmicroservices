namespace Catalog.Api.Products.CreateProduct
{
    // request object
    public record CreateProductRequest(string Name, List<string> Category, string Description, string ImageFile, decimal Price);

    // response object
    public record CreateProductResponse(Guid Id);

    public class CreateProductEndpoint : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapPost("/products",
                async (CreateProductRequest request, ISender sender) =>
            {
                // convert request to create product command object
                var command = request.Adapt<CreateProductCommand>();

                // mediatr send object to command handler
                var result = await sender.Send(command);

                // convert result to create product response object
                var response = result.Adapt<CreateProductResponse>();

                // return result to the products with id route and the reponse object 
                return Results.Created($"/products/{response.Id}", response);
            })
                .WithName("CreateProduct")
                .Produces<CreateProductResponse>(StatusCodes.Status201Created)
                .ProducesProblem(StatusCodes.Status400BadRequest)
                .WithSummary("Create Product")
                .WithDescription("Create Product");
        }
    }
}
