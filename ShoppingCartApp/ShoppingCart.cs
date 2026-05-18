namespace ShoppingCartApp
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public class ShoppingCart
    {
        private readonly List<CartItem> _cartItems;

        public ShoppingCart()
        {
            _cartItems = new List<CartItem>();
        }

        public void AddItem(string itemName, double price, int count)
        {
            var foundItem = _cartItems.FirstOrDefault(i => i.Name.Equals(itemName, StringComparison.OrdinalIgnoreCase));

            if (foundItem == null)
            {
                _cartItems.Add(new CartItem(itemName, price, count));
            }
            else
            {
                foundItem.IncreaseQuantity(count);
            }
        }

        public bool RemoveItem(string itemName)
        {
            var target = _cartItems.FirstOrDefault(i => i.Name.Equals(itemName, StringComparison.OrdinalIgnoreCase));
            
            if (target == null) 
                return false;
                
            _cartItems.Remove(target);
            return true;
        }

        public int GetItemCount() => _cartItems.Sum(item => item.Quantity);

        public decimal GetTotal() => (decimal)_cartItems.Sum(item => item.GetLineTotal());

        public IReadOnlyList<CartItem> GetItems() => _cartItems.AsReadOnly();

        public void Clear() => _cartItems.Clear();
    }
}
