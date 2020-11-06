namespace BlazorApp1.Data
{
    public class BioUnit
    {
        protected string color;
        public int living;
        public int livingTop;
        public int hungry;
        public int hungryTop;


        public int posx;
        public int posy;

        public Environment parent;

        public BioUnit(int x,int y,Environment e)
        {
            this.posx = x;
            this.posy = y;
            this.color="#46E300";
            this.parent=e;
        }
        public string myColor() => this.color;
        public virtual bool will_I_live() => true;
    }
}