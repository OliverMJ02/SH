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

            _database.CreateTable<SuperSetBlock>();
            _database.CreateTable<SuperSet>();

            _database.CreateTable<TextBlock>();

            _database.CreateTable<ToDoList>();
            _database.CreateTable<ToDoItem>();

            _database.CreateTable<Settings>();
            _database.CreateTable<model.Timer>();

            
            try {
                
                if (_database.Table<Settings>().ToList().Count < 1)     //Create new settings, but only do it once
                {
                    Settings settings = new Settings() { PresetTimers = new List<model.Timer>()};
                    SaveSettings(settings);
                    model.Timer t = new model.Timer() { IsPreset = false, PresetMenuVisible = true, PresetOrder = 10 };
                    SaveTimer(t);
                    settings.PresetTimers.Add(t);
                    UpdateSettings(settings);
                }
            }
            catch { }

            
            
        }

        //Methods for settings
        public Settings GetSettings()
        {
            return _database.GetWithChildren<Settings>(_database.Table<Settings>().ToList()[0].Id);
        }
        public int SaveSettings(Settings settings)
        {
            return _database.Insert(settings);
        }
        public void UpdateSettings(Settings settings)
        {
            _database.UpdateWithChildren(settings);
        }

        //Methods for Timer
        public List<model.Timer> GetTimers()
        {
            return _database.Table<model.Timer>().ToList();
        }

        public model.Timer GetTimer(object id)
        {
            return _database.Get<model.Timer>(id);
        }

        public int SaveTimer(model.Timer timer)
        {
            return _database.Insert(timer);
        }
        public void UpdateTimer(model.Timer timer)
        {
            _database.UpdateWithChildren(timer);
        }

        public int DeleteTimer(object id)
        {
            return _database.Delete<model.Timer>(id);
        }

        //Methods for TextBlock

        public TextBlock GetTextBlock(object id)
        {
            return _database.Get<TextBlock>(id);
        }

        public int SaveTextBlock(TextBlock textBlock)
        {
            return _database.Insert(textBlock);
        }
        public void UpdateTextBlock(TextBlock textBlock)
        {
            _database.UpdateWithChildren(textBlock);
        }

        public int DeleteTextBlock(object id)
        {
            return _database.Delete<TextBlock>(id);
        }

        //Methods for ToDoList

        public ToDoList GetToDoList(object id)
        {
            return _database.GetWithChildren<ToDoList>(id);
        }

        public int SaveToDoList(ToDoList toDoList)
        {
            return _database.Insert(toDoList);
        }
        public void UpdateToDoList(ToDoList toDoList)
        {
            _database.UpdateWithChildren(toDoList);
        }

        public int DeleteToDoList(object id)
        {
            return _database.Delete<ToDoList>(id);
        }

        //Methods for ToDoItem

        public ToDoItem GetToDoItem(object id)
        {
            return _database.Get<ToDoItem>(id);
        }

        public int SaveToDoItem(ToDoItem toDoItem)
        {
            return _database.Insert(toDoItem);
        }
        public void UpdateToDoItem(ToDoItem toDoItem)
        {
            _database.UpdateWithChildren(toDoItem);
        }

        public int DeleteToDoItem(object id)
        {
            return _database.Delete<ToDoItem>(id);
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


        //Methods for SuperSetBlocks
        public void UpdateSuperBlockWithChildren(SuperSetBlock block)
        {
            _database.UpdateWithChildren(block);
        }

        public int DeleteSingleSuperBlock(object id)
        {
            return _database.Delete<SuperSetBlock>(id);
        }

        public SuperSetBlock GetSuperBlockWithChildren(object id)
        {
            return _database.GetWithChildren<SuperSetBlock>(id);
        }

        public int SaveSuperBlock(SuperSetBlock block)
        {
            return _database.Insert(block);
        }

        public void DeleteSuperBlock(SuperSetBlock block)
        {
            _database.Delete(block);
        }

        //Methods for SuperSets
        public void UpdateSuperSetWithChildren(SuperSet set)
        {
            _database.UpdateWithChildren(set);
        }

        public int DeleteSingleSuperSet(object id)
        {
            return _database.Delete<DropSet>(id);
        }

        public SuperSet GetSuperSetWithChildren(object id)
        {
            return _database.GetWithChildren<SuperSet>(id);
        }

        public int SaveSuperSet(SuperSet set)
        {
            return _database.Insert(set);
        }
        public void DeleteSuperSet(SuperSet set)
        {
            _database.Delete(set);
        }


    }
}
