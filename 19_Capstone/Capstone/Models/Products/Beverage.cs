using System;
using System.Collections.Generic;
using System.Text;

namespace Capstone.Models
{
    public class Beverage : Product
    {
        //constructor
        public Beverage(string name) : base(name)
        {
            
        }



        // TODO Set up summary
        public override string YumYum()
        {
           return "Glug Glug, Yum!";
        }
    }
}
