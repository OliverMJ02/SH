using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using SQLite;
using System.Linq;
using System.Diagnostics;
using Xamarin.Forms;
using SQLiteNetExtensions.Attributes;
using SQLiteNetExtensions.Exceptions;
using SQLiteNetExtensions.Extensions.TextBlob;
using System.Reflection;
using SQLiteNetExtensions.Extensions;
using CurryFit.model;

namespace CurryFit
{
    public class Database
    {
        private readonly SQLiteConnection _database;

        public Database(string dbPath)
        {
            _database = new SQLiteConnection(dbPath);
            _database.CreateTable<TrainingProgram>();
            _database.CreateTable<Exercise>();
            _database.CreateTable<TrainingProgramsExercises>();
        }

        //Methods for TrainingPrograms 
        public List<TrainingProgram> GetTrainingPrograms()
        {
            return _database.Table<TrainingProgram>().ToList();
        }

        public int SaveTrainingProgram(TrainingProgram trainingProgram)
        {
            return _database.Insert(trainingProgram);
        }

        public int DeleteAllTrainingPrograms()
        {
            return _database.DeleteAll<TrainingProgram>();
        }

        public TrainingProgram GetSingleTrainingProgram(object id)
        {
            return _database.GetWithChildren<TrainingProgram>(id);
        }

        public int UpdateTrainingProgram(TrainingProgram trainingProgram)
        {
            return _database.Update(trainingProgram);
        }

        public void UpdateTrainingProgramWithChildren(TrainingProgram trainingProgram)
        {
            _database.UpdateWithChildren(trainingProgram);
        }

        public int DeleteSingleTrainingProgram(object id)
        {
            return _database.Delete<TrainingProgram>(id);
        }



        //Methods for Exercises 

        public List<Exercise> GetExercises()
        {
            return _database.Table<Exercise>().ToList();
        }

        public int SaveExercises(Exercise excercise)
        {
            return _database.Insert(excercise);
        }

        public int DeleteAllExercises()
        {
            return _database.DeleteAll<Exercise>();
        }

        public void UpdateExercise(Exercise exercise)
        {
            _database.Update(exercise);
        }

        public Exercise GetSingleExercise(object id)
        {
            return _database.GetWithChildren<Exercise>(id);
        }

        public int DeleteSingleExercise(object id)
        {
            return _database.Delete<Exercise>(id);
        }
    }
}
