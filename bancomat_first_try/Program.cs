using System;
using System.IO;

namespace bancomat_first_try
{
    class Program
    {
        static void PrintMenu()
        {
            Console.WriteLine("--------------");
            Console.WriteLine("1. Depunere");
            Console.WriteLine("2. Retragere");
            Console.WriteLine("3. Creare cont");
            Console.WriteLine("0. Exit");
            Console.WriteLine("--------------");
        }
        static double AddToSold(double sold)
        {
            Console.WriteLine("introduceti suma pe care o aveti de depus: ");
            double sumaDepusa = double.Parse(Console.ReadLine());
            sold = sold + sumaDepusa;
            Console.WriteLine("Suma a fost depusa cu succesuri!");
            Console.WriteLine($"Soldul dvs curent este: {sold}");
            Console.WriteLine("Mai doriti sa depuneti? DA / NU");
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


/*              public static StreamWriter CreateText(string newIban)
                {
                    Console.WriteLine("Indroduceti Ibanul dvs.:");
                    newIban = Console.ReadLine();
                }*/
        static void Main(string[] args)
        {
            string iban = "";
            string pass = "";
            double sold = 0;
            string raspuns = "";


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
                Console.WriteLine("Doriti sa il creati?");
                Console.WriteLine("Variante raspuns: DA / NU");
                raspuns = Console.ReadLine();
                if (raspuns == "DA"|| raspuns=="da")
                {
                    Console.WriteLine("Indtroduceti Ibanul dvs.");
                    readIban = Console.ReadLine();
                    //StreamReader file = new StreamReader($"cont_{readIban}.txt");
                    //creare cont
                }
                else 
                {
                    Console.WriteLine("Va multumim! La revedere!");
                    Environment.Exit(0);

                }
            }
            if (readIban != iban)
            {
                Console.WriteLine("Ibanul nu este valid!");
                return;
            }
            Console.WriteLine("Introdu parola:");
            string passw = Console.ReadLine();
            if (passw != pass)
            {
                Console.WriteLine("Parola incorecta! La revedere!");
                Environment.Exit(0);
                return;
            }
            PrintMenu();

            Console.WriteLine("Introdu optiunea:");
            
            int optiune = int.Parse(Console.ReadLine());

                while (optiune != 0)
                {

                    switch (optiune)
                    {
                        //case 0:
                           
                           // break;
                        case 1:
                            sold = AddToSold(sold);
                        string raspunsaddsold = Console.ReadLine();
                        if ((raspunsaddsold == "DA") || (raspunsaddsold == "da"))
                        {
                            AddToSold(sold);
                        }
                        else
                        {
                            Environment.Exit(0);
                        }
                        break;
                        case 2:
                            sold = Withdraw(sold);
                            break;
                        case 3:
                            Console.WriteLine("Adaugarea Ibanului este in lucru momentan. Ne cerem scuze pentru acest inconvenient!");
                            break;
                        default:
                            Console.WriteLine("Optiune invalida!");
                            break;

                    }
                }
                PrintMenu();
                Console.WriteLine("Introdu optiunea:");
                optiune = int.Parse(Console.ReadLine());
            
        }
    }
}