// <copyright file="Inventorylogic.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace ConsoleApp1.Logic
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using ConsoleApp1.Data;
    using ConsoleApp1.Models;

    /// <summary>
    /// Клас, що містить бізнес-логіку управління інвентарем кормів у зоопарку.
    /// </summary>
    public static class InventoryLogic
    {
        /// <summary>
        /// Додає новий запис про корм до системи.
        /// </summary>
        public static void AddItem()
        {
            Console.Clear();
            FeedItem item = default;
            item.Id = FileStorage.GenerateNewId(FileStorage.InventoryPath);

            Console.Write("Введіть назву корму: ");
            item.Name = Console.ReadLine().Replace(",", " ");

            double price;
            do
            {
                Console.Write("Введіть ціну за кг: ");
            }
            while (!double.TryParse(Console.ReadLine(), out price));
            item.PricePerKg = price;

            double weight;
            do
            {
                Console.Write("Введіть вагу (кг): ");
            }
            while (!double.TryParse(Console.ReadLine(), out weight));
            item.Weight = weight;

            Console.Write("Це преміум корм? (так/ні): ");
            string p = Console.ReadLine().ToLower();
            item.IsPremium = p == "так" || p == "+";

            string line = $"{item.Id},{item.Name},{item.PricePerKg},{item.Weight},{item.IsPremium}";
            File.AppendAllText(FileStorage.InventoryPath, line + Environment.NewLine);

            Console.WriteLine("Запис успішно додано!");
            Pause();
        }

        /// <summary>
        /// Виводить список усіх наявних кормів у консоль у вигляді таблиці.
        /// </summary>
        public static void PrintItems()
        {
            var items = FileStorage.ReadAllItems();
            if (items.Count == 0)
            {
                Console.WriteLine("На складі порожньо.");
                return;
            }

            Console.WriteLine("{0,-5} | {1,-15} | {2,-10} | {3,-10} | {4,-10}", "ID", "Назва", "Ціна", "Вага", "Преміум");
            Console.WriteLine(new string('-', 60));

            foreach (var item in items)
            {
                Console.WriteLine(
                    "{0,-5} | {1,-15} | {2,-10} | {3,-10} | {4,-10}",
                    item.Id,
                    item.Name,
                    item.PricePerKg,
                    item.Weight,
                    item.IsPremium ? "Так" : "Ні");
            }
        }

        /// <summary>
        /// Видаляє запис про корм за його ідентифікатором.
        /// </summary>
        public static void DeleteItem()
        {
            PrintItems();
            Console.Write("\nВведіть ID для видалення: ");
            if (int.TryParse(Console.ReadLine(), out int id))
            {
                var items = FileStorage.ReadAllItems();
                int initialCount = items.Count;
                items.RemoveAll(x => x.Id == id);

                if (items.Count < initialCount)
                {
                    FileStorage.SaveAllItems(items);
                    Console.WriteLine("Запис видалено.");
                }
                else
                {
                    Console.WriteLine("Запис з таким ID не знайдено.");
                }
            }

            Pause();
        }

        /// <summary>
        /// Здійснює пошук корму за назвою.
        /// </summary>
        public static void SearchItem()
        {
            Console.Clear();
            Console.Write("Введіть назву для пошуку: ");
            string query = Console.ReadLine().ToLower();

            var items = FileStorage.ReadAllItems();
            bool found = false;

            foreach (var item in items)
            {
                if (item.Name.ToLower().Contains(query))
                {
                    Console.WriteLine($"ID: {item.Id} | {item.Name} - {item.PricePerKg} грн/кг");
                    found = true;
                }
            }

            if (!found)
            {
                Console.WriteLine("Нічого не знайдено.");
            }

            Pause();
        }

        /// <summary>
        /// Сортує список кормів за ціною та виводить результат.
        /// </summary>
        public static void SortItems()
        {
            var items = FileStorage.ReadAllItems();
            items.Sort((x, y) => x.PricePerKg.CompareTo(y.PricePerKg));

            Console.WriteLine("Список відсортовано за зростанням ціни:");
            PrintItems();
            Pause();
        }

        /// <summary>
        /// Розраховує та виводить статистичні дані про склад кормів.
        /// </summary>
        public static void GetStatistics()
        {
            var items = FileStorage.ReadAllItems();
            if (items.Count == 0)
            {
                Console.WriteLine("Дані для статистики відсутні.");
                Pause();
                return;
            }

            double totalValue = 0;
            foreach (var item in items)
            {
                totalValue += item.PricePerKg * item.Weight;
            }

            Console.WriteLine("=== СТАТИСТИКА СКЛАДУ ===");
            Console.WriteLine($"Загальна кількість видів корму: {items.Count}");
            Console.WriteLine($"Загальна вартість усіх запасів: {Math.Round(totalValue, 2)} грн");
            Console.WriteLine($"Середня ціна за одиницю: {Math.Round(totalValue / items.Count, 2)} грн");
            Pause();
        }

        /// <summary>
        /// Призупиняє виконання програми до натискання клавіші Enter.
        /// </summary>
        private static void Pause()
        {
            Console.WriteLine("\nНатисніть Enter, щоб повернутися...");
            Console.ReadLine();
        }
    }
}