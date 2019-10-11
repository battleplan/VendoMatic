using System;
using System.Collections.Generic;
using System.Text;

namespace Capstone.Models
{
    public class Gum : Product
    {
        //constructor
        public Gum(string name) : base(name)
        {

        }
        // TODO Set up summary
        public override string YumYum()
        {
            return "Chew Chew, Yum!";
        }
    }
}
