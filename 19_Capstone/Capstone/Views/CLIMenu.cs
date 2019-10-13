﻿using Capstone.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Capstone.Views
{
    /// <summary>
    /// *CLIMenu* is an abstract class from which all other menu classes are derived.  To implement a menu, 
    /// you need to:
    ///     * Derived from CLIMenu
    ///     * Implement a constructor which builds the *menuOptions* dictionary.
    ///     * Override ExecuteSelection to handle weach of the user's selections.
    /// 
    /// </summary>
    public abstract class CLIMenu
    {
        /*** 
         * Model Data that this menu system needs to operate on goes here.
         ***/
        
        /// <summary>
        /// This is where every sub-menu puts its options for display to the user.
        /// </summary>
        protected Dictionary<string, MenuOption> menuOptions;

        /// <summary>
        /// The Title of this menu
        /// </summary>
        public string Title { get; set; }

        protected VendingMachine vendingMachine;

        /// <summary>
        /// Constructor - pass in model data here
        /// </summary>
        public CLIMenu(VendingMachine vendingMachine)
        {
            menuOptions = new Dictionary<string, MenuOption>();
            this.vendingMachine = vendingMachine;
        }

        protected const int charWidth = 120;

        /// <summary>
        /// Run starts the menu loop
        /// </summary>
        public void Run()
        {
            while (true)
            {
                Console.WindowHeight = (int)(Console.LargestWindowHeight * 0.8);
                Console.WindowWidth = (int)(Console.LargestWindowWidth * 0.8);

                DrawHeader();

                // TODO Dynamic width
                foreach (KeyValuePair<string, MenuOption> menuItem in menuOptions)
                {
                    if (menuItem.Value.IsVisible)
                    {
                        string menuItemDisplay = $"{menuItem.Key} - {menuItem.Value.Name}";
                        Console.Write($"{menuItemDisplay,-25}");
                    }
                }
                Console.WriteLine();
                Console.WriteLine(new string('=', charWidth));

                string choice = GetString(@"   ___ _  _  ___   ___  ___ ___ 
  / __| || |/ _ \ / _ \/ __| __|
 | (__| __ | (_) | (_) \__ \ _| 
  \___|_||_|\___/ \___/|___/___| OPTION:").ToUpper();

                if (menuOptions.ContainsKey(choice))
                {
                    if (choice == "Q")
                    {
                        break;
                    }
                    if (!ExecuteSelection(choice))
                    {
                        break;
                    }
                }
                else
                {
                    DrawHeader();
                    Pause("Invalid selection.");
                }

            }
        }

        protected void DrawHeader()
        {
            Console.Clear();
            //Console.WriteLine(this.Title);
            Console.WriteLine(@" 
 /$$    /$$ /$$$$$$$$ /$$   /$$ /$$$$$$$   /$$$$$$  /$$      /$$  /$$$$$$  /$$$$$$$$ /$$$$$$  /$$$$$$ 
| $$   | $$| $$_____/| $$$ | $$| $$__  $$ /$$__  $$| $$$    /$$$ /$$__  $$|__  $$__/|_  $$_/ /$$__  $$
| $$   | $$| $$      | $$$$| $$| $$  \ $$| $$  \ $$| $$$$  /$$$$| $$  \ $$   | $$     | $$  | $$  \__/
|  $$ / $$/| $$$$$   | $$ $$ $$| $$  | $$| $$  | $$| $$ $$/$$ $$| $$$$$$$$   | $$     | $$  | $$      
 \  $$ $$/ | $$__/   | $$  $$$$| $$  | $$| $$  | $$| $$  $$$| $$| $$__  $$   | $$     | $$  | $$      
  \  $$$/  | $$      | $$\  $$$| $$  | $$| $$  | $$| $$\  $ | $$| $$  | $$   | $$     | $$  | $$    $$
   \  $/   | $$$$$$$$| $$ \  $$| $$$$$$$/|  $$$$$$/| $$ \/  | $$| $$  | $$   | $$    /$$$$$$|  $$$$$$/
    \_/    |________/|__/  \__/|_______/  \______/ |__/     |__/|__/  |__/   |__/   |______/ \______/ 
");
            Console.WriteLine(new string('=', charWidth));
            Console.WriteLine();

            // Display slots
            List<string> slotsDisplay = vendingMachine.GetSlotsDisplayNames();
            int columnCounter = 0;
            for (int i = 0; i < slotsDisplay.Count; i++, columnCounter++)
            {
                // TODO Only display the selection numbers when available (not on feed money menu)

                // TODO Dynamic column width

                //int maxLength = 0;
                //int j = i;

                //for (int j = i; j < slotsDisplay.Count; j+=4)
                //{
                //    if (slotsDisplay[j].Length > maxLength)
                //    {
                //        maxLength = slotsDisplay[j].Length;
                //    }
                //}
                string slot = slotsDisplay[i];
                Console.Write($"{slot,-34}");
                if (columnCounter == 3)
                {
                    Console.WriteLine();
                    columnCounter = -1;
                }
            }
            Console.WriteLine(new string('=', charWidth));
            Console.WriteLine($@"  ___   _   _      _   _  _  ___ ___ 
 | _ ) /_\ | |    /_\ | \| |/ __| __|
 | _ \/ _ \| |__ / _ \| .` | (__| _| 
 |___/_/ \_\____/_/ \_\_|\_|\___|___|  {vendingMachine.Balance:C}");
            Console.WriteLine(new string('=', charWidth));
        }

        /// <summary>
        /// Given a valid menu selection, runs the approriate code to do what the user is asking for.
        /// </summary>
        /// <param name="choice">The menu option (key) selected by the user</param>
        /// <returns>True to keep executing the menu (loop), False to exit this menu (break)</returns>
        abstract protected bool ExecuteSelection(string choice);

        #region User Input Helper Methods
        /// <summary>
        /// This continually prompts the user until they enter a valid integer.
        /// </summary>
        /// <param name="message">The string to prompt the user with</param>
        /// <returns>A valid integer entered by the user</returns>
        protected int GetInteger(string message)
        {
            int resultValue = 0;
            while (true)
            {
                Console.Write(message + " ");
                string userInput = Console.ReadLine().Trim();
                if (int.TryParse(userInput, out resultValue))
                {
                    break;
                }
                else
                {
                    Console.WriteLine("!!! Invalid input. Please enter a valid whole number.");
                }
            }
            return resultValue;
        }

        /// <summary>
        /// This continually prompts the user until they enter a valid double.
        /// </summary>
        /// <param name="message">The string to prompt the user with</param>
        /// <returns>A valid double entered by the user</returns>
        protected double GetDouble(string message)
        {
            double resultValue = 0;
            while (true)
            {
                Console.Write(message + " ");
                string userInput = Console.ReadLine().Trim();
                if (double.TryParse(userInput, out resultValue))
                {
                    break;
                }
                else
                {
                    Console.WriteLine("!!! Invalid input. Please enter a valid decimal number.");
                }
            }
            return resultValue;
        }

        /// <summary>
        /// This continually prompts the user until they enter a valid bool.
        /// </summary>
        /// <param name="message">The string to prompt the user with</param>
        /// <returns>True or false.  The user can type Y or true for true values, N or false for false values.</returns>
        protected bool GetBool(string message)
        {
            bool resultValue = false;
            while (true)
            {
                Console.Write(message + " ");
                string userInput = Console.ReadLine().Trim();
                if (userInput.ToUpper() == "Y")
                {
                    resultValue = true;
                    break;
                }
                else if (userInput == "N")
                {
                    resultValue = false;
                    break;
                }
                else if (bool.TryParse(userInput, out resultValue))
                {
                    break;
                }
                else
                {
                    Console.WriteLine("!!! Invalid input. Please enter [True, False, Y or N].");
                }
            }
            return resultValue;
        }

        /// <summary>
        /// This continually prompts the user until they enter a valid string (1 or more characters).
        /// </summary>
        /// <param name="message">The string to prompt the user with</param>
        /// <returns>String entered by the user</returns>
        protected string GetString(string message)
        {
            while (true)
            {
                Console.Write(message + " ");
                string userInput = Console.ReadLine().Trim();
                if (!String.IsNullOrEmpty(userInput))
                {
                    return userInput;
                }
                else
                {
                    Console.WriteLine("!!! Invalid input. Please enter a valid decimal number.");
                }
            }
        }

        /// <summary>
        /// Shows a message to the user and waits for the user to hit return
        /// </summary>
        /// <param name="message">Displays a message to the user and then waits for them to hit Return.</param>
        protected void Pause(string message)
        {
            Console.WriteLine(message);
            Console.Write("Press Enter to continue.");
            Console.ReadLine();
        }
        #endregion

    }
}
