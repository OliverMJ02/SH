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
using CurryFit.model.blocks;
using CurryFit.model.Sets;

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

            _database.CreateTable<LogDay>();

            _database.CreateTable<NormalSetBlock>();
            _database.CreateTable<NormalSet>();

            _database.CreateTable<DropSetBlock>();
            _database.CreateTable<DropSet>();

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

        public void UpdateExerciseWithChildren(Exercise exercise)
        {
            _database.UpdateWithChildren(exercise);
        }
        public List<Exercise> GetExercises()
        {
            return _database.Table<Exercise>().ToList();
        }

        public Exercise GetExerciseWithChildren(object id)
        {
            return _database.GetWithChildren<Exercise>(id);
        }

        public int SaveExercise(Exercise excercise)
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

        //Methods for LogDay
        public void UpdateLogDayWithChildren(LogDay log)
        {
            _database.UpdateWithChildren(log);
        }

        public void UpdateLogDay(LogDay log)
        {
            _database.Update(log);
        }

        public int SaveLogDay(LogDay log)
        {
            return _database.Insert(log);
        }

        public List<LogDay> GetLogDays()
        {
            return _database.Table<LogDay>().ToList();
        }

        public LogDay GetLogDayWithChildren(object id)
        {
            return _database.GetWithChildren<LogDay>(id);
        }
        
        //Methods for NormalSetBlocks

        //test
        public List<NormalSetBlock> GetAllNormalSetBlocksWithChildren()
        {
            return _database.Table<NormalSetBlock>().ToList();
        }
        //test
        public void UpdateNormalBlockWithChildren(NormalSetBlock block)
        {
            _database.UpdateWithChildren(block);
        }

        public int DeleteSingleNormalBlock(object id)
        {
            return _database.Delete<NormalSetBlock>(id);
        }

        public NormalSetBlock GetNormalBlockWithChildren(object id)
        {
            return _database.GetWithChildren<NormalSetBlock>(id);
        }

        public void SaveNormalBlock(NormalSetBlock block)
        {
            _database.InsertWithChildren(block);
        }

        public void DeleteNormalBlock(NormalSetBlock block)
        {
            _database.Delete(block);
        }

        //Methods for NormalSets
        public void UpdateNormalSetWithChildren(NormalSet set)
        {
            _database.UpdateWithChildren(set);
        }

        public int DeleteSingleNormalSet(object id)
        {
            return _database.Delete<NormalSet>(id);
        }

        public NormalSet GetNormalSetWithChildren(object id)
        {
            return _database.GetWithChildren<NormalSet>(id);
        }

        public void SaveNormalSet(NormalSet set)
        {
            _database.InsertWithChildren(set);
        }
        public void DeleteNormalSet(NormalSet set)
        {
            _database.Delete(set);
        }



        //Methods for DropSetBlocks
        public void UpdateDropBlockWithChildren(DropSetBlock block)
        {
            _database.UpdateWithChildren(block);
        }

        public int DeleteSingleDropBlock(object id)
        {
            return _database.Delete<DropSetBlock>(id);
        }

        public DropSetBlock GetDropBlockWithChildren(object id)
        {
            return _database.GetWithChildren<DropSetBlock>(id);
        }

        public int SaveDropBlock(DropSetBlock block)
        {
            return _database.Insert(block);
        }

        public void DeleteDropBlock(DropSetBlock block)
        {
            _database.Delete(block);
        }

        //Methods for DropSets
        public void UpdateDropSetWithChildren(DropSet set)
        {
            _database.UpdateWithChildren(set);
        }

        public int DeleteSingleDropSet(object id)
        {
            return _database.Delete<DropSet>(id);
        }

        public DropSet GetDropSetWithChildren(object id)
        {
            return _database.GetWithChildren<DropSet>(id);
        }

        public int SaveDropSet(DropSet set)
        {
            return _database.Insert(set);
        }
        public void DeleteDropSet(DropSet set)
        {
            _database.Delete(set);
        }


    }
}
