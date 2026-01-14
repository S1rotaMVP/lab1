// <copyright file="Program.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace ConsoleApp1
{
    using System;
    using System.Text;
    using ConsoleApp1.Data;
    using ConsoleApp1.Security;
    using ConsoleApp1.UI;

    /// <summary>
    /// Головний клас програми, що забезпечує точку входу.
    /// </summary>
    internal class Program
    {
        /// <summary>
        /// Головний метод програми.
        /// </summary>
        /// <param name="args">Аргументи командного рядка.</param>
        private static void Main(string[] args)
        {
            Console.OutputEncoding = Encoding.UTF8;
            Console.InputEncoding = Encoding.UTF8;

            FileStorage.CheckAndCreateFiles();

            while (true)
            {
                Console.Clear();
                Console.WriteLine("=== ВІТАЄМО В СИСТЕМІ ЗООПАРКУ ===");
                Console.WriteLine("1. Вхід | 2. Реєстрація | 3. Вихід");

                string choice = Console.ReadLine();
                if (choice == "1")
                {
                    if (AuthService.Login())
                    {
                        MenuRenderer.RunZooMainMenu();
                    }
                }
                else if (choice == "2")
                {
                    AuthService.Register();
                }
                else if (choice == "3")
                {
                    break;
                }
            }
        }
    }
}