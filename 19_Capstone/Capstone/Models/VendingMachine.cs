using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Capstone.Models
{
    public class VendingMachine
    {
        // TODO Relevant methods should update this
        public decimal Balance;

        // TODO Assign this in constructor
        private readonly string inputFilePath;

        // TODO Relevant methods should update this
        private decimal totalSales;

        // TODO Set up summary
        // TODO Set up Product class
        // TODO Should this be private?
        // TODO Is this needed at all now that we have Slot?
        public List<Product> Products { get; private set; }


        // TODO Set up summary
        public List<Slot> Slots { get; private set; }



        //constructor
        
        public VendingMachine(string inputFilePath) 
        {
            this.inputFilePath = inputFilePath;
            Stock();
        }


        // TODO Set up summary
        // TODO Determine return value
        public bool Stock()
        {
            List<string> inputLines = new List<string>();
            // TODO Read from input file
            // TODO Create Product instances
            // TODO Add Products to Products
            //TODO bool?
            try
            {
                
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

            foreach(string line in inputLines)
            {
                //TODO price Decimal error alert input file
                string[] stockElements = line.Split("|");
                string identifier = stockElements[0];
                string nameProduct = stockElements[1];
                string priceProduct = stockElements[2];
                string productClass = stockElements[3];

                if (Products.IndexOf)
            }
            return true;
        }

        // TODO Set up summary
        public void FeedMoney()
        {
            // TODO Set up parameters
            // TODO Adjust Balance
            // TODO Determine return value
        }


        // TODO Set up summary
        private void MakeChange()
        {
            // TODO Set up parameters
            // TODO Adjust Balance
            // TODO Calculate quarters, dimes, and nickels
            // TODO Determine return value
        }

        // TODO Set up summary
        public void Purchase()
        {
            // TODO Set up parameters (Product? Slot?)
            // TODO Call Dispense() - this will call some kind of adjust balance method back here?
            // TODO Determine return value
            // TODO Should this be private? Abstract?
            // TODO Update total sales
        }

        // TODO Set up summary
        public void FinishTransaction()
        {
            // TODO Call MakeChange()
            // TODO Determine return value
        }

        // TODO Log transaction

        // TODO Sales report
    }
}
