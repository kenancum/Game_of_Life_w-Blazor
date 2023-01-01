using System;
using System.Collections.Generic;
using System.ComponentModel;
namespace BlazorApp1.Data
{
    public class Environment
    {
        private int rows = 1;
        private int cols = 1;
        private BioUnit[,] cell; 

        public Environment(int rows_,int columns_) {
            this.rows = rows_;
            this.cols = columns_;
            this.cell = new BioUnit[this.rows,this.cols];
            for(var i=0; i<this.rows; i++)
            for(var j=0; j<this.cols; j++)
                this.cell[i,j] = null;
        }
        public int total_of_rows(){
            return this.rows;
        }
        public int total_of_cols() {
            return this.cols;
        }        
        private bool rightPos(int i,int j){
            return i>=0 && i<this.rows && j>=0 && j<this.cols;
        }
        public void insert(int i, int j, BioUnit been)
        {
            if (this.rightPos(i, j))
            {
                this.cell[i, j] = been;
            }
        }
        public void onOff(int row, int col)
        {
            if (this.rightPos(row, col))
            {
                if (this.cell[row, col] != null)
                    this.cell[row, col] = null;
                else
                    this.cell[row, col] = new BioUnit(row, col, this);
            }
        }
        public BioUnit biounit(int i, int j)
        {
            if (this.rightPos(i, j))
            {
                return this.cell[i, j];
            }
            return null;
        }
        public List<BioUnit>neighbors(int i,int j)
        {
            List<BioUnit> ans = new List<BioUnit>();
            if (this.rightPos(i, j))
            {
                if (this.rightPos(i - 1, j - 1) && this.cell[i - 1, j - 1] != null) ans.Add(this.cell[i - 1, j - 1]);
                if (this.rightPos(i - 1, j ) && this.cell[i - 1, j ] != null) ans.Add(this.cell[i - 1, j ]);
                if (this.rightPos(i - 1, j + 1) && this.cell[i - 1, j + 1] != null) ans.Add(this.cell[i - 1, j + 1]);
                if (this.rightPos(i, j - 1) && this.cell[i, j - 1] != null) ans.Add(this.cell[i, j - 1]);
                if (this.rightPos(i, j + 1) && this.cell[i, j + 1] != null) ans.Add(this.cell[i, j + 1]);
                if (this.rightPos(i + 1, j - 1) && this.cell[i + 1, j - 1] != null) ans.Add(this.cell[i + 1, j - 1]);
                if (this.rightPos(i + 1, j ) && this.cell[i + 1, j ] != null) ans.Add(this.cell[i + 1, j ]);
                if (this.rightPos(i + 1, j + 1) && this.cell[i + 1, j + 1] != null) ans.Add(this.cell[i + 1, j + 1]);

            }
            return ans;
        }
        public int surrondingNeighborns(int i,int j, String specie)
        {
            int ans = 0;
            List<BioUnit> surr = this.neighbors(i, j);
            
            foreach (object unit in surr)
            {
                if (this.specie(unit) == specie) ans++;                
            }
            return ans;
        }
        public string specie(Object obj)
        {
            String[] w;
            if (obj == null) return "";
            w = TypeDescriptor.GetClassName(obj).Split(".");
            return w[w.Length - 1];
        }
        public void nextConwayStep()
        {
            int n;
            bool[,] aux = new bool[this.rows, this.cols];
            for (var i = 0; i < this.rows; i++)
                for (var j = 0; j < this.cols; j++)
                {
                    n = this.surrondingNeighborns(i, j,"BioUnit");
                    if (n == 3)
                        aux[i, j] = true;
                    else if (n == 2 && this.cell[i,j]!=null)
                        aux[i, j] = true;
                    else
                        aux[i, j] = false;
                }
            for (var i = 0; i < this.rows; i++)
                for (var j = 0; j < this.cols; j++)
                {
                    if (aux[i, j] && this.cell[i, j] == null)
                        this.cell[i, j]= new BioUnit(i,j,this);
                    else if(!aux[i,j]&& this.cell[i,j]!=null)
                        this.cell[i, j] = null;
                }
        }
        public void put_pattern(int x,int y, string pattern)
        {
            if (pattern.Equals("pentadecathlon"))
            {
                for(var i = 0; i < 8; i++)
                {
                    for (int j = 0; j < 3; j++)
                    {
                        if (!((i == 1 && j == 1) || (i == 6 && j == 1)))
                        {
                            this.insert(x + i, y + j, new BioUnit(x + i, y + j, this));
                        }
                    }
                }
            }
            else if (pattern.Equals("blink"))
            {
                for (var i = 0; i < 3; i++)
                {
                    this.insert(x, y + i, new BioUnit(x , y + i , this));
                }
            }
            else if (pattern.Equals("toad"))
            {
                for (int i = 0; i < 2; i++)
                {
                    for (int j = 0; j < 3; j++)
                    {
                        this.insert(x + i, y + 1-i+j, new BioUnit(x + i, y + 1-i+j, this));                        
                    }
                }
            }
            else if (pattern.Equals("bipole"))
            {
                for (int i = 0; i < 2; i++)
                {
                    for (int j = 0; j < 5; j++)
                    {
                        if ((i == 0 && j > 2) || ((i == 1) && ((j != 0) && (j % 2 == 0))))
                        {
                            this.insert(x + i, y + j, new BioUnit(x + i, y + j, this));
                            this.insert(x + 4 - i, y + 4 - j, new BioUnit(x + 4 - i, y + 4 - j, this));
                        }
                    }
                }
            }
            else if (pattern.Equals("a_for_all"))
            {
                for (int i = 0; i < 5; i++)
                {
                    for (int j = 0; j < 5; j++)
                    {
                        if ((i == 0 && j == 4) || (i==1&&j==3)||(i==2&&j>2)||((i==3)&&((j==1)||(j==3)))|| (i == 4 && j == 0))
                        {
                            this.insert(x + i, y + j, new BioUnit(x + i, y + j, this));
                            this.insert(x + i, y + 9 - j, new BioUnit(x + i, y + 9 - j, this));
                            this.insert(x + 9 - i, y + j, new BioUnit(x + 9 - i, y + j, this));
                            this.insert(x + 9 - i, y + 9 - j, new BioUnit(x + 9 - i, y + 9 - j, this));
                        }
                    }
                }
            }
            else if (pattern.Equals("gosper_glider"))
            {
                this.insert(x + 5, y + 1, new BioUnit(x + 5, y + 1, this));
                this.insert(x + 5, y + 2, new BioUnit(x + 5, y + 2, this));
                this.insert(x + 6, y + 1, new BioUnit(x + 6, y + 1, this));
                this.insert(x + 6, y + 2, new BioUnit(x + 6, y + 2, this));
                this.insert(x + 3, y + 13, new BioUnit(x + 3, y + 13, this));
                this.insert(x + 3, y + 14, new BioUnit(x + 3, y + 14, this));
                this.insert(x + 4, y + 12, new BioUnit(x + 4, y + 12, this));
                this.insert(x + 5, y + 11, new BioUnit(x + 5, y + 11, this));
                this.insert(x + 6, y + 11, new BioUnit(x + 6, y + 11, this));
                this.insert(x + 7, y + 11, new BioUnit(x + 7, y + 11, this));
                this.insert(x + 8, y + 12, new BioUnit(x + 8, y + 12, this));
                this.insert(x + 9, y + 13, new BioUnit(x + 9, y + 13, this));
                this.insert(x + 9, y + 14, new BioUnit(x + 9, y + 14, this));
                this.insert(x + 6, y + 15, new BioUnit(x + 6, y + 15, this));
                this.insert(x + 4, y + 16, new BioUnit(x + 4, y + 16, this));
                this.insert(x + 8, y + 16, new BioUnit(x + 8, y + 16, this));
                this.insert(x + 5, y + 17, new BioUnit(x + 5, y + 17, this));
                this.insert(x + 6, y + 17, new BioUnit(x + 6, y + 17, this));
                this.insert(x + 7, y + 17, new BioUnit(x + 7, y + 17, this));
                this.insert(x + 6, y + 18, new BioUnit(x + 6, y + 18, this));
                this.insert(x + 3, y + 21, new BioUnit(x + 3, y + 21, this));
                this.insert(x + 4, y + 21, new BioUnit(x + 4, y + 21, this));
                this.insert(x + 5, y + 21, new BioUnit(x + 5, y + 21, this));
                this.insert(x + 3, y + 22, new BioUnit(x + 3, y + 22, this));
                this.insert(x + 4, y + 22, new BioUnit(x + 4, y + 22, this));
                this.insert(x + 5, y + 22, new BioUnit(x + 5, y + 22, this));
                this.insert(x + 2, y + 23, new BioUnit(x + 2, y + 23, this));
                this.insert(x + 6, y + 23, new BioUnit(x + 6, y + 23, this));
                this.insert(x + 1, y + 25, new BioUnit(x + 1, y + 25, this));
                this.insert(x + 2, y + 25, new BioUnit(x + 2, y + 25, this));
                this.insert(x + 6, y + 25, new BioUnit(x + 6, y + 25, this));
                this.insert(x + 7, y + 25, new BioUnit(x + 7, y + 25, this));
                this.insert(x + 3, y + 35, new BioUnit(x + 3, y + 35, this));
                this.insert(x + 4, y + 35, new BioUnit(x + 4, y + 35, this));
                this.insert(x + 3, y + 36, new BioUnit(x + 3, y + 36, this));
                this.insert(x + 4, y + 36, new BioUnit(x + 4, y + 36, this));
            }
        }
    }
}