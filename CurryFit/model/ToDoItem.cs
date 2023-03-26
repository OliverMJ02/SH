using CurryFit.model.blocks;
using CurryFit.view;
using SQLite;
using SQLiteNetExtensions.Attributes;
using System;
using System.Collections.Generic;
using System.ComponentModel;
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
                OnPropertyChanged(nameof(Text));
            }
        }

        [ForeignKey(typeof(ToDoList))]
        public int ToDoListId { get; set; }

        public ToDoItem() 
        {
            Id = this.Id;
            ToDoListId= this.ToDoListId;
            Text = this.Text;
        }
    }
}
