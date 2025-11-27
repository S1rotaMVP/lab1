using System;
using System.Collections.Generic;
using System.Text;

namespace ConsoleApp1
{
    struct FeedItem
    {
        public string Name;
        public double PricePerKg;
        public double Weight;
        public bool IsPremium;
    }

    internal class Program
    {
        static void Main(string[] args)
        {
            Console.OutputEncoding = Encoding.UTF8;
            Console.InputEncoding = Encoding.UTF8;

            if (!Login())
            {
                return;
            }

            bool isRunning = true;
            while (isRunning)
            {
                Console.Clear();
                ShowMenu();

                Console.Write("\n Виберіть опцію (1-8): ");
                string choice = Console.ReadLine();

                switch (choice)
                {
                    case "1":
                        ManageAnimals();
                        break;
                    case "2":
                        ManageSpecies();
                        break;
                    case "3":
                        ManageCaretakers();
                        break;
                    case "4":
                        ManageFeedingSchedule();
                        break;
                    case "5":
                        ManageHealthRecords();
                        break;
                    case "6":
                        FilterBySpecies();
                        break;
                    case "7":
                        PurchaseFeed();
                        break;
                    case "8":
                        Console.WriteLine("Вихід з програми. До побачення!");
                        isRunning = false;
                        break;
                    default:
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("Невірний вибір. Спробуйте ще раз.");
                        Console.ResetColor();
                        Pause();
                        break;
                }
            }
        }

        static bool Login()
        {
            string pass = "12345";
            int attempts = 3;

            do
            {
                Console.Clear();
                Console.ForegroundColor = ConsoleColor.Cyan;
                Console.WriteLine("=== ВХІД У СИСТЕМУ ЗООПАРКУ ===");
                Console.ResetColor();
                Console.Write("Введіть пароль (адмін): ");
                string input = Console.ReadLine();

                if (input == pass)
                {
                    Console.WriteLine("Доступ дозволено!");
                    System.Threading.Thread.Sleep(1000);
                    return true;
                }
                else
                {
                    attempts--;
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine($"Невірний пароль! Залишилось спроб: {attempts}");
                    Console.ResetColor();
                    Console.ReadKey();
                }

            } while (attempts > 0);

            Console.WriteLine("Спроби вичерпано. До побачення.");
            return false;
        }

        static void PurchaseFeed()
        {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("=== ЗАКУПІВЛЯ КОРМІВ ===");
            Console.ResetColor();

            List<FeedItem> currentPurchase = new List<FeedItem>();
            double budgetLimit = 10000;
            double currentSpent = 0;

            Console.Write("Скільки видів корму ви хочете додати? (мінімум 5): ");
            int count;
            while (!int.TryParse(Console.ReadLine(), out count) || count < 1)
            {
                Console.WriteLine("Будь ласка, введіть коректне число (більше 0).");
            }

            for (int i = 0; i < count; i++)
            {
                Console.WriteLine($"\n--- Товар #{i + 1} ---");
                FeedItem item = new FeedItem();

                Console.Write("Назва корму: ");
                item.Name = Console.ReadLine();

                if (item.Name.Length < 3)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Помилка: Назва надто коротка! Цей товар пропущено.");
                    Console.ResetColor();
                    continue;
                }

                do
                {
                    Console.Write("Ціна за кг (грн): ");
                } while (!double.TryParse(Console.ReadLine(), out item.PricePerKg) || item.PricePerKg <= 0);

                do
                {
                    Console.Write("Вага (кг): ");
                } while (!double.TryParse(Console.ReadLine(), out item.Weight) || item.Weight <= 0);

                double cost = item.PricePerKg * item.Weight;

                if (currentSpent + cost > budgetLimit)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine($"УВАГА: Ліміт бюджету ({budgetLimit}) перевищено! Закупівлю зупинено.");
                    Console.ResetColor();
                    break;
                }

                Console.Write("Це преміум корм? (так/ні): ");
                string answer = Console.ReadLine().ToLower();
                item.IsPremium = (answer == "так" || answer == "yes" || answer == "+");

                currentSpent += cost;
                currentPurchase.Add(item);
            }

            Console.Clear();
            Console.WriteLine("=== ЗВІТ ПО ЗАКУПІВЛІ ===");

            double totalSum = 0;
            double maxPrice = 0;
            string mostExpensiveName = "";
            int premiumCount = 0;

            Console.WriteLine("{0,-15} | {1,-10} | {2,-10} | {3,-10}", "Назва", "Ціна", "Вага", "Сума");
            Console.WriteLine(new string('-', 55));

            foreach (var item in currentPurchase)
            {
                double itemTotal = item.PricePerKg * item.Weight;
                totalSum += itemTotal;

                if (item.PricePerKg > maxPrice)
                {
                    maxPrice = item.PricePerKg;
                    mostExpensiveName = item.Name;
                }

                if (item.IsPremium)
                {
                    premiumCount++;
                }

                Console.WriteLine("{0,-15} | {1,-10} | {2,-10} | {3,-10}",
                    item.Name, item.PricePerKg, item.Weight, itemTotal);
            }

            double averagePrice = currentPurchase.Count > 0 ? totalSum / currentPurchase.Count : 0;

            Console.WriteLine(new string('=', 55));
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine($"ЗАГАЛЬНА СУМА ЗАКУПІВЛІ: {totalSum} грн");
            Console.WriteLine($"Середня вартість позиції: {Math.Round(averagePrice, 2)} грн");
            Console.WriteLine($"Найдорожчий корм (за кг): {mostExpensiveName} ({maxPrice} грн)");
            Console.WriteLine($"Кількість преміум-кормів: {premiumCount}");
            Console.ResetColor();

            Pause();
        }

        static void ShowMenu()
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("=============================================\r\n    Ласкаво просимо до зоопарку         \r\n             \"ДИКИЙ СВІТ\"   \r\n==============================================");
            Console.ResetColor();
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("\n МЕНЮ:");
            Console.WriteLine("1. Введення даних про тварин");
            Console.WriteLine("2. Ведення даних про види");
            Console.WriteLine("3. Ведення даних про доглядачів");
            Console.WriteLine("4. Графік годування");
            Console.WriteLine("5. Фіксація стану здоров’я тварин");
            Console.WriteLine("6. Фільтр тварин за видом");
            Console.WriteLine("7. Закупівля кормів (РОБОЧИЙ ПУНКТ)");
            Console.WriteLine("8. Вихід");
            Console.ResetColor();
        }

        static void ManageAnimals()
        { Console.WriteLine("В розробці..."); Pause(); }
        static void ManageSpecies()
        { Console.WriteLine("В розробці..."); Pause(); }
        static void ManageCaretakers()
        { Console.WriteLine("В розробці..."); Pause(); }
        static void ManageFeedingSchedule()
        { Console.WriteLine("В розробці..."); Pause(); }
        static void ManageHealthRecords()
        { Console.WriteLine("В розробці..."); Pause(); }
        static void FilterBySpecies() 
        { Console.WriteLine("В розробці..."); Pause(); }

        static void Pause()
        {
            Console.ResetColor();
            Console.WriteLine("\nНатисніть Enter, щоб продовжити...");
            Console.ReadLine();
        }
    }
}