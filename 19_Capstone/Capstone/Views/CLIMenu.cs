using Capstone.Models;
using Capstone.Models.Monies;
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

        protected const int charWidth = 126;

        /// <summary>
        /// Run starts the menu loop
        /// </summary>
        public void Run()
        {
            while (true)
            {
                Console.WindowHeight = (int)(Console.LargestWindowHeight * 0.8);
                Console.WindowWidth = (int)(Console.LargestWindowWidth * 0.8);

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

            DrawSlots(true);
            Console.WriteLine(new string('=', charWidth));
            Console.WriteLine($@"  ___   _   _      _   _  _  ___ ___ 
 | _ ) /_\ | |    /_\ | \| |/ __| __|
 | _ \/ _ \| |__ / _ \| .` | (__| _| 
 |___/_/ \_\____/_/ \_\_|\_|\___|___|  {vendingMachine.Balance:C}");
            Console.WriteLine(new string('=', charWidth));
        }

        protected void DrawMenuOptions()
        {
            // Find number of visible menuOptions
            int optionCount = 0;
            foreach (KeyValuePair<string, MenuOption> menuItem in menuOptions)
            {
                if (menuItem.Value.IsVisible)
                {
                    optionCount++;
                }
            }
            int optionWidth = charWidth / optionCount;

            foreach (KeyValuePair<string, MenuOption> menuItem in menuOptions)
            {
                if (menuItem.Value.IsVisible)
                {
                    string key = $" [{menuItem.Key}] ";
                    int keyLength = key.Length;
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.Write(key);
                    Console.ForegroundColor = ConsoleColor.White;
                    Console.Write($"{PadWidth(menuItem.Value.Name, optionWidth - keyLength)}");
                }
            }
            Console.WriteLine();
            Console.WriteLine(new string('=', charWidth));
        }

        protected virtual void DrawSlots(bool displayIdentifier)
        {
            // Get all slots in the vending machine
            List<string> slotIdentifiers = new List<string>(vendingMachine.Slots.Keys);
            List<string> slotsDisplay = new List<string>();
            List<Slot> slots = new List<Slot>();

            // Determine how wide each column should be
            foreach (string id in slotIdentifiers)
            {
                Slot slot = vendingMachine.Slots[id];
                slots.Add(slot);
                string slotDisplay = " ";
                if (displayIdentifier)
                {
                    slotDisplay += $"[{slot.Identifier}] ";
                }
                slotDisplay += $"{SlotInfo(slot)}  ";
                slotsDisplay.Add(slotDisplay);
            }

            const int columnCount = 4;
            Dictionary<int, int> slotsColumnWidth = new Dictionary<int, int>();
            for (int i = 0; i < slotsDisplay.Count; i++)
            {
                string slotDisplay = slotsDisplay[i];
                int slotWidth = slotDisplay.Length;
                int slotColumn = i % columnCount;
                if (!slotsColumnWidth.ContainsKey(slotColumn) || slotsColumnWidth[slotColumn] < slotWidth)
                {
                    slotsColumnWidth[slotColumn] = slotWidth;
                }
            }

            // Draw the slots
            int columnCounter = 0;
            for (int i = 0; i < slots.Count; i++, columnCounter++)
            {
                Slot slot = slots[i];
                int slotIdLength = 0;
                if (displayIdentifier)
                {
                    string slotId = $" [{slot.Identifier}]";
                    slotIdLength = slotId.Length;
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.Write(slotId);
                    Console.ForegroundColor = ConsoleColor.White;
                }
                int displayWidth = slotsColumnWidth[i % columnCount] + 2 - slotIdLength;
                string displayInfo = PadWidth($" {SlotInfo(slot)}  ", displayWidth);
                Console.Write($"{displayInfo}");
                if (columnCounter == columnCount - 1)
                {
                    Console.WriteLine();
                    columnCounter = -1;
                }
            }
        }

        protected string SlotInfo(Slot slot)
        {
            if (slot.HasStock)
            {
                return $"{slot.Price:C} |{slot.QuantityRemaining}| {slot.Product.Name}";
            }
            else
            {
                return $"{slot.Product.Name} SOLD OUT";
            }
        }

        /// <summary>
        /// Given a valid menu selection, runs the approriate code to do what the user is asking for.
        /// </summary>
        /// <param name="choice">The menu option (key) selected by the user</param>
        /// <returns>True to keep executing the menu (loop), False to exit this menu (break)</returns>
        abstract protected bool ExecuteSelection(string choice);

        protected string PadWidth(string str, int finalWidth)
        {
            while (str.Length < finalWidth)
            {
                str += " ";
            }
            return str;
        }

        public string MoneyDisplay(List<Money> monies)
        {
            string moneyDisplay = "";
            if (monies.Count > 0)
            {
                moneyDisplay = "";
                for (int i = 0; i < monies.Count; i++)
                {
                    string currency = monies[i].Count + " " + monies[i].Name;
                    if (i == monies.Count - 1)
                    {
                        if (i == 0)
                        {
                            moneyDisplay += $"{currency}";
                        }
                        else
                        {
                            moneyDisplay += $"and {currency}";
                        }
                    }
                    else if (monies.Count > 2)
                    {
                        moneyDisplay += $"{currency}, ";
                    }
                    else
                    {
                        moneyDisplay += $"{currency} ";
                    }
                }
            }

            return moneyDisplay;
        }

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
                    DrawHeader();
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
                    DrawHeader();
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
                    DrawHeader();
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
                DrawHeader();
                DrawMenuOptions();
                Console.Write(message + " ");
                string userInput = Console.ReadLine().Trim();
                if (!String.IsNullOrEmpty(userInput))
                {
                    return userInput;
                }
                else
                {
                    DrawHeader();
                    Pause("!!! Invalid input. Please enter a valid menu option.");

                    // TODO Why does pressing enter take away all menu options?
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
