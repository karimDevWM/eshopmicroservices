namespace Catalog.Api.Products.GetProducts
{
    public record GetProductsQuery(int? PageNumber = 1, int? PageSize = 10) : IQuery<GetProductsResult>;
    public record GetProductsResult(IEnumerable<Product> Products);

    internal class GetProductsQueryHandler(
        IDocumentSession session
    ) : IQueryHandler<GetProductsQuery, GetProductsResult>
    {
        public async Task<GetProductsResult> Handle(GetProductsQuery query, CancellationToken cancellationToken)
        {
            var products = await session.Query<Product>()
                                    // if pageNumber not given, this will be initialized to 1 ad if page size is not given, this will be initialized to 10
                .ToPagedListAsync(query.PageNumber ?? 1, query.PageSize ?? 10,cancellationToken);

            return new GetProductsResult(products);
        }
    }
}
