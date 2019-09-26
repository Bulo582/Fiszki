using System;
using System.Collections.Generic;
using System.Text;

namespace Fiszki.PageClasses
{
    class YouStats
    {
        string title;
        int count_done = 0;

        public YouStats(string title, int count_done)
        {
            this.title = title;
            this.count_done = count_done;
        }

        public string Title
        {
            get { return this.title; }
        }
        public int Count_Done
        {
            get { return this.count_done; }
            set { this.count_done = value; }
        }

        public string DoneString
        {
            get { return "Skończenie w 100% = " + Count_Done.ToString(); }
        }
    }
}
