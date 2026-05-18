namespace ShoppingCartApp
{
    public class Discount
    {
        // percent: 0–100 között, különben ArgumentException
        // Példa: ApplyPercentage(200, 10) -> 180
        public double ApplyPercentage(double total, double percent)
        {
            if (percent < 0 || percent > 100)
            {
                throw new ArgumentException("Percent must be between 0 and 100.");
            }

            return total * ((100 - percent) / 100);
        }

        // Az eredmény soha nem lehet negatív — ha a kedvezmény nagyobb, 0-t ad vissza
        // Példa: ApplyFixed(100, 50) -> 50
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

        // true ha discountValue > 0
        public bool IsValid(double discountValue)
        {
            if (discountValue > 0)
                return true;
            else
                return false;
        }
    }
}
