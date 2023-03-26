using CurryFit.model.blocks;
using CurryFit.model.Sets;
using SQLite;
using SQLiteNetExtensions.Attributes;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;

namespace CurryFit.model.blocks
{
    public class ToDoList : ObservableObject
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }

        private bool toDoListVisibility;
        public bool ToDoListVisibility
        {
            get { return toDoListVisibility; }
            set
            {

                toDoListVisibility = value;
                OnPropertyChanged(nameof(ToDoListVisibility));
            }
        }

        private string blockTitle;
        public string BlockTitle
        {
            get { return blockTitle; }
            set
            {

                blockTitle = value;
                OnPropertyChanged(nameof(BlockTitle));
            }
        }

        [OneToMany]
        public List<ToDoItem> ToDoItems { get; set; } // List of ToDoItems connected to the ToDoList

        public string Title { get; set; }
        public int Order { get; set; }

        [ForeignKey(typeof(LogDay))]
        public int LogDayId { get; set; }


        public ToDoList()
        {
            Id = this.Id;
            LogDayId= this.LogDayId;
            ToDoListVisibility = this.ToDoListVisibility;
            Title= this.Title;
            BlockTitle= this.BlockTitle;
            Order = this.Order;
            ToDoItems = this.ToDoItems;

        }
    }
}
