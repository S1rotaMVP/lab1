// <copyright file="FileStorage.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace ConsoleApp1.Data
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Text;
    using ConsoleApp1.Models;

    /// <summary>
    /// Клас для роботи з файловою системою та зберігання даних у форматі CSV.
    /// </summary>
    public static class FileStorage
    {
        /// <summary>
        /// Шлях до файлу з інвентарем.
        /// </summary>
        private static string inventoryPath = "inventory.csv";

        /// <summary>
        /// Шлях до файлу з користувачами.
        /// </summary>
        private static string usersPath = "users.csv";

        /// <summary>
        /// Gets шлях до файлу з користувачами.
        /// </summary>
        public static string UsersPath => usersPath;

        /// <summary>
        /// Gets шлях до файлу з інвентарем.
        /// </summary>
        public static string InventoryPath => inventoryPath;

        /// <summary>
        /// Перевіряє наявність файлів даних і створює їх за потреби.
        /// </summary>
        public static void CheckAndCreateFiles()
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

        /// <summary>
        /// Генерує новий унікальний ID для запису у файлі.
        /// </summary>
        /// <param name="path">Шлях до файлу CSV.</param>
        /// <returns>Новий цілочисельний ідентифікатор.</returns>
        public static int GenerateNewId(string path)
        {
            if (!File.Exists(path))
            {
                return 1;
            }

            var lines = File.ReadAllLines(path);
            if (lines.Length <= 1)
            {
                return 1;
            }

            int maxId = 0;
            for (int i = 1; i < lines.Length; i++)
            {
                var parts = lines[i].Split(',');
                if (parts.Length > 0 && int.TryParse(parts[0], out int currentId))
                {
                    if (currentId > maxId)
                    {
                        maxId = currentId;
                    }
                }
            }

            return maxId + 1;
        }

        /// <summary>
        /// Зчитує всі елементи корму з файлу інвентаря.
        /// </summary>
        /// <returns>Список об'єктів FeedItem.</returns>
        public static List<FeedItem> ReadAllItems()
        {
            List<FeedItem> items = new List<FeedItem>();
            if (!File.Exists(inventoryPath))
            {
                return items;
            }

            string[] lines = File.ReadAllLines(inventoryPath);
            for (int i = 1; i < lines.Length; i++)
            {
                string[] parts = lines[i].Split(',');
                if (parts.Length < 5)
                {
                    continue;
                }

                try
                {
                    items.Add(new FeedItem
                    {
                        Id = int.Parse(parts[0]),
                        Name = parts[1],
                        PricePerKg = double.Parse(parts[2]),
                        Weight = double.Parse(parts[3]),
                        IsPremium = bool.Parse(parts[4]),
                    });
                }
                catch
                {
                    continue;
                }
            }

            return items;
        }

        /// <summary>
        /// Зберігає весь список елементів у файл CSV.
        /// </summary>
        /// <param name="items">Список елементів для збереження.</param>
        public static void SaveAllItems(List<FeedItem> items)
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine("Id,Name,Price,Weight,IsPremium");

            foreach (var item in items)
            {
                sb.AppendLine($"{item.Id},{item.Name},{item.PricePerKg},{item.Weight},{item.IsPremium}");
            }

            File.WriteAllText(inventoryPath, sb.ToString());
        }
    }
}