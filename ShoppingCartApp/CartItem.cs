namespace ShoppingCartApp
{
    public class CartItem
    {
        public string Name { get; }
        public double UnitPrice { get; }
        public int Quantity { get; private set; }

        public CartItem(string name, double unitPrice, int quantity)
        {
           
            if (name == null || name.Trim() == "")
            {
                throw new ArgumentException("Name cannot be null or empty.");
            }
            else if (unitPrice <= 0)
            {
                throw new ArgumentException("Unit price must be greater than zero.");
            }
            else if (quantity < 1)
            {
                throw new ArgumentException("Quantity must be at least 1.");
            }
            else
            {
                Name = name;
                UnitPrice = unitPrice;
                Quantity = quantity;
            }

        }
        public void IncreaseQuantity(int amount)
        {
            Quantity += amount;
        }

        public double GetLineTotal()
        {
            return UnitPrice * Quantity;
        }

        public void UpdateQuantity(int quantity)
        {
            if (quantity < 1)
            {
                throw new ArgumentException("Quantity must be at least 1.");
            }
            else
            {
                Quantity = quantity;
            }
        }
    }
}
