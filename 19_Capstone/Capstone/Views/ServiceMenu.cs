﻿using Capstone.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Capstone.Views
{
    public class ServiceMenu : CLIMenu
    {
        /// <summary>
        /// Constructor adds items to the top-level menu
        /// </summary>
        public ServiceMenu(VendingMachine vendingMachine) : base(vendingMachine)
        {
            Title = CreateTitle();
            menuOptions.Add("1", new MenuOption("Restock Change", true));
            menuOptions.Add("Q", new MenuOption("Main Menu", true));
        }

        protected override void DrawSlots(bool highlightIdentifier)
        {
            base.DrawSlots(false);
        }

        private string CreateTitle()
        {
            string changeAvailable = "";
            for (int i = 0; i < vendingMachine.ChangeCurrencyAvailable.Count; i++)
            {
                changeAvailable += vendingMachine.ChangeCurrencyAvailable[i].ToString();
                if (i < vendingMachine.ChangeCurrencyAvailable.Count - 1)
                {
                    changeAvailable += ", ";
                    if (i + 1 == vendingMachine.ChangeCurrencyAvailable.Count - 1)
                    {
                        changeAvailable += "and ";
                    }
                }
            }
            return $@"Service Menu
Change Available: {changeAvailable}";
        }

        /// <summary>
        /// The override of ExecuteSelection handles whatever selection was made by the user.
        /// This is where any business logic is executed.
        /// </summary>
        /// <param name="choice">"Key" of the user's menu selection</param>
        /// <returns></returns>
        protected override bool ExecuteSelection(string choice)
        {
            switch (choice)
            {
                case "1":
                    vendingMachine.ReplenishChange();
                    Title = CreateTitle();
                    DrawHeader(false);
                    DrawMenuOptions();
                    return true;
            }
            return true;
        }
    }
}
