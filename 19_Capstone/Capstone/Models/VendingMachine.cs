using System;
using System.Collections.Generic;
using System.Text;

namespace Capstone.Models
{
    public class VendingMachine
    {
        private decimal balance;

        // TODO Assign this in constructor
        private string inputFilePath;

        // TODO Relevant methods should update this
        private decimal totalSales;

        // TODO Set up summary
        // TODO Set up Product class
        // TODO Should this be private?
        public List<Product> Products { get; private set; }


        // TODO Set up summary
        // TODO Determine return value
        public void Stock()
        {
            // TODO Read from input file
            // TODO Create Product instances
            // TODO Add Products to Products
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
    }
}
