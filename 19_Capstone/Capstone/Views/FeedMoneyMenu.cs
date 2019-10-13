using Capstone.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Capstone.Views
{
    public class FeedMoneyMenu : CLIMenu
    {
        /// <summary>
        /// Constructor adds items to the top-level menu
        /// </summary>
        public FeedMoneyMenu(VendingMachine vendingMachine) : base(vendingMachine)
        {
            Title = "*** Sub Menu ***";
            menuOptions.Add("1", new MenuOption("Feed $1", true));
            menuOptions.Add("2", new MenuOption("Feed $2", true));
            menuOptions.Add("5", new MenuOption("Feed $5", true));
            menuOptions.Add("10", new MenuOption("Feed $10", true));
            menuOptions.Add("Q", new MenuOption("Return to Main Menu", true));
        }

        /// <summary>
        /// The override of ExecuteSelection handles whatever selection was made by the user.
        /// This is where any business logic is executed.
        /// </summary>
        /// <param name="choice">"Key" of the user's menu selection</param>
        /// <returns></returns>
        protected override bool ExecuteSelection(string choice)
        {
            if (menuOptions.ContainsKey(choice))
            {
                DrawHeader();
                bool success = vendingMachine.FeedMoney(int.Parse(choice));
                if (!success)
                {
                    Pause("Unable to feed money. Please enter a whole number greater than zero.");
                }
            }
            else
            {
                Pause("Invalid selection.");
            }
            return true;
        }
    }
}
