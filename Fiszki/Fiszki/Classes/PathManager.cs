using System;
using System.Collections.Generic;
using System.Text;
using System.IO;

namespace Fiszki.Classes
{
    static class PathManager
    {
        public static string TitleListPath()
        {
            var documentsPath = Environment.GetFolderPath(Environment.SpecialFolder.Personal);
            documentsPath = Path.Combine(documentsPath, "Fiszki");
            string filePath = Path.Combine(documentsPath, TitleFileName());

            return filePath;
        }

        public static string EnviromentPath()
        {
            var documentsPath = Environment.GetFolderPath(Environment.SpecialFolder.Personal);
            documentsPath = Path.Combine(documentsPath, "Fiszki");

            return documentsPath;
        }

        public static string TitleFileName()
        {
            return "!ListOfTitle.txt";
        }

        public static string EmbeddTitlePath()
        {
            return "Fiszki.Fiszki.!ListOfTitle.txt";
        }

        public static string EmbeddPath()
        {
            return "Fiszki.Fiszki.";
        }

        public static string Fiszki()
        {
            return "Fiszki";
        }

        public static string ToImprove()
        {
            return "ToImprove";
        }
        public static string StatsName()
        {
            return "!Stats.txt";
        }

    }
}
