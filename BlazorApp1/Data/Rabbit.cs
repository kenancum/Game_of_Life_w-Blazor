using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlazorApp1.Data
{
    public class Rabbit : Data.BioUnit
    {
        public Rabbit(int x, int y, Data.Environment e) : base(x, y, e) {
            this.color = "#fafafa";
            this.living = 0;
            this.livingTop = 6;
            this.hungryTop = 3;
        }

        public override bool will_I_live()
        {
            this.hungry++;
            this.living++;
            if ((this.living - 1) >= this.livingTop) return false;
            if ((this.hungry - 1) >= this.hungryTop) return false;
            return true;

        }
        public void eat()
        {
            this.hungry=0;
        }
    }


}
