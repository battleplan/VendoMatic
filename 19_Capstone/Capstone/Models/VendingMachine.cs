using Capstone.Models.Monies;
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

        /// <summary>
        /// The directory the input file was found in.
        /// </summary>
        private string fileDirectory;

        /// <summary>
        /// The name of the input file.
        /// </summary>
        private string inputFileName;

        /// <summary>
        /// The name of the file to log transactions to.
        /// </summary>
        private string logFileName => "Log.txt";

        /// <summary>
        /// The name of the file to write the sales report to, minus the extension.
        /// </summary>
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

        /// <summary>
        /// Currency in machine that can be used for change.
        /// </summary>
        private Dictionary<decimal, Money> currencyDenominations { get; set; }
        public List<Money> ChangeCurrencyAvailable
        {
            get
            {
                List<Money> output = new List<Money>();
                foreach (KeyValuePair<decimal, Money> currency in currencyDenominations)
                {
                    output.Add(currency.Value);
                }
                return output;
            }
        }

        /// <summary>
        /// Total value of all change currency in the machine.
        /// </summary>
        private decimal TotalChangeAvailable
        {
            get
            {
                decimal output = 0;
                foreach (KeyValuePair<decimal, Money> currency in currencyDenominations)
                {
                    output += currency.Value.TotalValue;
                }
                return output;
            }
        }

        /// <summary>
        /// What currency the machine can output in change.
        /// </summary>
        private List<decimal> validChangeDenominations
        {
            get
            {
                List<decimal> output = new List<decimal>() { 0.25M, 0.10M, 0.05M };
                return output;
            }
        }

        /// <summary>
        /// What currency the machine can be fed.
        /// </summary>
        private List<decimal> validFeedDenominations
        {
            get
            {
                List<decimal> output = new List<decimal>() { 1M, 2M, 5M, 10M };
                return output;
            }
        }
        public List<decimal> ValidFeedDenominations
        {
            get
            {
                return new List<decimal>(validFeedDenominations);
            }
        }

        /// <summary>
        /// Returns if amount is okay to desposit given current amount of change available.
        /// </summary>
        /// <param name="depositAmount">The amount wished to desposit.</param>
        /// <returns>Whether or not the deposit can be made.</returns>
        public bool CanDespositAmountGivenCurrentChange(int depositAmount)
        {
            return depositAmount + Balance <= TotalChangeAvailable;
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
            currencyDenominations = new Dictionary<decimal, Money>();
            RemoveBills();
            ReplenishChange();
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
                    identifier = stockElements[0].ToUpper();
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

        public void RemoveBills()
        {
            int replenishQuantity = 0;
            Money money = null;

            List<decimal> denominations = new List<decimal>(validFeedDenominations);
            denominations.Sort();
            denominations.Reverse();
            foreach (decimal denomination in denominations)
            {
                if (currencyDenominations.ContainsKey(denomination))
                {
                    money = currencyDenominations[denomination];
                    money.SetQuantity(replenishQuantity);
                }
                else
                {
                    switch (denomination)
                    {
                        case 100M:
                            money = new HundredDollar(false, replenishQuantity);
                            break;
                        case 50M:
                            money = new FiftyDollar(false, replenishQuantity);
                            break;
                        case 20M:
                            money = new TwentyDollar(false, replenishQuantity);
                            break;
                        case 10M:
                            money = new TenDollar(false, replenishQuantity);
                            break;
                        case 5M:
                            money = new FiveDollar(false, replenishQuantity);
                            break;
                        case 2M:
                            money = new TwoDollar(false, replenishQuantity);
                            break;
                        case 1M:
                            money = new OneDollar(false, replenishQuantity);
                            break;
                    }
                    if (money != null)
                    {
                        currencyDenominations.Add(denomination, money);
                    }
                }
            }
        }

        /// <summary>
        /// Replenishes all change up to maximum amount.
        /// </summary>
        public void ReplenishChange()
        {
            int replenishQuantity = 200;
            Money money = null;

            List<decimal> denominations = new List<decimal>(validChangeDenominations);
            denominations.Sort();
            denominations.Reverse();
            foreach (decimal denomination in denominations)
            {
                if (currencyDenominations.ContainsKey(denomination))
                {
                    money = currencyDenominations[denomination];
                    money.SetQuantity(replenishQuantity);
                }
                else
                {
                    switch (denomination)
                    {
                        case 0.25M:
                            money = new Quarter(true, replenishQuantity);
                            break;
                        case 0.10M:
                            money = new Dime(true, replenishQuantity);
                            break;
                        case 0.05M:
                            money = new Nickel(true, replenishQuantity);
                            break;
                    }
                    if (money != null)
                    {
                        currencyDenominations.Add(denomination, money);
                    }
                }
            }
        }

        /// <summary>
        /// Feed money into the machine.
        /// </summary>
        /// <param name="money">Integer value of dollars to feed into the machine.</param>
        public bool FeedMoney(int money)
        {
            if (money <= 0 || money + Balance > TotalChangeAvailable)
            {
                return false;
            }
            currencyDenominations[money].AddQuantity(1);
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
            decimal startingBalance = Balance;
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
                TransactionLog($"{slot.Product.Name} {slot.Identifier} {startingBalance:C}");
                return true;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// Give change back to the machine operator.
        /// </summary>
        /// <returns></returns>
        public List<Money> FinishTransaction()
        {
            List<Money> changeCurrency = new List<Money>();

            if (TotalChangeAvailable > 0 && Balance > 0)
            {
                decimal currencyThreshold = 0.25M;
                if (currencyDenominations[currencyThreshold].Count > 0 && Balance >= currencyThreshold)
                {
                    Money quarters = new Quarter(true);
                    quarters.MakeChange(currencyDenominations[currencyThreshold], Balance);
                    changeCurrency.Add(quarters);
                    Balance -= quarters.TotalValue;
                }

                currencyThreshold = 0.10M;
                if (currencyDenominations[currencyThreshold].Count > 0 && Balance >= currencyThreshold)
                {

                    Money dimes = new Dime(true);
                    dimes.MakeChange(currencyDenominations[currencyThreshold], Balance);
                    changeCurrency.Add(dimes);
                    Balance -= dimes.TotalValue;
                }

                currencyThreshold = 0.05M;
                if (currencyDenominations[currencyThreshold].Count > 0 && Balance >= currencyThreshold)
                {

                    Money nickels = new Nickel(true);
                    nickels.MakeChange(currencyDenominations[currencyThreshold], Balance);
                    changeCurrency.Add(nickels);
                    Balance -= nickels.TotalValue;
                }

                decimal changeGiven = 0;
                foreach (Money currency in changeCurrency)
                {
                    changeGiven += currency.TotalValue;
                }

                TransactionLog($"GIVE CHANGE: {changeGiven:C}");
            }

            return changeCurrency;
           
            // Old version before Money class

            //decimal changeGiven = Balance;
            //int quarters = 0;
            //int dimes = 0;
            //int nickels = 0;
            //while (Balance > 0)
            //{
            //    if (Balance >= (decimal).25)
            //    {
            //        Balance -= (decimal).25;
            //        quarters++;
            //    }
            //    else if (Balance >= (decimal).10)
            //    {
            //        Balance -= (decimal).10;
            //        dimes++;
            //    }
            //    else if (Balance >= (decimal).05)
            //    {
            //        Balance -= (decimal).05;
            //        nickels++;
            //    }
            //}

            //TransactionLog($"GIVE CHANGE: {changeGiven:C}");

            //string totalChange = $"Your Change is {quarters} Quarter(s), {dimes} Dime(s), and {nickels} Nickel(s)";
            //return totalChange;
        }

        /// <summary>
        /// Creates a file with the sales of the machine since it was lasted started.
        /// </summary>
        /// <returns>Sales report file name and data.</returns>
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
