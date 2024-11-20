namespace Basket.Api.Models
{
    public class ShoppingCart
    {
        // primary key of ShoppingCart table
        public string UserName { get; set; } = default!;

        // Relation OneToMany : a ShoppingCart can contain many ShoppingCartItems
        public List<ShoppingCartItem> Items { get; set; } = new();

        public decimal TotalPrice => Items.Sum(x => x.Price * x.Quantity);

        public ShoppingCart(string userName)
        {
            UserName = userName;
        }

        // Required for Mapping
        public ShoppingCart()
        {
            
        }
    }
}
