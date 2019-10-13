using Capstone.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Capstone.Views
{
    /// <summary>
    /// The top-level menu in our Market Application
    /// </summary>
    public class MainMenu : CLIMenu
    {
        /// <summary>
        /// Constructor adds items to the top-level menu
        /// </summary>
        public MainMenu(VendingMachine vendingMachine) : base(vendingMachine)
        {
            Title = @"Main Menu";
            menuOptions.Add("1", new MenuOption("Feed Money", true));
            menuOptions.Add("2", new MenuOption("Get Change", true));
            menuOptions.Add("4", new MenuOption("Sales Report", false));
            menuOptions.Add("Q", new MenuOption("Quit", true));

            // Add all slots
            foreach (string key in vendingMachine.Slots.Keys)
            {
                menuOptions.Add(key, new MenuOption("", false));
            }
        }

        /// <summary>
        /// The override of ExecuteSelection handles whatever selection was made by the user.
        /// This is where any business logic is executed.
        /// </summary>
        /// <param name="choice">"Key" of the user's menu selection</param>
        /// <returns></returns>
        protected override bool ExecuteSelection(string choice)
        {
            choice.ToUpper();
            if (vendingMachine.Slots.ContainsKey(choice))
            {
                bool purchaseComplete = vendingMachine.Purchase(choice);
                DrawHeader();
                if (purchaseComplete)
                {
                    Console.WriteLine(vendingMachine.Slots[choice].Product.YumYum());
                    Console.WriteLine($"You purchased {vendingMachine.Slots[choice].Product.Name} for {vendingMachine.Slots[choice].Price:C}.");
                    Pause($"You have {vendingMachine.Balance:C} remaining.");
                }
                else
                {
                    Pause("Purchase not made.");
                }
            }
            else
            {
                switch (choice)
                {
                    case "1":
                        new FeedMoneyMenu(vendingMachine).Run();
                        return true;
                    case "2":
                        string message = vendingMachine.FinishTransaction();
                        DrawHeader();
                        Pause(message);
                        break;
                    case "4":
                        string salesReport = vendingMachine.CreateSalesReport();
                        DrawHeader();
                        if (salesReport != "")
                        {
                            Pause($"Sales report generated: {salesReport}");
                        }
                        else
                        {
                            Pause("Sales report could not be generated.");
                        }
                        break;
                }
            }
            return true;
        }

    }
}
