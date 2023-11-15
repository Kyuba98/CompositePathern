using static CompositePathern.Program;

namespace CompositePathern
{
    internal class Program
    {
        static void Main()
        {
            // Creating individual discounts
            var fixedDiscount1 = new FixedDiscount(3);
            var fixedDiscount2 = new FixedDiscount(7);
            var percentageDiscount = new PercentageDiscount(10);

            // Creating a composite discount
            var compositeDiscount1 = new CompositeDiscount();
            compositeDiscount1.AddDiscount(fixedDiscount1);
            compositeDiscount1.AddDiscount(percentageDiscount);
            var compositeDiscount2 = new CompositeDiscount();
            compositeDiscount2.AddDiscount(fixedDiscount2);
            compositeDiscount2.AddDiscount(compositeDiscount1);

            double originalAmount = 100;
            double finalAmount = compositeDiscount2.ApplyDiscount(originalAmount);

            Console.WriteLine($"Oryginalna cena: {originalAmount} PLN");
            Console.WriteLine($"Cena końcowa: {finalAmount} PLN");
        }
        public interface IDiscount
        {
            double ApplyDiscount(double amount);
        }
        public class FixedDiscount : IDiscount
        {
            private double _fixedAmount;

            public FixedDiscount(double fixedAmount)
            {
                _fixedAmount = fixedAmount;
            }

            public double ApplyDiscount(double amount)
            {
                return amount - _fixedAmount;
            }
        }

        public class PercentageDiscount : IDiscount
        {
            private double _percentage;

            public PercentageDiscount(double percentage)
            {
                _percentage = percentage;
            }

            public double ApplyDiscount(double amount)
            {
                return amount * (1 - _percentage / 100);
            }
        }



    public class CompositeDiscount : IDiscount
    {
        private List<IDiscount> _discounts = new List<IDiscount>();

        public void AddDiscount(IDiscount discount)
        {
            _discounts.Add(discount);
        }

        public double ApplyDiscount(double amount)
        {
            double discountedAmount = amount;
            foreach (var discount in _discounts)
            {
                discountedAmount = discount.ApplyDiscount(discountedAmount);
            }
            return discountedAmount;
        }
    }

}
}