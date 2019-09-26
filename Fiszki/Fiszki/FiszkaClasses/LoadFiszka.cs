using System;
using System.Collections.Generic;
using System.Text;
using Fiszki.Classes;
using System.IO;
using System.Threading.Tasks;

namespace Fiszki.FiszkaClasses
{
   static class LoadFiszka
    {
        public static bool Embedd(string title)
        {
            EmbeddFiles subjects = new EmbeddFiles(PathManager.EmbeddTitlePath());

            string titleFormat = TitleManager.RemoveEmbeddTitle(subjects.Text, TitleManager.TitleList(PathManager.ToImprove()));

            string[] titleAray = titleFormat.Split('#');

            for (int i = 0; i < titleAray.Length; i++)
            {
                if (titleAray[i] == title)
                    return true;
            }

            return false;
        }

        public static string Fiszka(string title, string folder)
        {
            if(Embedd(title))
            {
                EmbeddFiles fiszka = new EmbeddFiles(PathManager.EmbeddPath() + title + ".txt");
                return fiszka.Text;
            }
            else
            {
                if (FileManager.FileExist(title + ".txt", folder).Result)
                {
                    Task<string> task = FileManager.Read(title + ".txt", folder);
                    return task.Result;
                }
                else
                    return "";
            }
        }
    }
}
