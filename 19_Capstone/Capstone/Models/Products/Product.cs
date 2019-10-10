using System;
using System.Collections.Generic;
using System.Text;

namespace Capstone.Models
{
    public abstract class Product
    {
        // TODO Set up summary
        public string Name { get; }


        // TODO Is this needed here?
        // TODO Set up summary
        //public string Price { get; }

        //TODO Summary
        public int QuantitySold;

        // TODO Override this method
        public abstract void YumYum();

        //constructor
        public Product(string name)
        {
            Name = name;
        }

        // TODO Add Summary
        public void SellProduct()
        {
            QuantitySold++;
        }
    }
}
