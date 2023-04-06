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
    public class Settings
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }

        [OneToMany]
        public List<Timer> PresetTimers { get; set; }
    }
}
