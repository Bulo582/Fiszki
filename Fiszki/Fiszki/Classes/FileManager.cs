using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using Xamarin.Forms;
using System.Threading.Tasks;

namespace Fiszki.Classes
{
    class FileManager
    {
        public static async Task<string> Read(string name, string folder)
        {
            string content = await DependencyService.Get<ISaveAndLoad>().LoadTextAsync(name, folder);

            return content;
            
        }

        public static async void Write(string fileName, string content, string folder, bool append)
        {
           await DependencyService.Get<ISaveAndLoad>().SaveTextAsync(fileName, folder, content, append);
        }

        public static async Task<bool> FileExist(string fileName, string folder)
        {
            bool flag = await DependencyService.Get<ISaveAndLoad>().FileExistAsync(fileName, folder);

            return flag;
        }

        public static bool FileIsEmpty(string fileName, string folderName)
        {
            if (Read(fileName, folderName).Result == "")
                return true;
            else
                return false;
        }

        // usuwa wszytskie foldery
        private static void DeleteAll_Droid()
        {
            var documentsPath = System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal);
            documentsPath = Path.Combine(documentsPath, PathManager.Fiszki());
            System.IO.Directory.Delete(documentsPath, true);
            documentsPath = System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal);
            documentsPath = Path.Combine(documentsPath, PathManager.ToImprove());
            System.IO.Directory.Delete(documentsPath, true);
        }

    }
}
