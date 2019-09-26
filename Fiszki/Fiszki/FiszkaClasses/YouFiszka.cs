using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel;

namespace Fiszki.FiszkaClasses
{
    public class YouFiszka : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        void NotifyPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public int index { get; set; }
        public string polski { get; set; }
        public string angielski;

        public string Angielski
        {
            get { return this.angielski; }
            set
            {
                this.angielski = value;
                NotifyPropertyChanged(nameof(Angielski));
            }
        }
        private string correctAnglish;

        public YouFiszka(int index, string polski, string angielski, string correctAnglishWord)
        {
            this.index = index;
            this.polski = polski;
            this.angielski = angielski;
            this.correctAnglish = correctAnglishWord;

            if (angielski.ToUpper() != correctAnglish.ToUpper())
                Angielski = angielski + " / " + correctAnglish;
        }

        public string Color
        {
            get
            {
                if (angielski.ToUpper() == correctAnglish.ToUpper())
                    return "Green";
                else
                {
                    
                    return "Red";
                }
            }
        }

    }
}
