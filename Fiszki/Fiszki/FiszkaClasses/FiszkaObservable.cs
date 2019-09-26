using System;
using System.Collections.Generic;
using System.Text;
using System.Collections.ObjectModel;

namespace Fiszki.FiszkaClasses
{
    public class FiszkaObservable
    {
        public ObservableCollection<YouFiszka> fiszkaObservable = new ObservableCollection<YouFiszka>();
        public string title;

        public FiszkaObservable(string title)
        {
            this.title = title;
        }

        public void Add(int index, string pl,string enteredAng, string ang)
        {
            fiszkaObservable.Add(new YouFiszka(index, pl, enteredAng ,ang));
        }

    }
}
