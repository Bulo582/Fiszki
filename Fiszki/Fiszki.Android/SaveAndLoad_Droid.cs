using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Fiszki.Droid;
using Xamarin.Forms;

[assembly: Dependency(typeof(SaveAndLoad_Droid))]
namespace Fiszki.Droid
{
    class SaveAndLoad_Droid : ISaveAndLoad
    {
        public async Task SaveTextAsync(string filename, string folder, string text, bool append)
        {
            if (append)
            {
                var documentsPath = System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal);
                documentsPath = Path.Combine(documentsPath, folder);
                string filePath = Path.Combine(documentsPath, filename);
                System.IO.File.AppendAllText(filePath, text);
            }
            else
            {
                var documentsPath = System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal);
                documentsPath = Path.Combine(documentsPath, folder);
                string filePath = Path.Combine(documentsPath, filename);
                System.IO.File.WriteAllText(filePath, text);
            }
        }

        public async Task<string> LoadTextAsync(string filename, string folder)
        {
            var documentsPath = System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal);
            documentsPath = Path.Combine(documentsPath, folder);
            string filePath = Path.Combine(documentsPath, filename);
            return System.IO.File.ReadAllText(filePath);
        }

        public async Task<bool> FileExistAsync(string filename, string folder)
        {
            var documentsPath = System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal);
            documentsPath = Path.Combine(documentsPath, folder);
            string filePath = Path.Combine(documentsPath, filename);
            return System.IO.File.Exists(filePath);
        }

        public async Task CreateFolderAsync(string FolderName)
        {
            var folderPath = System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal);
            string path = Path.Combine(folderPath, FolderName);

            Directory.CreateDirectory(path);
        }

        public async Task DeleteFileAsync(string fileName, string folderName)
        {
            var documentsPath = System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal);
            documentsPath = Path.Combine(documentsPath, folderName);
            string filePath = Path.Combine(documentsPath, fileName);
            System.IO.File.Delete(filePath);
        }

        public async Task<bool> FolderExist(string FolderName)
        {
            var folderPath = System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal);
            string path = Path.Combine(folderPath, FolderName);
            return System.IO.Directory.Exists(path);
        }
    }
}