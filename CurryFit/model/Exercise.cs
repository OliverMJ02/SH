using System;
using System.Collections.Generic;
using System.Text;
using Firebase.Database;
using Firebase.Database.Query;
using SQLite;
using SQLiteNetExtensions.Attributes;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CurryFit.model
{
    public class Exercise
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
        public string Key { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Creator { get; set; }
        public string MainMuscle { get; set; }
        public string MainEquipment { get; set; }
        public string FavorisedSource { get; set; }


        [ManyToMany(typeof(TrainingProgramsExercises))]
        public List<TrainingProgram> TrainingPrograms { get; set; }

        public void UpdateFavorised()
        {
            if (this.FavorisedSource.Equals("star_empty.png"))
            {
                this.FavorisedSource = "star_filled.png";
            }
            else
            {
                this.FavorisedSource = "star_empty.png";
            }
            App.Database.UpdateExercise(this);
        }

        public void HandleChange(Exercise ex1, Exercise ex2)
        {
            ex1.Name = ex2.Name;
            ex1.Description = ex2.Description;
            ex1.MainEquipment = ex2.MainEquipment;
            ex1.MainMuscle = ex2.MainMuscle;
            ex1.Creator = ex2.Creator;
            App.Database.UpdateExercise(ex1);
            App.Database.UpdateExerciseWithChildren(ex1);
        }

        public bool CheckExistence(List<Exercise> exs)
        {
            bool doesExist = false;
            foreach(Exercise exercise in exs)
            {
                try
                {
                    if (exercise.Key.Equals(this.Key))
                    {
                        doesExist = true;
                        HandleChange(exercise, this);
                        break;
                    }
                }
                catch { }
            }
            return doesExist;
        }


    }
}
