using BuildingBlocks.Exceptions;

namespace Catalog.Api.Exceptions
{
    public class ProductNotFoundException : NotFoundException
    {
        // more secific not found exception with the id of the product not found
        public ProductNotFoundException(Guid Id) : base("Product", Id)
        {

        }
    }
}
