// <copyright file="MenuRenderer.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace ConsoleApp1.UI
{
    using System;
    using ConsoleApp1.Logic;

    /// <summary>
    /// Клас для відображення меню та взаємодії з користувачем у системі зоопарку.
    /// </summary>
    public static class MenuRenderer
    {
        /// <summary>
        /// Запускає головне меню зоопарку "ДИКИЙ СВІТ".
        /// </summary>
        public static void RunZooMainMenu()
        {
            bool isRunning = true;
            while (isRunning)
            {
                Console.Clear();
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("=============================================");
                Console.WriteLine("    Ласкаво просимо до зоопарку \"ДИКИЙ СВІТ\"");
                Console.WriteLine("=============================================");
                Console.ResetColor();

                Console.WriteLine("\n ГОЛОВНЕ МЕНЮ:");
                Console.WriteLine("1-6. (В розробці відповідно до завдань практики)");
                Console.WriteLine("7. УПРАВЛІННЯ СКЛАДОМ КОРМІВ (CSV)");
                Console.WriteLine("8. Вихід з акаунту");

                Console.Write("\nВиберіть опцію (1-8): ");
                string choice = Console.ReadLine();

                switch (choice)
                {
                    case "7":
                        FeedInventoryMenu();
                        break;
                    case "8":
                        isRunning = false;
                        break;
                    default:
                        Console.WriteLine("Цей розділ знаходиться в розробці.");
                        Console.ReadLine();
                        break;
                }
            }
        }

        /// <summary>
        /// Відображає меню управління складом кормів та обробляє CRUD-операції.
        /// </summary>
        public static void FeedInventoryMenu()
        {
            bool back = false;
            while (!back)
            {
                Console.Clear();
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine("=== УПРАВЛІННЯ СКЛАДОМ КОРМІВ ===");
                Console.ResetColor();
                Console.WriteLine("1. Додати запис");
                Console.WriteLine("2. Переглянути всі (Таблиця)");
                Console.WriteLine("3. Видалити запис");
                Console.WriteLine("4. Пошук корму");
                Console.WriteLine("5. Сортування за ціною");
                Console.WriteLine("6. Статистика складу");
                Console.WriteLine("0. Назад до головного меню");
                Console.Write("\nВаш вибір: ");

                string subChoice = Console.ReadLine();
                switch (subChoice)
                {
                    case "1":
                        InventoryLogic.AddItem(); // Реалізація створення запису [cite: 106]
                        break;
                    case "2":
                        InventoryLogic.PrintItems(); // Читання даних [cite: 106]
                        Console.WriteLine("\nНатисніть Enter для повернення...");
                        Console.ReadLine();
                        break;
                    case "3":
                        InventoryLogic.DeleteItem(); // Видалення запису [cite: 106]
                        break;
                    case "4":
                        InventoryLogic.SearchItem(); // Пошук та фільтрація [cite: 107]
                        break;
                    case "5":
                        InventoryLogic.SortItems(); // Сортування [cite: 107]
                        break;
                    case "6":
                        InventoryLogic.GetStatistics(); // Статистика
                        break;
                    case "0":
                        back = true;
                        break;
                    default:
                        Console.WriteLine("Невірний вибір. Спробуйте ще раз.");
                        System.Threading.Thread.Sleep(1000);
                        break;
                }
            }
        }
    }
}