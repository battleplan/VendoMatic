using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Capstone.Models
{
    public class VendingMachine
    {
        /// <summary>
        /// Current dollar amount fed into the machine.
        /// </summary>
        public decimal Balance;

        /// <summary>
        /// Total lifetime sales of the machine.
        /// </summary>
        public decimal TotalSales { get; private set; }

        private string fileDirectory;

        private string inputFileName;

        private string logFileName => "Log.txt";

        private string salesReportFileName => "SalesReport.txt";

        /// <summary>
        /// Products in the machine accessible by their names.
        /// </summary>
        public Dictionary<string, Product> Products { get; private set; }


        /// <summary>
        /// Slots in the machine accessible by their identifier.
        /// </summary>
        public Dictionary<string, Slot> Slots { get; private set; }

        // Constructor
        /// <summary>
        /// Create a new vending machine.
        /// </summary>
        public VendingMachine()
        {
            Products = new Dictionary<string, Product>();
            Slots = new Dictionary<string, Slot>();
            fileDirectory = Directory.GetCurrentDirectory();
        }


        /// <summary>
        /// Use a pipe-delimited input file to stock the vending machine.
        /// </summary>
        /// <param name="inputFilePath">Path to the pipe-delimited input file.</param>
        /// <returns>Success of stocking the machine completely.</returns>
        public bool StockFromFile(string inputFilePath)
        {
            List<string> inputLines = new List<string>();
            // TODO Read from input file - check this!!
            //TODO bool?
            try
            {
                fileDirectory = Path.GetDirectoryName(inputFilePath);
                inputFileName = Path.GetFileName(inputFilePath);
                
                using (StreamReader sr = new StreamReader(inputFilePath))
                {
                    while (!sr.EndOfStream)
                    {
                        inputLines.Add(sr.ReadLine()); 

                    }
                }
            }
            catch (FileNotFoundException)
            {
                //TODO dont forget about this
                 return false;
            }
            catch (Exception)
            {
                return false;
            }

            return Stock(inputLines.ToArray());
        }

        /// <summary>
        /// Stock machine from a string array.
        /// </summary>
        /// <param name="inputLines">Pipe-delimited string array.</param>
        /// <returns>Success of completely stocking the machine.</returns>
        public bool Stock(string[] inputLines)
        {
            // TODO This should load in the sales report history for each product
            foreach (string line in inputLines)
            {
                string identifier = "";
                string nameProduct = "";
                string priceProduct = "";
                string productClass = "";
                try
                {
                    string[] stockElements = line.Split("|");
                    identifier = stockElements[0];
                    nameProduct = stockElements[1];
                    priceProduct = stockElements[2];
                    productClass = stockElements[3];
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"An error occurred stocking the machine: {ex.Message}");
                    Console.ReadKey();
                    return false;
                }

                decimal priceDecimal = 0;
                bool priceWasParsed = decimal.TryParse(priceProduct, out priceDecimal);

                if (priceWasParsed)
                {
                    // Find if Product already exists
                    Product product = null;
                    if (Products.ContainsKey(nameProduct))
                    {
                        product = Products[nameProduct];
                    }
                    if (product == null)
                    {
                        // Create new product
                        switch (productClass)
                        {
                            case "Chip":
                                product = new Chip(nameProduct);
                                break;
                            case "Drink":
                                product = new Beverage(nameProduct);
                                break;
                            case "Gum":
                                product = new Gum(nameProduct);
                                break;
                            case "Candy":
                                product = new Candy(nameProduct);
                                break;
                        }
                        Products[nameProduct] = product;
                    }
                    
                    // Create slot and put product in it
                    Slots[identifier] = (new Slot(identifier, product, priceDecimal));
                }
                else
                {
                    return false;
                }
            }
            return true;
        }

        /// <summary>
        /// Feed money into the machine.
        /// </summary>
        /// <param name="money">Integer value of dollars to feed into the machine.</param>
        public void FeedMoney(int money)
        {
            Balance += money;
            string logText = ($"FEED MONEY: {money:C}"); 
            TransactionLog(logText);
            //TODO transaction log - test that this works
            // TODO Determine return value
            // TODO This doesn't check for a negative for $0 value
        }

        //TODO check path location for Log and Write a Catch block
        /// <summary>
        /// Add a new entry to the transaction log.
        /// </summary>
        /// <param name="logText">Text to write to the log.</param>
        private void  TransactionLog(string logText)
        {
            // TODO Make sure this works
            if (fileDirectory != "" && fileDirectory != null && Directory.Exists(fileDirectory))
            {
                string time = DateTime.Now.ToString();
                string balance = Balance.ToString();
                try
                {
                    using (StreamWriter log = new StreamWriter(Path.Combine(fileDirectory, logFileName), true))
                    {
                        log.WriteLine($"{time} {logText} {balance}");
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"An error occurred writing the transaction log: {ex.Message}");
                    Console.ReadKey();
                }
            }
        }

        /// <summary>
        /// Make a purchase from a given slot in the machine.
        /// </summary>
        /// <param name="slotIdentifier">The identifier for the slot.</param>
        /// <returns>Success of making the purchase.</returns>
        public bool Purchase(string slotIdentifier)
        {
            Slot slot;
            if (Slots.ContainsKey(slotIdentifier))
            {
                slot = Slots[slotIdentifier];
            }
            else
            {
                return false;
            }
            bool slotDispensed = slot.Dispense();
            if (slotDispensed)
            {
                slot.Product.SellProduct();
                TotalSales += slot.Price;
                Balance -= slot.Price;
                TransactionLog($"{slot.Product.Name} {slot.Identifier} {slot.Price:C}");
                return true;
                // TODO Make sure transaction log works
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// Get display names of the information in all slots loading into the machine.
        /// </summary>
        /// <returns>List of each slot's display name.</returns>
        public List<string> GetSlotsDisplayNames()
        {
            List<string> slots = new List<string>();
            List<string> slotIdentifiers = new List<string>(Slots.Keys);
            foreach (string key in slotIdentifiers)
            {
                slots.Add(Slots[key].DisplayName);
            }

            return slots;
        }

        /// <summary>
        /// Give change back to the machine operator.
        /// </summary>
        /// <returns></returns>
        public string FinishTransaction()
        {
            decimal changeGiven = Balance;
            int quarters = 0;
            int dimes = 0;
            int nickels = 0;
            while (Balance > 0)
            {
                if (Balance >= (decimal).25)
                {
                    Balance -= (decimal).25;
                    quarters++;
                }
                else if (Balance >= (decimal).10)
                {
                    Balance -= (decimal).10;
                    dimes++;
                }
                else if (Balance >= (decimal).05)
                {
                    Balance -= (decimal).05;
                    nickels++;
                }
            }

            TransactionLog($"GIVE CHANGE: {changeGiven:C}");

            string totalChange = $"Your Change is {quarters} Quarters, {dimes} Dimes, and {nickels} Nickels";
            return totalChange;
            // TODO Determine return value - new Money class with subclasses of Quarter, Dime, and Nickel?
            // TODO Make sure this log works
        }

        // TODO Sales report
    }
}
