using CurryFit.model.blocks;
using CurryFit.model.Sets;
using SQLite;
using SQLiteNetExtensions.Attributes;
using System;
using System.Collections.Generic;
using System.Text;
namespace CurryFit.model.blocks
{
    public class TextBlock
    {
        public bool IsTextBlock { get; set; }
        public bool IsNormalSet { get; set; }
        public bool IsDropSet { get; set; }
        public bool IsSuperSet { get; set; }
        public bool IsEnduranceSet { get; set; }
        public string Text { get; set; }
        public int Order { get; set; }
        public string Title { get; set; }

        [ForeignKey(typeof(LogDay))]
        public int LogDayId { get; set; }
    }
}
