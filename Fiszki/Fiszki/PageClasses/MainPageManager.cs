using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel;

namespace Fiszki.PageClasses
{
    class MainPageManager : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        void NotifyPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public bool InCorrectFlag { get; set; }

        public MainPageManager(bool incorect)
        {
            InCorrectFlag = incorect;
        }

        public void Refresh()
        {
            if(InCorrectFlag)
            {
                TitlePageDependent = "Fiszki do poprawienia";
                TbiModeTitleDependent ="Niezaczęte";
            }
            else
            {
                TitlePageDependent = "Fiszki do nauki angielskiego";
                TbiModeTitleDependent = "Do poprawy";
            }
        }

        string titlePageDependent;

        public string TitlePageDependent
        {
            get
            {
                return this.titlePageDependent;
            }
            set
            {
                titlePageDependent = value;
                NotifyPropertyChanged(nameof(TitlePageDependent));
            }
        }

        string tbiModeTitleDependent;

        public string TbiModeTitleDependent
        {
            get
            {
                return this.tbiModeTitleDependent;
            }
            set
            {
                tbiModeTitleDependent = value;
                NotifyPropertyChanged(nameof(TbiModeTitleDependent));
            }
        }
    }
}
