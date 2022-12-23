using CurryFit.model.blocks;
using SQLite;
using SQLiteNetExtensions.Attributes;
using System;
using System.Collections.Generic;
using System.Text;

namespace CurryFit.model.Sets
{
    public class NormalSet
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
        public double Weight { get; set; }
        public int Reps { get; set; }
        public bool IsVisible { get; set; }
        public string IsVisibleSource { get; set; }
        public string Title { get; set; }

        [ForeignKey(typeof(NormalSetBlock))]
        public int NormalSetBlockId { get; set; }


        public NormalSet()
        {
            Weight = 0;
            Reps = 0;
            IsVisible = true;
            IsVisibleSource = "pointer_up_gray.png";
            Title = "SET 1";
        }

        public NormalSet(int c)
        {
            Weight = 0;
            Reps = 0;
            IsVisible = true;
            IsVisibleSource = "pointer_up_gray.png";
            Title = "SET " + c.ToString();
        }


        //Upadates the visability off set
        public NormalSet UpdateSetVisibility()
        {
            if (this.IsVisible)
            {
                this.IsVisible = false;
                this.IsVisibleSource = "pointer_down_gray.png";
            }
            else
            {
                this.IsVisible = true;
                this.IsVisibleSource = "pointer_up_gray.png";
            }
            return this;
        }
    }
}
