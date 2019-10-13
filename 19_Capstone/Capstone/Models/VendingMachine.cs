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
        public decimal Balance { get; private set; }

        /// <summary>
        /// Total lifetime sales of the machine.
        /// </summary>
        public decimal TotalSales { get; private set; }

        private string fileDirectory;

        private string inputFileName;

        private string logFileName => "Log.txt";

        private string salesReportFileName => "SalesReport";

        /// <summary>
        /// Products in the machine accessible by their names.
        /// </summary>
        private Dictionary<string, Product> products { get; set; }
        public Dictionary<string, Product> Products
        {
            get
            {
                return new Dictionary<string, Product>(products);
            }
        }

        /// <summary>
        /// Slots in the machine accessible by their identifier.
        /// </summary>
        private Dictionary<string, Slot> slots { get; set; }
        public Dictionary<string, Slot> Slots
        {
            get
            {
                return new Dictionary<string, Slot>(slots);
            }
        }

        // Constructor
        /// <summary>
        /// Create a new vending machine.
        /// </summary>
        public VendingMachine()
        {
            products = new Dictionary<string, Product>();
            slots = new Dictionary<string, Slot>();
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
                
                bool priceWasParsed = decimal.TryParse(priceProduct, out decimal priceDecimal);

                if (priceWasParsed)
                {
                    // Find if Product already exists
                    Product product = null;
                    if (products.ContainsKey(nameProduct))
                    {
                        product = products[nameProduct];
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
                        products[nameProduct] = product;
                    }
                    
                    // Create slot and put product in it
                    slots[identifier] = new Slot(identifier, product, priceDecimal);
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
        public bool FeedMoney(int money)
        {
            if (money <= 0)
            {
                return false;
            }
            Balance += money;
            string logText = $"FEED MONEY: {money:C}";
            TransactionLog(logText);
            return true;
        }

        
        /// <summary>
        /// Add a new entry to the transaction log.
        /// </summary>
        /// <param name="logText">Text to write to the log.</param>
        private void  TransactionLog(string logText)
        {
            if (fileDirectory != "" && fileDirectory != null && Directory.Exists(fileDirectory))
            {
                string time = DateTime.Now.ToString();
                try
                {
                    using (StreamWriter log = new StreamWriter(Path.Combine(fileDirectory, logFileName), true))
                    {
                        log.WriteLine($"{time} {logText} {Balance:C}");
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
            if (slots.ContainsKey(slotIdentifier))
            {
                slot = slots[slotIdentifier];
            }
            else
            {
                return false;
            }

            if (Balance < slot.Price)
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
            List<string> slotIdentifiers = new List<string>(this.slots.Keys);
            foreach (string key in slotIdentifiers)
            {
                slots.Add(this.slots[key].DisplayName);
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

            string totalChange = $"Your Change is {quarters} Quarter(s), {dimes} Dime(s), and {nickels} Nickel(s)";
            return totalChange;
            // TODO Determine return value - new Money class with subclasses of Quarter, Dime, and Nickel?
        }

        public string CreateSalesReport()
        {
            string outputFileName;
            string result;
            try
            {
                List<string> productsKeys = new List<string>(products.Keys);
                productsKeys.Sort();

                outputFileName = salesReportFileName + " " + DateTime.Now + ".txt";
                char[] invalidChars = Path.GetInvalidFileNameChars();
                foreach (char invalid in invalidChars)
                {
                    outputFileName = outputFileName.Replace(invalid.ToString(), "_");
                }

                string outputPath = Path.Combine(fileDirectory, outputFileName);
                result = outputFileName;

                using (StreamWriter sw = new StreamWriter(outputPath))
                {
                    foreach (string key in productsKeys)
                    {
                        string line = $"{products[key].Name}|{products[key].QuantitySold}";
                        result += "\n" + line;
                        sw.WriteLine(line);
                    }

                    string totalSales = $"Total Sales: {TotalSales:C}";
                    result += "\n\n" + totalSales;

                    sw.WriteLine();
                    sw.WriteLine(totalSales);
                }
            }
            catch (Exception)
            {
                return "";
            }

            return result;
        }
    }
}
