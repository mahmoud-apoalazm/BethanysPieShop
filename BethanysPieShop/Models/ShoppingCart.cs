using Microsoft.EntityFrameworkCore;

namespace BethanysPieShop.Models
{
    public class ShoppingCart : IShoppingCart
    {
        private readonly BethanyPieShopDbContext _bethanypieshopDbContext;
        public string? ShoppingCartId { get; set; }
        public List<ShoppingCartItem> ShoppingCartItems { get; set; } = default!;

        public ShoppingCart(BethanyPieShopDbContext bethanypieshopDbContext)
        {
            _bethanypieshopDbContext = bethanypieshopDbContext;
           
        }
        public static ShoppingCart GetCart(IServiceProvider services)
        {
            ISession? session = services.GetRequiredService<IHttpContextAccessor>()?.HttpContext?.Session;

            BethanyPieShopDbContext context = services.GetService<BethanyPieShopDbContext>() ?? throw new Exception("Error initializing");

            string cartId = session?.GetString("CartId") ?? Guid.NewGuid().ToString();

            session?.SetString("CartId", cartId);

            return new ShoppingCart(context) { ShoppingCartId = cartId };
        }


        public void AddToCart(Pie pie)
        {
            var shoppingCartItem =
                   _bethanypieshopDbContext.ShoppingCartItems.SingleOrDefault(
                       s => s.Pie.PieId == pie.PieId && s.ShoppingCartId == ShoppingCartId);

            if (shoppingCartItem == null)
            {
                shoppingCartItem = new ShoppingCartItem
                {
                    ShoppingCartId = ShoppingCartId,
                    Pie = pie,
                    Amount = 1
                };

                _bethanypieshopDbContext.ShoppingCartItems.Add(shoppingCartItem);
            }
            else
            {
                shoppingCartItem.Amount++;
            }
            _bethanypieshopDbContext.SaveChanges();
        }

        public void ClearCart()
        {
            var cartItems = _bethanypieshopDbContext.ShoppingCartItems
                .Where(cart => cart.ShoppingCartId == ShoppingCartId);

            _bethanypieshopDbContext.ShoppingCartItems.RemoveRange(cartItems);

            _bethanypieshopDbContext.SaveChanges();
        }

        public List<ShoppingCartItem> GetShoppingCartItems()
        {
            return ShoppingCartItems ??= _bethanypieshopDbContext.ShoppingCartItems
                .Where(c => c.ShoppingCartId == ShoppingCartId)
                .Include(s => s.Pie)
                .ToList();
          
        }

        public decimal GetShoppingCartTotal()
        {
            var total = _bethanypieshopDbContext.ShoppingCartItems.Where(c => c.ShoppingCartId == ShoppingCartId)
                 .Select(c => c.Pie.Price * c.Amount).Sum();

            return total;
        }

        public int RemoveFromCart(Pie pie)
        {
            var shoppingCarItem = _bethanypieshopDbContext.ShoppingCartItems
                 .SingleOrDefault(s => s.Pie.PieId == pie.PieId && s.ShoppingCartId == ShoppingCartId);
            var localAmount = 0;
            if(shoppingCarItem != null)
            {
                if(shoppingCarItem.Amount > 1)
                {
                    shoppingCarItem.Amount--;
                    localAmount=shoppingCarItem.Amount;
                }
                else
                {
                    _bethanypieshopDbContext.ShoppingCartItems.Remove(shoppingCarItem);
                }
            }
            _bethanypieshopDbContext.SaveChanges();

            return localAmount;
        }
    }
}
