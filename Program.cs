using System;
using System.Collections.Generic;
using System.IO;
using System.Security.Cryptography;
using System.Text;


namespace ConsoleApp1
{
    struct FeedItem
    {
        public int Id;
        public string Name;
        public double PricePerKg;
        public double Weight;
        public bool IsPremium;
    }

    internal class Program
    {
       
        static string inventoryPath = "inventory.csv";
        static string usersPath = "users.csv";

        static void Main(string[] args)
        {
            Console.OutputEncoding = Encoding.UTF8;
            Console.InputEncoding = Encoding.UTF8;

            CheckAndCreateFiles();

            while (true)
            {
                Console.Clear();
                Console.ForegroundColor = ConsoleColor.Cyan;
                Console.WriteLine("=== ВІТАЄМО В СИСТЕМІ ЗООПАРКУ ===");
                Console.ResetColor();
                Console.WriteLine("1. Вхід (Авторизація)");
                Console.WriteLine("2. Реєстрація нового користувача");
                Console.WriteLine("3. Вихід");
                Console.Write("Виберіть дію: ");

                string choice = Console.ReadLine();

                switch (choice)
                {
                    case "1":
                        if (Login())
                        {
                            RunZooMainMenu();
                        }
                        break;
                    case "2":
                        Register();
                        break;
                    case "3":
                        return;
                    default:
                        Console.WriteLine("Невірний вибір.");
                        Pause();
                        break;
                }
            }
        }

        static void RunZooMainMenu()
        {
            bool isRunning = true;
            while (isRunning)
            {
                Console.Clear();
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
                Console.WriteLine("7. УПРАВЛІННЯ СКЛАДОМ КОРМІВ (CSV)");
                Console.WriteLine("8. Вихід з акаунту");
                Console.ResetColor();

                Console.Write("\nВиберіть опцію (1-8): ");
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
                    case "8": isRunning = false; break;
                    default:
                        Console.WriteLine("Невірний вибір.");
                        Pause();
                        break;
                }
            }
        }

        static void CheckAndCreateFiles()
        {
            if (!File.Exists(usersPath))
            {
                File.WriteAllText(usersPath, "Id,Email,PasswordHash\n");
            }

            if (!File.Exists(inventoryPath))
            {
                File.WriteAllText(inventoryPath, "Id,Name,Price,Weight,IsPremium\n");
            }
        }

        static int GenerateNewId(string path)
        {
            if (!File.Exists(path)) return 1;

            var lines = File.ReadAllLines(path);
            if (lines.Length <= 1) return 1;

            int maxId = 0;

            for (int i = 1; i < lines.Length; i++)
            {
                var parts = lines[i].Split(',');
                if (parts.Length > 0 && int.TryParse(parts[0], out int currentId))
                {
                    if (currentId > maxId) maxId = currentId;
                }
            }

            return maxId + 1;
        }

