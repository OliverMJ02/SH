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
    public class TextBlock : ObservableObject
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }

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

        private bool textBlockVisibility;
        public bool TextBlockVisibility
        {
            get { return textBlockVisibility; }
            set
            {

                textBlockVisibility = value;
                OnPropertyChanged(nameof(TextBlockVisibility));
            }
        }
        private string text;
        public string Text
        {
            get { return text; }
            set
            {

                text = value;
                hasText= true;
                HasText= true;
                OnPropertyChanged(nameof(Text));
            }
        }
        private string textTitle;
        public string TextTitle
        {
            get { return textTitle; }
            set
            {

                textTitle = value;
                OnPropertyChanged(nameof(TextTitle));
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
        public int Order { get; set; }
        public string Title { get; set; }

        [ForeignKey(typeof(LogDay))]
        public int LogDayId { get; set; }

        public ICommand SaveTextBlockCmd { get; private set; }

        public ICommand EditTextBlockCmd { get; private set; }

        public TextBlock()
        {
            Id = this.Id;
            IsEditing = this.IsEditing;
            TextBlockVisibility= this.TextBlockVisibility;
            Text= this.Text;
            Order = this.Order;
            Title = this.Title;
            LogDayId= this.LogDayId;
            HasText = this.HasText;

            SaveTextBlockCmd = new Command(() => { IsEditing = false; App.Database.UpdateTextBlock(this); });
            EditTextBlockCmd = new Command(() => { IsEditing = true; });
        }
    }
}
