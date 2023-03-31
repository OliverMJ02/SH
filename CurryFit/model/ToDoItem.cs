using CurryFit.model.blocks;
using CurryFit.view;
using SQLite;
using SQLiteNetExtensions.Attributes;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.Design;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading;
using System.Windows.Input;
using Xamarin.Forms;

namespace CurryFit.model
{
    public class ToDoItem : ObservableObject
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }

        private string text;
        public string Text
        {
            get { return text; }
            set
            {

                text = value;
                hasText = true;
                HasText = true;
                OnPropertyChanged(nameof(Text));
            }
        }

        private bool isEditing;
        public bool IsEditing
        {
            get { return isEditing; }
            set
            {

                isEditing = value;
                OnPropertyChanged(nameof(IsEditing));
            }
        }

        private bool hasText;
        public bool HasText
        {
            get { return hasText; }
            set
            {

                hasText = value;
                OnPropertyChanged(nameof(HasText));
            }
        }

        private bool checkMarked;
        public bool CheckMarked
        {
            get { return checkMarked; }
            set
            {

                checkMarked = value;
                OnPropertyChanged(nameof(CheckMarked));
            }
        }

        [ForeignKey(typeof(ToDoList))]
        public int ToDoListId { get; set; }

        public ICommand EditCmd { get; private set; }
        public ICommand SaveCmd { get; private set; }
        public ICommand UpdateCheckMarkCmd { get; private set; }

        public ToDoItem() 
        {
            Id = this.Id;
            ToDoListId= this.ToDoListId;
            Text = this.Text;
            IsEditing = this.IsEditing;
            HasText = this.HasText;
            CheckMarked = this.CheckMarked;

            EditCmd = new Command(() => { IsEditing = true; });
            SaveCmd = new Command(() => { IsEditing = false; App.Database.UpdateToDoItem(this); });
            UpdateCheckMarkCmd = new Command(() => { if (CheckMarked) { CheckMarked = false; } else { CheckMarked = true; } App.Database.UpdateToDoItem(this); });
        }
    }
}
