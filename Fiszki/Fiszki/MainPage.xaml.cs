using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using Fiszki.Classes;
using System.IO;
using Fiszki.PageClasses;

namespace Fiszki
{
    public partial class MainPage : ContentPage
    {
        MainPageManager pageManager;
        public MainPage(bool incorrect)
        {
            pageManager = new MainPageManager(incorrect);
            pageManager.Refresh();
            InitializeComponent();
            BindingContext = this.pageManager;
            if (incorrect)
            {
                pik_category.ItemsSource = TitleManager.TitleList(PathManager.ToImprove());
            }
            else
            {
                pik_category.ItemsSource = TitleManager.TitleList(PathManager.Fiszki());
            }
            
        }

        protected override void OnAppearing()
        {
            TitleManager.LoadEmbeddTitle();
            pageManager.InCorrectFlag = false;
            pageManager.Refresh();
            pik_category.ItemsSource = TitleManager.TitleList(PathManager.Fiszki());
            base.OnAppearing();
        }

        private void Btn_start_Clicked(object sender, EventArgs e)
        {
            if (pik_category.SelectedItem != null)
                Navigation.PushAsync(new FiszkaPage(pik_category.SelectedItem.ToString(), pageManager.InCorrectFlag));
            else
                DisplayAlert("Informacja", "Wybierz temat fiszki", "OK.");
        }

        private void Tbi_stats_Clicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new StatsPage());
        }

        private void Tbi_addFiszka_Clicked(object sender, EventArgs e)
        {
            DisplayAlert("Kominukat", "Funkcja niedostępna w tej wersji aplikacji", "OK");
        }

        private void Tbi_mode_Clicked(object sender, EventArgs e)
        {
            if (pageManager.InCorrectFlag)
            {
                pageManager.InCorrectFlag = false;
                pageManager.Refresh();
                pik_category.ItemsSource = TitleManager.TitleList(PathManager.Fiszki());
            }
            else
            {
                if (FileManager.FileExist(PathManager.TitleFileName(), PathManager.ToImprove()).Result)
                {
                    pageManager.InCorrectFlag = true;
                    pageManager.Refresh();
                    pik_category.ItemsSource = TitleManager.TitleList(PathManager.ToImprove());
                }
                else
                    FileManager.Write(PathManager.TitleFileName(), String.Empty, PathManager.ToImprove(), false);
            }
        }
    }
}
