namespace ShoppingBasketApp
{
    using System;

    public class BasketItem
    {
        public string Name { get; }
        public double UnitPrice { get; }
        public int Quantity { get; private set; }

        public BasketItem(string productName, double pricePerUnit, int itemQuantity)
        {
            if (string.IsNullOrWhiteSpace(productName))
                throw new ArgumentException("Product name cannot be empty.");
                
            if (pricePerUnit <= 0)
                throw new ArgumentException("Price must be greater than zero.");
                
            if (itemQuantity < 1)
                throw new ArgumentException("Minimum quantity is 1.");
                
            Name = productName;
            UnitPrice = pricePerUnit;
            Quantity = itemQuantity;
        }

        public void IncreaseQuantity(int amountToAdd)
        {
            Quantity += amountToAdd;
        }

        public double GetLineTotal() => UnitPrice * Quantity;

        public void UpdateQuantity(int newQuantity)
        {
            if (newQuantity < 1)
                throw new ArgumentException("Quantity must be at least 1.");
                
            Quantity = newQuantity;
        }
    }
}
