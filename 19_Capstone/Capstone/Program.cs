using Capstone.Models;
using Capstone.Views;
using System;

namespace Capstone
{
    class Program
    {
        static void Main(string[] args)
        {
            VendingMachine vm = new VendingMachine();
            //vm.Stock(new string[] { "A1|Potato Crisps|3.05|Chip", "A2|Stackers|1.45|Chip", "A3|Grain Waves|2.75|Chip", "A4|Cloud Popcorn|3.65|Chip" });\

            vm.StockFromFile("..\\..\\..\\..\\vendingmachine.csv");

            MainMenu mainMenu = new MainMenu();

            mainMenu.Run(vm);
        }
    }
}
