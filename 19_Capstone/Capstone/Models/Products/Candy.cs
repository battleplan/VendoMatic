using System;
using System.Collections.Generic;
using System.Text;

namespace Capstone.Models
{
    public class Candy : Product
    {
        //constructor
        public Candy(string name) : base(name)
        {

        }

        // TODO Set up summary
        public override string YumYum()
        {
           return "Munch Munch, Yum!";
        }
    }
}
