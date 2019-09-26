using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using Xamarin.Forms;
using System.Threading.Tasks;

namespace Fiszki.Classes
{
    static class DirectoryManager
    {


        public static async Task<bool> FolderExist(string folder)
        {
            bool flag = await DependencyService.Get<ISaveAndLoad>().FolderExist(folder);

            return flag;
        }

        public static async Task CreateFolder(string folderName)
        {
           await DependencyService.Get<ISaveAndLoad>().CreateFolderAsync(folderName);
        }

    }
}
