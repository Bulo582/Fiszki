using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Fiszki.Classes;

[assembly: XamlCompilation(XamlCompilationOptions.Compile)]
namespace Fiszki
{
    public partial class App : Application
    {
        public App()
        {
            //FileManager.DeleteAll_Droid();

            InitializeComponent();

            StartFolderSynch();

            MainPage = new NavigationPage(new MainPage(false));
        }

        private async void StartFolderSynch()
        {
            try
            {
                if ((DirectoryManager.FolderExist("Fiszki")).Result == false )
                {
                    await DependencyService.Get<ISaveAndLoad>().CreateFolderAsync("Fiszki");
                }
                if((DirectoryManager.FolderExist("ToImprove")).Result == false)
                {
                    await DependencyService.Get<ISaveAndLoad>().CreateFolderAsync("ToImprove");
                }
                if(DirectoryManager.FolderExist("ToImprove").Result && DirectoryManager.FolderExist("Fiszki").Result)
                {
                    if (!(FileManager.FileExist("!Stats.txt", PathManager.ToImprove()).Result))
                        FileManager.Write(PathManager.StatsName(), String.Empty, PathManager.ToImprove(), false);

                    if (!(FileManager.FileExist(PathManager.TitleFileName(), PathManager.ToImprove()).Result))
                        FileManager.Write(PathManager.TitleFileName(), String.Empty, PathManager.ToImprove(), false);

                    TitleManager.LoadEmbeddTitle();
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex.Message);
            }
        }

        protected override void OnStart()
        {
            // Handle when your app starts
        }

        protected override void OnSleep()
        {
            // Handle when your app sleeps
        }

        protected override void OnResume()
        {
            // Handle when your app resumes
        }
    }
}
