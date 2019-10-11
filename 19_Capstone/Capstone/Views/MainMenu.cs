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
            this.Title = @" 
 /$$    /$$ /$$$$$$$$ /$$   /$$ /$$$$$$$   /$$$$$$  /$$      /$$  /$$$$$$  /$$$$$$$$ /$$$$$$  /$$$$$$ 
| $$   | $$| $$_____/| $$$ | $$| $$__  $$ /$$__  $$| $$$    /$$$ /$$__  $$|__  $$__/|_  $$_/ /$$__  $$
| $$   | $$| $$      | $$$$| $$| $$  \ $$| $$  \ $$| $$$$  /$$$$| $$  \ $$   | $$     | $$  | $$  \__/
|  $$ / $$/| $$$$$   | $$ $$ $$| $$  | $$| $$  | $$| $$ $$/$$ $$| $$$$$$$$   | $$     | $$  | $$      
 \  $$ $$/ | $$__/   | $$  $$$$| $$  | $$| $$  | $$| $$  $$$| $$| $$__  $$   | $$     | $$  | $$      
  \  $$$/  | $$      | $$\  $$$| $$  | $$| $$  | $$| $$\  $ | $$| $$  | $$   | $$     | $$  | $$    $$
   \  $/   | $$$$$$$$| $$ \  $$| $$$$$$$/|  $$$$$$/| $$ \/  | $$| $$  | $$   | $$    /$$$$$$|  $$$$$$/
    \_/    |________/|__/  \__/|_______/  \______/ |__/     |__/|__/  |__/   |__/   |______/ \______/ 
";
            this.menuOptions.Add("1", new MenuOption("Feed Money", true));
            this.menuOptions.Add("2", new MenuOption("Get Change", true));
            this.menuOptions.Add("4", new MenuOption("Sales Report", false));
            this.menuOptions.Add("Q", new MenuOption("Quit", true));

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
                if (purchaseComplete)
                {
                    Pause(vendingMachine.Slots[choice].Product.YumYum());
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
                        // TODO This traps them into feeding money. Should there be an escape?
                        bool success = vendingMachine.FeedMoney(GetInteger("Enter a whole dollar amount to feed into the machine:"));
                        if (!success)
                        {
                            Pause("Unable to feed money. Please enter a whole number greater than zero.");
                        }
                        return true;
                    case "2":
                        Pause(vendingMachine.FinishTransaction());
                        // TODO Balance on screen doesn't update till enter is pressed. Does this matter?
                        break;
                    case "4":
                        vendingMachine.CreateSalesReport();
                        // TODO This should display a message with the file name generated
                        break;
                }
            }
            return true;
        }

    }
}
