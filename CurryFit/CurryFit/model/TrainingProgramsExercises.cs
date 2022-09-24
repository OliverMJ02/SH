using System;
using System.Collections.Generic;
using System.Text;
using SQLite;
using SQLiteNetExtensions.Attributes;

namespace CurryFit.model
{
    public class TrainingProgramsExercises
    {
        [ForeignKey(typeof(TrainingProgram))]
        public int TrainingProgramID { get; set; }

        [ForeignKey(typeof(Exercise))]
        public int ExerciseID { get; set; }
    }
}
