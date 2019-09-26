using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Fiszki
{
    public interface ISaveAndLoad
    {

        Task SaveTextAsync(string filename, string folder, string text, bool append);
        Task<string> LoadTextAsync(string filename, string folder);
        Task<bool> FileExistAsync(string filename, string folder);
        Task CreateFolderAsync(string name);
        Task DeleteFileAsync(string fileName, string folder);
        Task<bool> FolderExist(string folderName);
    }
}
