using CurryFit.model.blocks;
using CurryFit.model.Sets;
using SQLite;
using SQLiteNetExtensions.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CurryFit.model
{
    public class LogDay
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
        public int Day { get; set; }

        public int Counter { get; set; }  //Counter for order

        //[OneToMany]
        //public List<TextBlock> TextBlocks { get; set; }

        [OneToMany]
        public List<NormalSetBlock> NormalSetBlocks { get; set; }

        [OneToMany]
        public List<DropSetBlock> DropSetBlocks { get; set; }

        //[OneToMany]
        //public List<SuperSetBlock> SuperSetBlocks { get; set; }

        //[OneToMany]
        //public List<EnduranceSetBlock> EnduranceSetBlocks { get; set; }


        public List<object> GetAllBlocks()
        {
            LogDay d = App.Database.GetLogDayWithChildren(this.Id);
            List<object> blocks = new List<object>();
    

            /*
            foreach(TextBlock tb in this.TextBlocks)
            {
                blocks.Add(tb);
            }
            */
            
            
                foreach (NormalSetBlock nb in d.NormalSetBlocks)
                {
                    blocks.Add(App.Database.GetNormalBlockWithChildren(nb.Id));
                    //blocks.Add(nb);
                }
            
            
            /*
            try
            {
                foreach (DropSetBlock db in d.DropSetBlocks)
                {
                    blocks.Add(App.Database.GetDropBlockWithChildren(db.Id));
                    //blocks.Add(db);
                }
            }
            catch { }
            /*
            foreach (SuperSetBlock sb in this.SuperSetBlocks)
            {
                blocks.Add(sb);
            }
            foreach (EnduranceSetBlock eb in this.EnduranceSetBlocks)
            {
                blocks.Add(eb);
            }
            */
            return blocks;
        }
    }
}
