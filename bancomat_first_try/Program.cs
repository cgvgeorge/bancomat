using System;
using System.IO;

namespace bancomat_first_try
{
    class Program
    {
        static void PrintMenu()
        {
            Console.WriteLine("-----------");
            Console.WriteLine("1. Depunere");
            Console.WriteLine("2. Retragere");
            Console.WriteLine("0. Exit");
            Console.WriteLine("-----------");
        }
        static double AddToSold(double sold)
        {
            Console.WriteLine("introduceti suma pe care o aveti de depus: ");
            double sumaDepusa = double.Parse(Console.ReadLine());
            sold = sold + sumaDepusa;
            Console.WriteLine("Suma a fost depusa cu succesuri!");
            Console.WriteLine($"Soldul dvs curent este: {sold}");
            return sold;
        }

        static double Withdraw(double sold)
        {
            Console.WriteLine("introduceti suma pe care o aveti de retras: ");
            double sumaRetrasa = double.Parse(Console.ReadLine());
            if (sumaRetrasa > sold)
            {
                Console.WriteLine("Fonduri insuficiente!");
                Environment.Exit(0);
            }
            sold = sold - sumaRetrasa;
            Console.WriteLine("Suma a fost retrasa cu succesuri!");
            Console.WriteLine($"Soldul dvs curent este: {sold}");
            return sold;
        }

        static void Main(string[] args)
        {
            string iban = "";
            string pass = "";
            double sold = 0;

            Console.WriteLine("Introdu iban:");
            string readIban = Console.ReadLine();
            if (File.Exists($"cont_{readIban}.txt"))
            {
                string line;
                StreamReader file = new StreamReader($"cont_{readIban}.txt");
                while ((line = file.ReadLine()) != null)
                {
                    if (line.Contains("iban"))
                    {
                        //iban:01234567
                        iban = line.Substring(5);
                    }
                    if (line.Contains("pass"))
                    {
                        //pass:5555
                        pass = line.Substring(5);
                    }
                    if (line.Contains("sold"))
                    {
                        //sold:100,5
                        sold = double.Parse(line.Substring(5));
                    }
                }
                file.Close();
            }
            else
            {
                Console.WriteLine("Contul nu exista in baza noastra de date!");
                Environment.Exit(0);
            }

            if (readIban != iban)
            {
                Console.WriteLine("ibanul nu este valid");
                return;
            }
            Console.WriteLine("introdu parola:");
            string passw = Console.ReadLine();
            if (passw != pass)
            {
                Console.WriteLine("parola nu e buna");
                return;
            }
            PrintMenu();

            Console.WriteLine("Introdu optiunea:");
            int optiune = int.Parse(Console.ReadLine());

            while (optiune != 0)
            {
                switch (optiune)
                {
                    case 1:
                        sold = AddToSold(sold);
                        break;
                    case 2:
                        sold = Withdraw(sold);
                        break;
                    default:
                        Console.WriteLine("Optiune invalida!");
                        break;

                }

                PrintMenu();
                Console.WriteLine("Introdu optiunea:");
                optiune = int.Parse(Console.ReadLine());
            }
        }
    }
}