        static string HashPassword(string password)
        {
            using (SHA256 sha256 = SHA256.Create())
            {
                byte[] bytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(password));
                StringBuilder builder = new StringBuilder();
                for (int i = 0; i < bytes.Length; i++)
                {
                    builder.Append(bytes[i].ToString("x2"));
                }
                return builder.ToString();
            }
        }

        static void Register()
        {
            Console.Clear();
            Console.WriteLine("=== РЕЄСТРАЦІЯ ===");
            Console.Write("Введіть Email: ");
            string email = Console.ReadLine();

            if (string.IsNullOrWhiteSpace(email) || !email.Contains("@"))
            {
                Console.WriteLine("Некоректний Email.");
                Pause();
                return;
            }

            string[] lines = File.ReadAllLines(usersPath);
            for (int i = 1; i < lines.Length; i++)
            {
                var parts = lines[i].Split(',');
                if (parts.Length > 1 && parts[1] == email)
                {
                    Console.WriteLine("Користувач з таким Email вже існує.");
                    Pause();
                    return;
                }
            }

            Console.Write("Введіть пароль: ");
            string pass = Console.ReadLine();

            if (string.IsNullOrWhiteSpace(pass) || pass.Length < 4)
            {
                Console.WriteLine("Пароль надто короткий.");
                Pause();
                return;
            }

            int newId = GenerateNewId(usersPath);
            string hash = HashPassword(pass);
            string newLine = $"{newId},{email},{hash}";

            File.AppendAllText(usersPath, newLine + Environment.NewLine);
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("Реєстрація успішна! Тепер увійдіть.");
            Console.ResetColor();
            Pause();
        }

        static bool Login()
        {
            Console.Clear();
            Console.WriteLine("=== ВХІД ===");
            Console.Write("Email: ");
            string email = Console.ReadLine();
            Console.Write("Пароль: ");
            string pass = Console.ReadLine();

            string hashInput = HashPassword(pass);
            string[] lines = File.ReadAllLines(usersPath);

            for (int i = 1; i < lines.Length; i++)
            {
                var parts = lines[i].Split(',');
                if (parts.Length < 3) continue;

                if (parts[1] == email && parts[2] == hashInput)
                {
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine("Авторизація успішна!");
                    Console.ResetColor();
                    System.Threading.Thread.Sleep(1000);
                    return true;
                }
            }

            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("Невірний логін або пароль.");
            Console.ResetColor();
            Pause();
            return false;
        }

        static void FeedInventoryMenu()
        {
            bool back = false;
            while (!back)
            {
                Console.Clear();
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine("=== СКЛАД КОРМІВ (ФАЙЛОВА СИСТЕМА) ===");
                Console.ResetColor();
                Console.WriteLine("1. Додати запис");
                Console.WriteLine("2. Переглянути всі (Таблиця)");
                Console.WriteLine("3. Видалити запис");
                Console.WriteLine("4. Пошук");
                Console.WriteLine("5. Сортування");
                Console.WriteLine("6. Статистика");
                Console.WriteLine("0. Назад до головного меню");
                Console.Write("Вибір: ");

                string sub = Console.ReadLine();
                switch (sub)
                {
                    case "1": AddItem(); break;
                    case "2": PrintItems(); Pause(); break;
                    case "3": DeleteItem(); break;
                    case "4": SearchItem(); break;
                    case "5": SortItems(); break;
                    case "6": ShowStatistics(); break;
                    case "0": back = true; break;
                    default: Console.WriteLine("Помилка."); Pause(); break;
                }
            }
        }

        static List<FeedItem> ReadAllItems()
        {
            List<FeedItem> items = new List<FeedItem>();

            if (!File.Exists(inventoryPath)) return items;

            string[] lines = File.ReadAllLines(inventoryPath);

            for (int i = 1; i < lines.Length; i++)
            {
                string line = lines[i];
                if (string.IsNullOrWhiteSpace(line)) continue;

                string[] parts = line.Split(',');
                if (parts.Length < 5) continue;

                try
                {
                    FeedItem item = new FeedItem();
                    item.Id = int.Parse(parts[0]);
                    item.Name = parts[1];
                    item.PricePerKg = double.Parse(parts[2]);
                    item.Weight = double.Parse(parts[3]);
                    item.IsPremium = bool.Parse(parts[4]);

                    items.Add(item);
                }
                catch
                {
                    continue;
                }
            }
            return items;
        }

        static void SaveAllItems(List<FeedItem> items)
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine("Id,Name,Price,Weight,IsPremium");

            foreach (var item in items)
            {
                sb.AppendLine($"{item.Id},{item.Name},{item.PricePerKg},{item.Weight},{item.IsPremium}");
            }

            File.WriteAllText(inventoryPath, sb.ToString());
        }

        static void AddItem()
        {
            Console.Clear();
            Console.WriteLine("--- Додавання ---");

            FeedItem item = new FeedItem();
            item.Id = GenerateNewId(inventoryPath);

            Console.Write("Назва: ");
            item.Name = Console.ReadLine();

           
            if (item.Name.Contains(","))
                item.Name = item.Name.Replace(",", " ");

            do
            {
                Console.Write("Ціна: ");
            } while (!double.TryParse(Console.ReadLine(), out item.PricePerKg) || item.PricePerKg <= 0);

            do
            {
                Console.Write("Вага: ");
            } while (!double.TryParse(Console.ReadLine(), out item.Weight) || item.Weight <= 0);

            Console.Write("Преміум (так/ні): ");
            string p = Console.ReadLine().ToLower();
            item.IsPremium = (p == "так" || p == "yes" || p == "+");

            string line = $"{item.Id},{item.Name},{item.PricePerKg},{item.Weight},{item.IsPremium}";
            File.AppendAllText(inventoryPath, line + Environment.NewLine);

            Console.WriteLine("Запис збережено у файл!");
            Pause();
        }

        static void PrintItems()
        {
            var items = ReadAllItems();
            if (items.Count == 0)
            {
                Console.WriteLine("Файл порожній або відсутній.");
                return;
            }

            Console.WriteLine("{0,-5} | {1,-15} | {2,-10} | {3,-10} | {4,-10}", "ID", "Назва", "Ціна", "Вага", "Преміум");
            Console.WriteLine(new string('-', 60));

            foreach (var item in items)
            {
                Console.WriteLine("{0,-5} | {1,-15} | {2,-10} | {3,-10} | {4,-10}",
                    item.Id, item.Name, item.PricePerKg, item.Weight, item.IsPremium ? "Так" : "Ні");
            }
        }

        static void DeleteItem()
        {
            PrintItems();
            Console.Write("\nВведіть ID для видалення: ");
            if (int.TryParse(Console.ReadLine(), out int id))
            {
                var items = ReadAllItems();
                bool found = false;

                for (int i = 0; i < items.Count; i++)
                {
                    if (items[i].Id == id)
                    {
                        items.RemoveAt(i);
                        found = true;
                        break;
                    }
                }

                if (found)
                {
                    SaveAllItems(items);
                    Console.WriteLine("Видалено успішно.");
                }
                else
                {
                    Console.WriteLine("ID не знайдено.");
                }
            }
            else
            {
                Console.WriteLine("Некоректний ID.");
            }
            Pause();
        }

        static void SearchItem()
        {
            Console.Clear();
            Console.Write("Введіть назву для пошуку: ");
            string q = Console.ReadLine().ToLower();

            var items = ReadAllItems();
            bool found = false;

            Console.WriteLine("\nРезультати:");
            foreach (var item in items)
            {
                if (item.Name.ToLower().Contains(q))
                {
                    Console.WriteLine($"ID: {item.Id} | {item.Name} - {item.PricePerKg} грн");
                    found = true;
                }
            }

            if (!found) Console.WriteLine("Нічого не знайдено.");
            Pause();
        }

        static void SortItems()
        {
            var items = ReadAllItems();
            if (items.Count < 2)
            {
                Console.WriteLine("Мало даних.");
                Pause();
                return;
            }

            for (int i = 0; i < items.Count - 1; i++)
            {
                for (int j = 0; j < items.Count - i - 1; j++)
                {
                    if (items[j].PricePerKg > items[j + 1].PricePerKg)
                    {
                        var temp = items[j];
                        items[j] = items[j + 1];
                        items[j + 1] = temp;
                    }
                }
            }

            Console.WriteLine("Відсортовано (локально).");
            Console.WriteLine("{0,-5} | {1,-15} | {2,-10} | {3,-10}", "ID", "Назва", "Ціна", "Вага");

            foreach (var item in items)
            {
                Console.WriteLine("{0,-5} | {1,-15} | {2,-10} | {3,-10}", item.Id, item.Name, item.PricePerKg, item.Weight);
            }
            Pause();
        }

        static void ShowStatistics()
        {
            var items = ReadAllItems();
            if (items.Count == 0)
            {
                Console.WriteLine("Дані відсутні.");
                Pause();
                return;
            }

            double sum = 0;
            double min = double.MaxValue;
            double max = double.MinValue;

            foreach (var item in items)
            {
                sum += (item.PricePerKg * item.Weight);
                if (item.PricePerKg < min) min = item.PricePerKg;
                if (item.PricePerKg > max) max = item.PricePerKg;
            }

            Console.WriteLine("=== СТАТИСТИКА ===");
            Console.WriteLine($"Кількість записів: {items.Count}");
            Console.WriteLine($"Загальна вартість: {sum}");
            Console.WriteLine($"Мін. ціна: {min}");
            Console.WriteLine($"Макс. ціна: {max}");
            Console.WriteLine($"Середня ціна: {Math.Round(sum / items.Count, 2)}");
            Pause();
        }

        static void ManageAnimals() { Console.WriteLine("В розробці..."); Pause(); }
        static void ManageSpecies() { Console.WriteLine("В розробці..."); Pause(); }
        static void ManageCaretakers() { Console.WriteLine("В розробці..."); Pause(); }
        static void ManageFeedingSchedule() { Console.WriteLine("В розробці..."); Pause(); }
        static void ManageHealthRecords() { Console.WriteLine("В розробці..."); Pause(); }
        static void FilterBySpecies() { Console.WriteLine("В розробці..."); Pause(); }

        static void Pause()
        {
            Console.WriteLine("\nНатисніть Enter...");
            Console.ReadLine();
        }
    }
}