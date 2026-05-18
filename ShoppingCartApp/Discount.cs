namespace ShoppingCartApp
{
    public class Discount
    {
        public double ApplyPercentage(double total, double percent)
        {
            if (percent < 0 || percent > 100)
            {
                throw new ArgumentException("Percent must be between 0 and 100.");
            }

            return total * ((100 - percent) / 100);
        }

        public double ApplyFixed(double total, double discountAmount)
        {
            if (discountAmount < 0)
            {
                throw new ArgumentException("Discount amount cannot be negative.");
            }
            if (discountAmount > total)
            {
                return 0;
            }
            else
            {
                return total - discountAmount;
            }
        }

        public bool IsValid(double discountValue)
        {
            if (discountValue > 0)
                return true;
            else
                return false;
        }
    }
}
