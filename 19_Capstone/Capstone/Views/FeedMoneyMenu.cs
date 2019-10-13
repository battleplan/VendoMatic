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
            //Title = "Feed Money";
            menuOptions.Add("1", new MenuOption("Feed $1", true));
            menuOptions.Add("2", new MenuOption("Feed $2", true));
            menuOptions.Add("5", new MenuOption("Feed $5", true));
            menuOptions.Add("10", new MenuOption("Feed $10", true));
            //menuOptions.Add("20", new MenuOption("Feed $20", true));
            //menuOptions.Add("50", new MenuOption("Feed $50", true));
            //menuOptions.Add("100", new MenuOption("Feed $100", true));
            menuOptions.Add("Q", new MenuOption("Main Menu", true));
        }
        
        protected override void DrawSlots(bool highlightIdentifier)
        {
            base.DrawSlots(false);
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
                int depositAmount = int.Parse(choice);
                DrawHeader(false);
                if (vendingMachine.CanDespositAmountGivenCurrentChange(depositAmount))
                {
                    bool success = vendingMachine.FeedMoney(depositAmount);
                    if (!success)
                    {
                            Pause("Unable to feed money. Please enter a whole number greater than zero.");
                    }
                }
                else
                {
                    Pause("Not enough change available to deposite that amount.");
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
