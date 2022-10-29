using System;
using System.Collections.Generic;
using System.Text;
using SQLite;
using SQLiteNetExtensions.Attributes;

namespace CurryFit.model
{
    public class Exercise
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }

        public string Name { get; set; }
        public string Description { get; set; }
        public string Creator { get; set; }
        public string MainMuscle { get; set; }
        public string MainEquipment { get; set; }
        public bool isFavorised { get; set; }
        public string FavorisedSource { get; set; }


        [ManyToMany(typeof(TrainingProgramsExercises))]
        public List<TrainingProgram> TrainingPrograms { get; set; }

    }
}
