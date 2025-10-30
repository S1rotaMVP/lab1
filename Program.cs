using System;
namespace ConsoleApp1
{
    internal class Program
    {
        static void Main(string[] args)
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





        }
    }
}
