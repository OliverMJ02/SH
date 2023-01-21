using CurryFit.model.blocks;
using SQLite;
using SQLiteNetExtensions.Attributes;
using System;
using System.Collections.Generic;
using System.Text;

namespace CurryFit.model.Sets
{
    public class DropSet
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
        public double StartWeight { get; set; }
        public double EndWeight { get; set; }
        public int Reps { get; set; }
        public bool IsVisible { get; set; }
        public string IsVisibleSource { get; set; }
        public string Title { get; set; }

        [ForeignKey(typeof(DropSetBlock))]
        public int DropSetBlockId { get; set; }


        public DropSet()
        {
            StartWeight = 0;
            EndWeight = 0;
            Reps = 0;
            IsVisible = true;
            IsVisibleSource = "pointer_up_gray.png";
            Title = "SET 1";
        }

        public DropSet(int c)
        {
            StartWeight = 0;
            EndWeight = 0;
            Reps = 0;
            IsVisible = true;
            IsVisibleSource = "pointer_up_gray.png";
            Title = "SET " + c.ToString();
        }

        public DropSet UpdateSetVisibility()
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
