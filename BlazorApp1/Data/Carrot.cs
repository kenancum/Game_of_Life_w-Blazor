using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlazorApp1.Data
{
    public class Carrot : Data.BioUnit
    {

        public Carrot(int x, int y, Data.Environment e) : base(x, y, e) {
            this.color = "#fa5511";
            this.living = 0;
            this.livingTop = 3;
        }
        public override bool will_I_live()
        {
            this.living++;
            if ((this.living - 1) >= this.livingTop) return false;
            return true;
        }
    }
}
