using System;
using System.Collections.Generic;
using System.Text;

namespace Capstone.Models
{
    public abstract class Product
    {
        // TODO Set up summary
        public string Name { get; }

        //TODO Summary
        public int QuantitySold;

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
