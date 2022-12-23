using CurryFit.model.Sets;
using SQLite;
using SQLiteNetExtensions.Attributes;
using System;
using System.Collections.Generic;
using System.Text;

namespace CurryFit.model.blocks
{
    public class NormalSetBlock
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
        public List<NormalSet> NormalSets { get; set; }

        [ForeignKey(typeof(LogDay))]
        public int LogDayId { get; set; }

        public NormalSetBlock(int Counter)
        {
            IsTextBlock = false;
            IsNormalSet = true;
            IsDropSet = false;
            IsSuperSet = false;
            IsEnduranceSet = false;
            Order = Counter;
            Title = "NORMAL SET";
            Fade1 = "#A6A0A6";
            Fade2 = "#A6A0A6";
        }

        public NormalSetBlock()
        {
            IsTextBlock = false;
            IsNormalSet = true;
            IsDropSet = false;
            IsSuperSet = false;
            IsEnduranceSet = false;
            Order = this.Order;
            Title = "NORMAL SET";
            Fade1 = this.Fade1;
            Fade2 = this.Fade2;
        }

        public NormalSetBlock CloseAllSets()
        {
            foreach (NormalSet NS in this.NormalSets)
            {
                NS.IsVisible = false;
                NS.IsVisibleSource = "pointer_down_gray.png";
                App.Database.UpdateNormalSetWithChildren(NS);
            }
            return this;
        }

        public void UpdateNormalSetTitels(int i)
        {
            foreach (NormalSet ns in this.NormalSets)
            {
                if (int.Parse((ns.Title).Remove(0, 4)) > i)
                {
                    ns.Title = "SET " + (int.Parse((ns.Title).Remove(0, 4)) - 1).ToString();
                    App.Database.UpdateNormalSetWithChildren(ns);
                }
            }
        }

    }
}
