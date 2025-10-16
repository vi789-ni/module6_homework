using System;

namespace StrategyPatternExample
{
    public interface IPaymentStrategy
    {
        void Pay(double amount);
    }

    public class CreditCardPayment : IPaymentStrategy
    {
        public void Pay(double amount)
        {
            Console.WriteLine($" Оплата {amount} банковской картой прошла успешно.");
        }
    }

    public class PayPalPayment : IPaymentStrategy
    {
        public void Pay(double amount)
        {
            Console.WriteLine($" Оплата {amount} через PayPal прошла успешно.");
        }
    }

    public class CryptoPayment : IPaymentStrategy
    {
        public void Pay(double amount)
        {
            Console.WriteLine($" Оплата {amount} криптовалютой прошла успешно.");
        }
    }

    public class PaymentContext
    {
        private IPaymentStrategy _strategy;

        public void SetStrategy(IPaymentStrategy strategy)
        {
            _strategy = strategy;
        }

        public void ExecutePayment(double amount)
        {
            if (_strategy == null)
            {
                Console.WriteLine(" Стратегия оплаты не выбрана!");
                return;
            }

            _strategy.Pay(amount);
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            PaymentContext context = new PaymentContext();

            Console.WriteLine(" Выбор способа оплаты ");
            Console.WriteLine("1 — Банковская карта");
            Console.WriteLine("2 — PayPal");
            Console.WriteLine("3 — Криптовалюта");
            Console.Write("Введите номер способа оплаты: ");
            string choice = Console.ReadLine();

            switch (choice)
            {
                case "1":
                    context.SetStrategy(new CreditCardPayment());
                    break;
                case "2":
                    context.SetStrategy(new PayPalPayment());
                    break;
                case "3":
                    context.SetStrategy(new CryptoPayment());
                    break;
                default:
                    Console.WriteLine(" Некорректный выбор!");
                    return;
            }

            Console.Write("Введите сумму оплаты: ");
            if (double.TryParse(Console.ReadLine(), out double amount))
            {
                context.ExecutePayment(amount);
            }
            else
            {
                Console.WriteLine(" Ошибка: введена некорректная сумма!");
            }

            Console.WriteLine("\nНажмите любую клавишу для выхода...");
            Console.ReadKey();
        }
    }
}
