using System;
using System.Collections.Generic;
using System.Text;
using System.Reflection;
using System.IO;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace Fiszki.Classes
{
    static class TitleManager
    {
        public static void LoadEmbeddTitle()
        {
            EmbeddFiles titleFile = new EmbeddFiles("Fiszki.Fiszki.!ListOfTitle.txt");
            
            AddTitle(RemoveEmbeddTitle(titleFile.Text,TitleList(PathManager.ToImprove())));
        }


        public static List<string> TitleList(string folderName)
        {
            List<string> titleList = new List<string>();

            if (FileManager.FileExist(PathManager.TitleFileName(), folderName).Result)
            {
                Task<string> file = FileManager.Read(PathManager.TitleFileName(), folderName);
                string[] titleArray = file.Result.Split(new string[] { "#", "\n" }, options: StringSplitOptions.RemoveEmptyEntries);

                foreach (string item in titleArray)
                {
                    titleList.Add(item);
                }
            }
            else
                System.Diagnostics.Debug.WriteLine("Brak pliku " + PathManager.TitleFileName() + "w folderze " + folderName);

            return titleList;
        }


        public static void AddTitle(string title)
        {
            DependencyService.Get<ISaveAndLoad>().SaveTextAsync(PathManager.TitleFileName(), "Fiszki", title, false);
        }

        public static bool CompareTitleIntoFile(string stringToEqual, string folderName)
        {
            foreach (string item in TitleList(folderName))
            {
                if (item == stringToEqual)
                {
                    return true;
                }
            }
            return false;
        }

        public static string RemoveEmbeddTitle(string titleFormat, List<string> removableTitles)
        {
            string format = titleFormat;

            foreach (string item in removableTitles)
            {
                try
                {
                    format = format.Remove(titleFormat.IndexOf(item), item.Length + 1);
                }
                catch(ArgumentOutOfRangeException ex)
                {
                    int debug = titleFormat.IndexOf(item);
                    format = format.Remove(format.IndexOf(item), item.Length + 1);
                    System.Diagnostics.Debug.WriteLine(ex.Message);
                }
            }

            return format;
            
        }

        public static void RemoveCustomTitle(string customeTitle, string folderName)
        {
            
            string format = FileManager.Read(PathManager.TitleFileName(), folderName).Result;

            try
            {
                format = format.Remove(format.IndexOf(customeTitle), customeTitle.Length + 1);
            }
            catch(ArgumentOutOfRangeException ex)
            {
                format = format.Remove(format.IndexOf(customeTitle), customeTitle.Length);
                System.Diagnostics.Debug.WriteLine(ex.Message);

            }

            FileManager.Write(PathManager.TitleFileName(), format, folderName, false);

            LoadEmbeddTitle();
        }

        private static bool ExistTitle(string folderName, string title)
        {
            List<string> list = TitleList(PathManager.ToImprove());
            foreach (string item in list)
            {
                if (item == title)
                    return true;
            }

            return false;
        }

        

        public static void AddDontExistTitle(string folderName, string title)
        {
            if (!(ExistTitle(folderName, title)))
                FileManager.Write(PathManager.TitleFileName(), title + "#", folderName, true);
            else if (FileManager.FileIsEmpty(PathManager.TitleFileName(), folderName))
                FileManager.Write(PathManager.TitleFileName(), title + "#", folderName, false);
        }

    }
}
