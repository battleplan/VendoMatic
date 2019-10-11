using Capstone.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Capstone.Views
{
    public class SubMenu : CLIMenu
    {
        /// <summary>
        /// Constructor adds items to the top-level menu
        /// </summary>
        public SubMenu(VendingMachine vendingMachine) : base(vendingMachine)
        {
            this.Title = "*** Sub Menu ***";
            this.menuOptions.Add("1", "Option One");
            this.menuOptions.Add("2", "Option 2");
            this.menuOptions.Add("Q", "Return to Main Menu");
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
                    Console.WriteLine("You selected option 1");
                    Pause("Press any key");
                    return true;
                case "2":
                    Console.WriteLine("You selected option 2. When it is done, the sub-manu will exit, because it returns false.");
                    Pause("Press any key");
                    return false;
            }
            return true;
        }
    }
}
