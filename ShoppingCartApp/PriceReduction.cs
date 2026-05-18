namespace ShoppingBasketApp
{
    using System;

    public class PriceReduction
    {
        public double ApplyPercentage(double totalAmount, double percentage)
        {
            if (percentage < 0 || percentage > 100)
                throw new ArgumentException("Percentage should be within 0 and 100.");

            double pricereductionFactor = (100.0 - percentage) / 100.0;
            return totalAmount * pricereductionFactor;
        }

        public double ApplyFixed(double totalAmount, double pricereductionValue)
        {
            if (pricereductionValue < 0)
                throw new ArgumentException("PriceReduction cannot be a negative value.");

            if (totalAmount < pricereductionValue)
                return 0;
                
            return totalAmount - pricereductionValue;
        }

        public bool IsValid(double val)
        {
            return val > 0;
        }
    }
}
