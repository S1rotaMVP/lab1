// <copyright file="AuthService.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace ConsoleApp1.Security
{
    using System;
    using System.IO;
    using System.Security.Cryptography;
    using System.Text;
    using ConsoleApp1.Data;

    /// <summary>
    /// Клас для автентифікації користувачів та хешування паролів у системі зоопарку.
    /// </summary>
    public static class AuthService
    {
        /// <summary>
        /// Створює хеш пароля за допомогою алгоритму SHA256.
        /// </summary>
        /// <param name="password">Пароль у відкритому вигляді.</param>
        /// <returns>Хеш-рядок пароля.</returns>
        public static string HashPassword(string password)
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

        /// <summary>
        /// Забезпечує процес входу користувача в систему.
        /// </summary>
        /// <returns>Значення true, якщо вхід успішний; інакше — false.</returns>
        public static bool Login()
        {
            Console.Clear();
            Console.WriteLine("=== ВХІД ===");
            Console.Write("Email: ");
            string email = Console.ReadLine();
            Console.Write("Пароль: ");
            string pass = Console.ReadLine();

            string hashInput = HashPassword(pass);

            // Використовуємо UsersPath з великої літери
            string[] lines = File.ReadAllLines(FileStorage.UsersPath);

            for (int i = 1; i < lines.Length; i++)
            {
                var parts = lines[i].Split(',');
                if (parts.Length >= 3 && parts[1] == email && parts[2] == hashInput)
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

        /// <summary>
        /// Забезпечує реєстрацію нового користувача.
        /// </summary>
        public static void Register()
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

            string[] lines = File.ReadAllLines(FileStorage.UsersPath);
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

            int newId = FileStorage.GenerateNewId(FileStorage.UsersPath);
            string hash = HashPassword(pass);
            File.AppendAllText(FileStorage.UsersPath, $"{newId},{email},{hash}" + Environment.NewLine);

            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("Реєстрація успішна!");
            Console.ResetColor();
            Pause();
        }

        /// <summary>
        /// Призупиняє роботу програми до натискання Enter.
        /// </summary>
        private static void Pause()
        {
            Console.WriteLine("\nНатисніть Enter...");
            Console.ReadLine();
        }
    }
}