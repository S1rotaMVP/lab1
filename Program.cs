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
        static List<FeedItem> inventory = new List<FeedItem>();

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
                ShowMainMenu();

                Console.Write("\n Виберіть опцію (1-8): ");
                string choice = Console.ReadLine();

                switch (choice)
                {
                    case "1": ManageAnimals(); break;
                    case "2": ManageSpecies(); break;
                    case "3": ManageCaretakers(); break;
                    case "4": ManageFeedingSchedule(); break;
                    case "5": ManageHealthRecords(); break;
                    case "6": FilterBySpecies(); break;
                    case "7": FeedInventoryMenu(); break;
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
            string correctLogin = "admin";
            string correctPass = "12345";
            int attempts = 3;

            do
            {
                Console.Clear();
                Console.ForegroundColor = ConsoleColor.Cyan;
                Console.WriteLine("=== ВХІД У СИСТЕМУ ЗООПАРКУ ===");
                Console.ResetColor();

                Console.Write("Введіть логін: ");
                string inputLogin = Console.ReadLine();

                Console.Write("Введіть пароль: ");
                string inputPass = Console.ReadLine();

                if (inputLogin == correctLogin && inputPass == correctPass)
                {
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine("Доступ дозволено!");
                    Console.ResetColor();
                    System.Threading.Thread.Sleep(1000);
                    return true;
                }
                else
                {
                    attempts--;
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine($"Помилка! Невірний логін або пароль. Залишилось спроб: {attempts}");
                    Console.ResetColor();
                    Console.ReadKey();
                }

            } while (attempts > 0);

            Console.WriteLine("Спроби вичерпано. До побачення.");
            return false;
        }

        static void FeedInventoryMenu()
        {
            bool backToMain = false;
            while (!backToMain)
            {
                Console.Clear();
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine("=== СКЛАД КОРМІВ ===");
                Console.ResetColor();
                Console.WriteLine("1. Додати елементи (мінімум 5)");
                Console.WriteLine("2. Вивести всі елементи");
                Console.WriteLine("3. Пошук елемента");
                Console.WriteLine("4. Видалення елемента");
                Console.WriteLine("5. Сортування");
                Console.WriteLine("6. Статистика");
                Console.WriteLine("0. Повернутися в головне меню");
                Console.WriteLine("======================================");
                Console.Write("Ваш вибір: ");

                string subChoice = Console.ReadLine();

                switch (subChoice)
                {
                    case "1": AddItems(); break;
                    case "2": PrintTable(); Pause(); break;
                    case "3": SearchItem(); break;
                    case "4": DeleteItem(); break;
                    case "5": SortItems(); break;
                    case "6": ShowStatistics(); break;
                    case "0": backToMain = true; break;
                    default: Console.WriteLine("Невірний вибір."); Pause(); break;
                }
            }
        }

        static void AddItems()
        {
            Console.Clear();
            Console.WriteLine("Скільки записів ви хочете додати?");
            int count;
            while (!int.TryParse(Console.ReadLine(), out count) || count < 1)
            {
                Console.WriteLine("Введіть коректне число > 0:");
            }

            for (int i = 0; i < count; i++)
            {
                Console.WriteLine($"\n--- Запис #{i + 1} ---");
                FeedItem item = new FeedItem();

                Console.Write("Назва: ");
                item.Name = Console.ReadLine();

                do
                {
                    Console.Write("Ціна за кг: ");
                } while (!double.TryParse(Console.ReadLine(), out item.PricePerKg) || item.PricePerKg <= 0);

                do
                {
                    Console.Write("Вага (кг): ");
                } while (!double.TryParse(Console.ReadLine(), out item.Weight) || item.Weight <= 0);

                Console.Write("Преміум (так/ні): ");
                string p = Console.ReadLine().ToLower();
                item.IsPremium = (p == "так" || p == "yes" || p == "+");

                inventory.Add(item);
                Console.WriteLine("Додано!");
            }
            Pause();
        }

        static void PrintTable()
        {
            if (inventory.Count == 0)
            {
                Console.WriteLine("Список порожній.");
                return;
            }

            Console.WriteLine("\n{0,-5} | {1,-15} | {2,-10} | {3,-10} | {4,-10}", "№", "Назва", "Ціна", "Вага", "Преміум");
            Console.WriteLine(new string('-', 60));

            for (int i = 0; i < inventory.Count; i++)
            {
                FeedItem it = inventory[i];
                Console.WriteLine("{0,-5} | {1,-15} | {2,-10} | {3,-10} | {4,-10}",
                    i + 1, it.Name, it.PricePerKg, it.Weight, it.IsPremium ? "Так" : "Ні");
            }
        }

        static void SearchItem()
        {
            Console.Clear();
            Console.Write("Введіть назву для пошуку: ");
            string query = Console.ReadLine().ToLower();
            bool found = false;

            Console.WriteLine("\nРезультати пошуку:");
            foreach (var item in inventory)
            {
                if (item.Name.ToLower().Contains(query))
                {
                    Console.WriteLine($"Знайдено: {item.Name} - {item.PricePerKg} грн/кг ({item.Weight} кг)");
                    found = true;
                }
            }

            if (!found) Console.WriteLine("Нічого не знайдено.");
            Pause();
        }

        static void DeleteItem()
        {
            Console.Clear();
            PrintTable();
            Console.Write("\nВведіть номер (№) елемента для видалення: ");

            if (int.TryParse(Console.ReadLine(), out int index))
            {
                int realIndex = index - 1;

                if (realIndex >= 0 && realIndex < inventory.Count)
                {
                    Console.WriteLine($"Видалено елемент: {inventory[realIndex].Name}");
                    inventory.RemoveAt(realIndex);
                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Помилка: Такого номеру немає в списку.");
                    Console.ResetColor();
                }
            }
            else
            {
                Console.WriteLine("Помилка: Введіть число.");
            }
            Pause();
        }

        static void SortItems()
        {
            Console.Clear();

            if (inventory.Count < 2)
            {
                Console.WriteLine("Мало даних для сортування.");
                Pause();
                return;
            }

            for (int i = 0; i < inventory.Count - 1; i++)
            {
                for (int j = 0; j < inventory.Count - i - 1; j++)
                {
                    if (inventory[j].PricePerKg > inventory[j + 1].PricePerKg)
                    {
                        FeedItem temp = inventory[j];
                        inventory[j] = inventory[j + 1];
                        inventory[j + 1] = temp;
                    }
                }
            }

            Console.WriteLine("Список відсортовано за ціною (зростання).");
            PrintTable();
            Pause();
        }

        static void ShowStatistics()
        {
            Console.Clear();
            if (inventory.Count == 0)
            {
                Console.WriteLine("Дані відсутні.");
                Pause();
                return;
            }

            double totalSum = 0;
            double maxPrice = double.MinValue;
            double minPrice = double.MaxValue;
            string maxName = "";
            string minName = "";

            foreach (var item in inventory)
            {
                totalSum += (item.PricePerKg * item.Weight);

                if (item.PricePerKg > maxPrice)
                {
                    maxPrice = item.PricePerKg;
                    maxName = item.Name;
                }

                if (item.PricePerKg < minPrice)
                {
                    minPrice = item.PricePerKg;
                    minName = item.Name;
                }
            }

            double avgPrice = totalSum / inventory.Count;

            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("=== СТАТИСТИКА СКЛАДУ ===");
            Console.ResetColor();
            Console.WriteLine($"Кількість елементів: {inventory.Count}");
            Console.WriteLine($"Загальна вартість складу: {totalSum} грн");
            Console.WriteLine($"Середня вартість позиції: {Math.Round(avgPrice, 2)} грн");
            Console.WriteLine($"Найдорожчий корм: {maxName} ({maxPrice} грн)");
            Console.WriteLine($"Найдешевший корм: {minName} ({minPrice} грн)");

            Pause();
        }

        static void ShowMainMenu()
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("=============================================\r\n    Ласкаво просимо до зоопарку         \r\n             \"ДИКИЙ СВІТ\"   \r\n==============================================");
            Console.ResetColor();
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("\n ГОЛОВНЕ МЕНЮ:");
            Console.WriteLine("1. Введення даних про тварин");
            Console.WriteLine("2. Ведення даних про види");
            Console.WriteLine("3. Ведення даних про доглядачів");
            Console.WriteLine("4. Графік годування");
            Console.WriteLine("5. Фіксація стану здоров’я тварин");
            Console.WriteLine("6. Фільтр тварин за видом");
            Console.WriteLine("7. УПРАВЛІННЯ ЗАПАСАМИ КОРМІВ");
            Console.WriteLine("8. Вихід");
            Console.ResetColor();
        }

        static void ManageAnimals() { Console.WriteLine("В розробці..."); Pause(); }
        static void ManageSpecies() { Console.WriteLine("В розробці..."); Pause(); }
        static void ManageCaretakers() { Console.WriteLine("В розробці..."); Pause(); }
        static void ManageFeedingSchedule() { Console.WriteLine("В розробці..."); Pause(); }
        static void ManageHealthRecords() { Console.WriteLine("В розробці..."); Pause(); }
        static void FilterBySpecies() { Console.WriteLine("В розробці..."); Pause(); }

        static void Pause()
        {
            Console.ResetColor();
            Console.WriteLine("\nНатисніть Enter, щоб продовжити...");
            Console.ReadLine();
        }
    }
}