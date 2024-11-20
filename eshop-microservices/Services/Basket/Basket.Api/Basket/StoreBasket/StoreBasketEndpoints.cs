
using Basket.Api.Basket.GetBasket;

namespace Basket.Api.Basket.StoreBasket
{
    public record StoreBasketRequest(ShoppingCart Cart);

    public record StoreBasketResponse(string UserName);
    public class StoreBasketEndpoints : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapPost("/basket", async(StoreBasketRequest request, ISender sender) =>
            {
                // convert request to StoreBasketCommand object
                var command = request.Adapt<StoreBasketCommand>();

                // mediatr send object to StoreBasketCommandHandler
                var result = await sender.Send(command);

                // convert result to StoreBasketResponse object
                var response = result.Adapt<StoreBasketResponse>();

                // return result to the basket with username route and the reponse object 
                return Results.Created($"/basket/{response.UserName}", response);
            })
                .WithName("StoreBasket")
                .Produces<GetBasketResponse>(StatusCodes.Status200OK)
                .ProducesProblem(StatusCodes.Status400BadRequest)
                .WithSummary("Store Basket")
                .WithDescription("Store Basket");
        }
    }
}
