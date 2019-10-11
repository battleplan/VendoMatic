using Capstone.Models;
using Capstone.Views;
using System;

namespace Capstone
{
    class Program
    {
        static void Main(string[] args)
        {
            VendingMachine vendingMachine = new VendingMachine();
            //vm.Stock(new string[] { "A1|Potato Crisps|3.05|Chip", "A2|Stackers|1.45|Chip", "A3|Grain Waves|2.75|Chip", "A4|Cloud Popcorn|3.65|Chip" });\

            // TODO Search for file, if not found ask user
            bool isStocked = vendingMachine.StockFromFile("..\\..\\..\\..\\vendingmachine.csv");
            if (!isStocked)
            {
                Console.WriteLine("Machine not stock properly.");
                Console.ReadKey();
                return;
            }

            MainMenu mainMenu = new MainMenu(vendingMachine);

            mainMenu.Run();
        }
    }
}
