using System;
using System.Collections.Generic;

namespace ObserverPatternExample
{
    public interface IObserver
    {
        void Update(string currency, double rate);
    }

    public interface ISubject
    {
        void Attach(IObserver observer);
        void Detach(IObserver observer);
        void Notify(string currency, double rate);
    }

    public class CurrencyExchange : ISubject
    {
        private List<IObserver> observers = new List<IObserver>();

        public void Attach(IObserver observer)
        {
            observers.Add(observer);
        }

        public void Detach(IObserver observer)
        {
            observers.Remove(observer);
        }

        public void Notify(string currency, double rate)
        {
            foreach (var observer in observers)
            {
                observer.Update(currency, rate);
            }
        }

        public void ChangeRate(string currency, double newRate)
        {
            Console.WriteLine($"\n Новый курс {currency}: {newRate}");
            Notify(currency, newRate);
        }
    }

    public class MobileApp : IObserver
    {
        public void Update(string currency, double rate)
        {
            Console.WriteLine($" Мобильное приложение: курс {currency} обновлён до {rate}");
        }
    }

    public class BankDisplay : IObserver
    {
        public void Update(string currency, double rate)
        {
            Console.WriteLine($" Электронное табло банка: курс {currency} теперь {rate}");
        }
    }

    public class NewsAgency : IObserver
    {
        public void Update(string currency, double rate)
        {
            Console.WriteLine($" Новостное агентство сообщает: курс {currency} изменился на {rate}");
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            CurrencyExchange exchange = new CurrencyExchange();

            IObserver mobileApp = new MobileApp();
            IObserver bankDisplay = new BankDisplay();
            IObserver newsAgency = new NewsAgency();

            exchange.Attach(mobileApp);
            exchange.Attach(bankDisplay);
            exchange.Attach(newsAgency);

            Console.WriteLine(" Симуляция обновления курса валют ");
            while (true)
            {
                Console.Write("\nВведите валюту (например, USD, EUR) или 'exit' для выхода: ");
                string currency = Console.ReadLine();

                if (currency.ToLower() == "exit")
                    break;

                Console.Write($"Введите новый курс для {currency}: ");
                if (double.TryParse(Console.ReadLine(), out double rate))
                {
                    exchange.ChangeRate(currency.ToUpper(), rate);
                }
                else
                {
                    Console.WriteLine(" Ошибка: некорректное значение курса!");
                }
            }

            Console.WriteLine("\nПрограмма завершена. Нажмите любую клавишу...");
            Console.ReadKey();
        }
    }
}
