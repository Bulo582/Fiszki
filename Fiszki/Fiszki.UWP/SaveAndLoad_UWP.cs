using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Fiszki;
using Fiszki.UWP;
using Windows.Storage;
using Xamarin.Forms;


[assembly: Dependency(typeof(SaveAndLoad_UWP))]
namespace Fiszki.UWP
{
    class SaveAndLoad_UWP : ISaveAndLoad
    {
        public async Task SaveTextAsync(string filename, string folder, string text, bool append)
        {
            if(append)
            {
                StorageFolder localFolder = ApplicationData.Current.LocalFolder;
                localFolder = await localFolder.GetFolderAsync(folder);
                StorageFile sampleFile = await localFolder.CreateFileAsync(filename);
                await FileIO.AppendTextAsync(sampleFile, text);
            }
            else
            {
                StorageFolder localFolder = ApplicationData.Current.LocalFolder;
                localFolder = await localFolder.GetFolderAsync(folder);
                StorageFile sampleFile = await localFolder.CreateFileAsync(filename);
                await FileIO.WriteTextAsync(sampleFile, text);
            }
        }

        public async Task<string> LoadTextAsync(string filename, string folder)
        {

            StorageFolder storageFolder = ApplicationData.Current.LocalFolder;
            storageFolder = await storageFolder.GetFolderAsync(folder);
            StorageFile sampleFile = await storageFolder.GetFileAsync(filename);
            string text = await Windows.Storage.FileIO.ReadTextAsync(sampleFile);
            return text;
        }

        public async Task<bool> FileExistAsync(string filename, string folder)
        {
            
            StorageFolder storageFolder = ApplicationData.Current.LocalFolder;
            storageFolder = await storageFolder.GetFolderAsync(folder);
            var item = await storageFolder.TryGetItemAsync(filename);
            if (item != null)
                return true;
            else
                return false;
        }

        public async Task CreateFolderAsync(string name)
        {
            string folderName = name ;
            StorageFolder folder = ApplicationData.Current.LocalFolder;
            await folder.CreateFolderAsync(folderName);
        }

        public async Task DeleteFileAsync(string fileName, string folderName)
        {
            StorageFolder storageFolder = ApplicationData.Current.LocalFolder;
            storageFolder = await storageFolder.GetFolderAsync(folderName);
            var item = await storageFolder.TryGetItemAsync(fileName);
        }

        public async Task<bool> FolderExist(string folderName)
        {
            StorageFolder storageFolder = ApplicationData.Current.LocalFolder;
            var item = await storageFolder.TryGetItemAsync(folderName);

            if (item != null)
                return true;
            else
                return false;
        }
    }
}
