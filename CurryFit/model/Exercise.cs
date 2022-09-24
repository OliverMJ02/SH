﻿using System;
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
        public string Difficulty { get; set; }
        public string MuscleGroup { get; set; }
        public bool isFavorised { get; set; }
        public string VideoLink { get; set; }
        public string Creator { get; set; }

        public string Location { get; set; }

        public string btnAddOrRemove { get; set; }

        public string btnColor { get; set; }

        public string FavorisedSource { get; set; }


        [ManyToMany(typeof(TrainingProgramsExercises))]
        public List<TrainingProgram> TrainingPrograms { get; set; }
    }
}
