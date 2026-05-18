namespace ShoppingCartApp
{
    public class ShoppingCart
    {
        private readonly List<CartItem> _items;

        public ShoppingCart()
        {
            _items = new List<CartItem>();
        }

        public void AddItem(string name, double unitPrice, int quantity)
        {
            var existingItem = _items.FirstOrDefault(a =>
                string.Equals(a.Name, name, StringComparison.OrdinalIgnoreCase));

            if (existingItem != null)
            {
                existingItem.IncreaseQuantity(quantity);
            }
            else
            {
                _items.Add(new CartItem(name, unitPrice, quantity));
            }
        }

        public bool RemoveItem(string name)
        {
            var item = _items.FirstOrDefault(a =>
            string.Equals(a.Name, name, StringComparison.OrdinalIgnoreCase));

            if (item != null)
            {
                _items.Remove(item);
                return true;
            }

            return false;
        }

        public int GetItemCount()
        {
            return _items.Sum(x => x.Quantity);
        }

        public decimal GetTotal()
        {
            return (decimal)_items.Sum(x => x.GetLineTotal());
        }

        public IReadOnlyList<CartItem> GetItems()
        {
            return _items.AsReadOnly();
        }

        public void Clear()
        {
            _items.Clear();
        }
    }
}
