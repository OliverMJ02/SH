using CurryFit.model.Sets;
using SQLite;
using SQLiteNetExtensions.Attributes;
using System;
using System.Collections.Generic;
using System.Text;

namespace CurryFit.model.blocks
{
    public class DropSetBlock
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
        public bool IsTextBlock { get; set; }
        public bool IsNormalSet { get; set; }
        public bool IsDropSet { get; set; }
        public bool IsSuperSet { get; set; }
        public bool IsEnduranceSet { get; set; }
        public int Order { get; set; }
        public string Title { get; set; }
        public string Fade1 { get; set; }
        public string Fade2 { get; set; }

        [OneToMany]
        public List<DropSet> DropSets { get; set; }

        [ForeignKey(typeof(LogDay))]
        public int LogDayId { get; set; }

        public DropSetBlock(int Counter)
        {
            IsTextBlock = false;
                IsNormalSet = false;
                IsDropSet = true;
                IsSuperSet = false;
                IsEnduranceSet = false;
                Order = Counter;
                Title = "DROP SET";
                Fade1 = "#A6A0A6";
                Fade2 = "#A6A0A6";
        }

        public DropSetBlock()
        {
            IsTextBlock = false;
            IsNormalSet = false;
            IsDropSet = true;
            IsSuperSet = false;
            IsEnduranceSet = false;
            Order = this.Order;
            Title = "DROP SET";
            Fade1 = this.Fade1;
            Fade2 = this.Fade2;
        }

        public DropSetBlock CloseAllSets()
        {
            foreach (DropSet DS in this.DropSets)
            {
                DS.IsVisible = false;
                DS.IsVisibleSource = "pointer_down_gray.png";
                App.Database.UpdateDropSetWithChildren(DS);
            }
            return this;
        }

        public void UpdateDropSetTitels(int i)
        {
            foreach (DropSet ds in this.DropSets)
            {
                if (int.Parse((ds.Title).Remove(0, 4)) > i)
                {
                    ds.Title = "SET " + (int.Parse((ds.Title).Remove(0, 4)) - 1).ToString();
                    App.Database.UpdateDropSetWithChildren(ds);
                }
            }
        }
    }
}
