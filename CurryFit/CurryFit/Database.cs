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

        public List<TrainingProgram> GetTrainingProgramsAsync()
        {
            return _database.Table<TrainingProgram>().ToList();
        }

        public int SaveTrainingProgramAsync(TrainingProgram trainingProgram)
        {
            return _database.Insert(trainingProgram);
        }

        public int DeleteAllTrainingProgramsAsync()
        {
            return _database.DeleteAll<TrainingProgram>();
        }

        public TrainingProgram GetSingleTrainingProgramAsync(object id)
        {
            return _database.GetWithChildren<TrainingProgram>(id);
        }

        public int UpdateTrainingProgramAsync(TrainingProgram trainingProgram)
        {
            return _database.Update(trainingProgram);
        }

        public void UpdateTrainingProgramWithChildren(TrainingProgram trainingProgram)
        {
            _database.UpdateWithChildren(trainingProgram);
        }

        public int DeleteSingleTrainingProgramAsync(object id)
        {
            return _database.Delete<TrainingProgram>(id);
        }



        // Methods for Exercises

        public List<Exercise> GetExcercisesAsync()
        {
            return _database.Table<Exercise>().ToList();
        }

        public int SaveExcercisesAsync(Exercise excercise)
        {
            return _database.Insert(excercise);
        }

        public int DeleteAllExcercisesAsync()
        {
            return _database.DeleteAll<Exercise>();
        }

        public void UpdateExcerciseAsync(Exercise excercise)
        {
            _database.Update(excercise);
        }

        public Exercise GetSingleExcerciseAsync(object id)
        {
            return _database.GetWithChildren<Exercise>(id);
        }

        public int DeleteSingleExcerciseAsync(object id)
        {
            return _database.Delete<Exercise>(id);
        }
    }
}
