using System;
using System.ComponentModel;
using System.Security.Cryptography.X509Certificates;
namespace ConsoleApp1
{
    internal class Program
    {
        
        static void Main(string[] args)
        {
            Console.OutputEncoding = System.Text.Encoding.UTF8;
            Console.InputEncoding = System.Text.Encoding.UTF8;
                             
                ShowMenu();
                Console.Write("\n Виберіть опцію (1-8): ");
                string choice = Console.ReadLine();
                switch (choice)
                {
                    case "1":
                        Console.WriteLine("Введення даних про тварин");
                        ;
                        ManageAnimals();
                    Main(args);

                    break;
                    case "2":
                        Console.WriteLine("Ведення даних про види");
                       
                    ManageSpecies();
                    Main(args);
                    break;
                    case "3":
                        Console.WriteLine("Ведення даних про доглядачів");
                        
                        ManageCaretakers();
                    Main(args);
                    break;
                    case "4":
                        Console.WriteLine("Графік годування");
                        
                        ManageFeedingSchedule();
                    Main(args);
                    break;
                    case "5":
                        Console.WriteLine("Фіксація стану здоров’я тварин");
                        
                        ManageHealthRecords();
                    Main(args);
                    break;
                    case "6":
                        Console.WriteLine("Фільтр тварин за видом");
                        
                        FilterBySpecies();
                    Main(args);
                    break;
                    case "7":
                        TicketSales();
                    Main(args);

                    break;
                    case "8":                       
                        
                        Console.WriteLine("Вихід з програми. До побачення!");
                        break;
                    default:
                        
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("Невірний вибір. Спробуйте ще раз.");
                        Pause();
                        
                    Console.ResetColor();
                    Main(args);
                    break;
                
            }
        }



        static void TicketSales()
        {
            double ticketsold = 250;
            double ticketschild = 150;
            double ticketsstudents = 80;
            double ticketsmilitary = 70;
            double ticketsfamily = 600;
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("=======№№№№Вітаю в нашому Зоопарку№№№=======");
            Console.ResetColor();
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("=======Ціни на квитки======= ");
            Console.WriteLine($"Білет для осіб 18+ {ticketsold} грн");
            Console.WriteLine($"Білет для дітей 6-17 років {ticketschild} грн");
            Console.WriteLine($"Білет для студентів {ticketsstudents}  грн");
            Console.WriteLine($"Білет для військових {ticketsmilitary} грн");
            Console.WriteLine($"Сімейний квиток (2 дорослих + 2 дітей) {ticketsfamily} грн");
            Console.ResetColor();
            Console.WriteLine("\n");



            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.Write("Введіть кількість квитків дорослих квитків:");
            double counticketsold = Convert.ToInt32(Console.ReadLine());
            Console.Write("Введіть кількість дитячих квитків:");
            double counticketschild = Convert.ToInt32(Console.ReadLine());
            Console.Write("Введіть кількість студентських квитків:");
            double counticketsstudents = Convert.ToInt32(Console.ReadLine());
            Console.Write("Введіть кількість військових квитків:");
            double counticketsmilitary = Convert.ToInt32(Console.ReadLine());
            Console.Write("Введіть кількість сімейних квитків:");
            double counticketsfamily = Convert.ToInt32(Console.ReadLine());
            double totalprice = (ticketsold * counticketsold) + (ticketschild * counticketschild) + (ticketsstudents * counticketsstudents) + (ticketsmilitary * counticketsmilitary) + (ticketsfamily * counticketsfamily);
            Console.ResetColor();
            Console.WriteLine($"Сумма вашої покупки: {totalprice}грн");

            double discount = new Random().NextDouble() * 40;
            double rounddiscount = Math.Round(discount, 0);
            Console.WriteLine($"Знижка: {rounddiscount}%");
            double discountamount = (totalprice * rounddiscount) / 100;
            double finalprice = totalprice - discountamount;
            Console.WriteLine($"Сума до оплати з урахуванням знижки: {finalprice}грн");
            double sqrttotalprice = Math.Sqrt(finalprice);
            double roundfinalprice = Math.Round(sqrttotalprice, 2);
            Console.WriteLine($"Квадратний корінь від суми покупки:  {roundfinalprice}");
            Console.WriteLine("\n");
            Console.WriteLine("=======Деталі вашої покупки======= ");
            Console.ForegroundColor = ConsoleColor.DarkBlue;
            Console.WriteLine($"Ви придбали дорослих квитків  {counticketsold}");
            Console.WriteLine($"Ви придбали дитячих квитків  {counticketschild}");
            Console.WriteLine($"Ви придбали студентських квитків  {counticketsstudents}");
            Console.WriteLine($"Ви придбали військових квитків  {counticketsmilitary}");
            Console.WriteLine($"Ви придбали сімейних квитків  {counticketsfamily}");
            Console.WriteLine($"Загальна сума без знижки: {totalprice}грн");
            Console.WriteLine($"Сума знижки: {discountamount}грн");
            Console.WriteLine($"Сума до оплати з урахуванням знижки: {finalprice}грн");
            Console.ResetColor();
            Console.WriteLine("Дякую за покупку");
            Pause();
        }

        static double Check()
        {
            double choice = 0;
            try 
            {
                choice = Convert.ToDouble(Console.ReadLine());
            }
            catch (FormatException)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Помилка: введено некоректне значення. Спробуйте ще раз.");
                Console.ResetColor();
                Check();
            }
            return choice;

        }

        static void ShowMenu()
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("=============================================\r\n    Ласкаво просимо до зоопарку         \r\n             \"ДИКИЙ СВІТ\"   \r\n==============================================");
            Console.WriteLine("");
            Console.ResetColor();
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("\n МЕНЮ:");
            Console.WriteLine("1.Введення даних про тварин");
            Console.WriteLine("2. Ведення даних про види");
            Console.WriteLine("3. Ведення даних про доглядачів");
            Console.WriteLine("4. Графік годування");
            Console.WriteLine("5. Фіксація стану здоров’я тварин");
            Console.WriteLine("6. Фільтр тварин за видом");
            Console.WriteLine("7. Продаж квитків");
            Console.WriteLine("8. Вихід");
            Console.ResetColor();
        }
        
        static void ManageAnimals()
        {
            Console.Clear();
            Console.WriteLine("Функція управління тваринами ще не реалізована.");
            Pause();
        }
        static void ManageSpecies()
        {
            Console.Clear();
            Console.WriteLine("Функція управління видами ще не реалізована.");
            Pause();
        }
        static void ManageCaretakers()
        {
            Console.Clear();
            Console.WriteLine("Функція управління доглядачами ще не реалізована.");
            Pause();
        }
        static void ManageFeedingSchedule()
        {
            Console.Clear();
            Console.WriteLine("Функція управління графіком годування ще не реалізована.");
            Pause();
        }
        static void ManageHealthRecords()
        {
            Console.Clear();
            Console.WriteLine("Функція управління станом здоров’я тварин ще не реалізована.");
            Pause();
        }
        static void FilterBySpecies()
        {
            Console.Clear();
            Console.WriteLine("Функція фільтрації тварин за видом ще не реалізована.");
            Pause();
        }

        static void Pause()
        {
            Console.ResetColor();
            Console.WriteLine("\nНатисніть Enter, щоб повернутися до головного меню...");
            Console.ReadLine();
        }
    }
}
