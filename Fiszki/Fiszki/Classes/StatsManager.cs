using System;
using System.Collections.Generic;
using System.Text;
using Fiszki.PageClasses;

namespace Fiszki.Classes
{
    class StatsManager
    {
        public List<YouStats> Stats;

        public StatsManager ()
        {
            Stats = ListStats();
        }

        string TakeStatsFormat()
        {
            string statsFormat = FileManager.Read(PathManager.StatsName(), PathManager.ToImprove()).Result;

            return statsFormat;
        }

        public List<YouStats> ListStats()
        {
            List<YouStats> list = new List<YouStats>();
            string statsFormat;

            try
            {
                statsFormat = TakeStatsFormat();
            }
            catch(Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex.Message);
                return list;
            }

            string[] context;

            context = statsFormat.Split(new string[] { "\n" }, options: StringSplitOptions.RemoveEmptyEntries);
            context = statsFormat.Split(new string[] { "#", "\r", ".", "\\", "\n" }, options: StringSplitOptions.RemoveEmptyEntries);

            for (int i = 0; i < context.Length; i = i + 2)
            {
                int.TryParse(context[i + 1], out int count);
                list.Add(new YouStats(context[i], count));

                if (context.Length <= i + 1)
                    break;
            }

            return list;
        }

        public void Update(string title)
        {
            foreach (YouStats item in Stats)
            {
                if (item.Title == title)
                    item.Count_Done++;
            }

            SaveStats();
        }

        private void SaveStats()
        {
            string statsFormat = String.Empty;
            foreach (YouStats item in Stats)
            {
                statsFormat = StatsFormat(item.Title, item.Count_Done);
            }

            FileManager.Write(PathManager.StatsName(), statsFormat, PathManager.ToImprove(), false);
            Stats = ListStats();
        }

        public bool CheckExistTitle(string title)
        {
            foreach (YouStats item in Stats)
            {
                if (item.Title == title)
                    return true;
            }

            return false;
        }

        public void Add(string title)
        {

            if(CheckExistTitle(title))
            {
                foreach (YouStats item in Stats)
                {
                    if (item.Title == title)
                        item.Count_Done++;
                }
            }
            else
            {
                Stats.Add(new YouStats(title, 1));
            }

            SaveStats();
        }

        private string StatsFormat(string title, int count)
        {
            return title + "#" + count.ToString() + "."; 
        }

        

         
    }
}
