namespace ShoppingCartApp
{
    using System;

    public class Discount
    {
        public double ApplyPercentage(double totalAmount, double percentage)
        {
            if (percentage < 0 || percentage > 100)
                throw new ArgumentException("Percentage should be within 0 and 100.");

            double discountFactor = (100.0 - percentage) / 100.0;
            return totalAmount * discountFactor;
        }

        public double ApplyFixed(double totalAmount, double discountValue)
        {
            if (discountValue < 0)
                throw new ArgumentException("Discount cannot be a negative value.");

            if (totalAmount < discountValue)
                return 0;
                
            return totalAmount - discountValue;
        }

        public bool IsValid(double val)
        {
            return val > 0;
        }
    }
}
