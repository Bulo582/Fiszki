using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Fiszki.FiszkaClasses;
using Fiszki.Classes;

namespace Fiszki
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class FiszkaPage : ContentPage
	{
        private FiszkaContainer fiszka;
        private FiszkaObservable fiszkaViewModel;
        private StatsManager stats;
        int currentIndex = 0;
        int lastIndex;
        bool inproveMode;

		public FiszkaPage (string subject, bool inproveMode)
		{
            
            this.inproveMode = inproveMode;
			InitializeComponent ();

            if(!inproveMode)
                fiszka = new FiszkaContainer(LoadFiszka.Fiszka(subject, PathManager.Fiszki()));
            else
                fiszka = new FiszkaContainer(LoadFiszka.Fiszka(subject, PathManager.ToImprove()));

            fiszkaViewModel = new FiszkaObservable(fiszka.Title);
            lastIndex = fiszka.D_Fiszki.Count;
            RefreshFiszka();
		}
        
        public void RefreshFiszka()
        {
            try
            {
                lbl_count.Text = (((fiszka.correct) * 100) / lastIndex).ToString() + " % poprawnych";
                lbl_title.Text = fiszka.Title + " (" + (currentIndex) + " / " + lastIndex + ")";
                lbl_pl.Text = fiszka.pl[currentIndex];
                list_done.ItemsSource = fiszkaViewModel.fiszkaObservable;
            }
            catch(IndexOutOfRangeException ex)
            {
                lbl_pl.Text = fiszka.pl[currentIndex-1];
                list_done.ItemsSource = fiszkaViewModel.fiszkaObservable;
            }
        }

        private void Btn_complete_Clicked(object sender, EventArgs e)
        {
            // jeśli text ang nie jest pusty
            if (ent_ang.Text != null)
            {
                if (currentIndex < lastIndex)
                {
                    fiszkaViewModel.Add(currentIndex, fiszka.pl[currentIndex], ent_ang.Text, fiszka.ang[currentIndex]);
                    if(ent_ang.Text.ToUpper() == fiszka.ang[currentIndex].ToUpper())
                    {
                        fiszka.correct++;
                    }
                    else
                    {
                        fiszka.incorrectWords.Add(fiszka.ang[currentIndex]);
                    }
                }

                currentIndex++;

                if (currentIndex < lastIndex)
                {
                    RefreshFiszka();
                }
            }
            else
                DisplayAlert("Bład", "Wpisz słówko po angielski", "OK.");

            // po ostatniej fiszce
            if(currentIndex == lastIndex)
            {
                lbl_count.Text = (((fiszka.correct) * 100) / lastIndex).ToString() + " % poprawnych";
                // jeśli 100 % udanych
                if (fiszka.correct == lastIndex)
                {
                    if(inproveMode)
                    {
                        TitleManager.RemoveCustomTitle(fiszka.Title, PathManager.ToImprove());
                    }

                    RefreshFiszka();
                    stats = new StatsManager();
                    stats.Add(fiszka.Title);    
                    inproveMode = false;
                    DisplayAlert("Gratulacje!", "100 % Poprawnych odpowiedzi", "OK.", "");
                }
                // jeśli nie 100 % udanych
                else
                {
                    RefreshFiszka();
                    string content = fiszka.Title + "$" + fiszka.InCorrectFiszkaFormat;
                   
                    // Tworzy/Uaktualnia plik w folderze toImprove z fiszkami do poprawy
                    FileManager.Write(fiszka.Title +".txt", content, PathManager.ToImprove(), false);

                    if (!(TitleManager.CompareTitleIntoFile(fiszka.Title, PathManager.ToImprove())))
                    {
                        // Tworzy/Uaktualnia plik w folderze toImprove z tematami fiszek do poprawy
                        TitleManager.AddDontExistTitle(PathManager.ToImprove(), fiszka.Title);
                    }

                    DisplayAlert("Koniec", "Zakończyłeś fiszkę w " + (((fiszka.correct) * 100) / lastIndex).ToString() + " %", "OK.");

                    inproveMode = false;
                    
                }
            }
        }

        private void Tbi_exit_Clicked(object sender, EventArgs e)
        {
            if (currentIndex < lastIndex)
            {
                string content = fiszka.Title + "$" + fiszka.InCorrectFiszkaFormat;


                // Tworzy/Uaktualnia plik w folderze toImprove z fiszkami do poprawy
                FileManager.Write(fiszka.Title + ".txt", content, PathManager.ToImprove(), false);

                if (!(TitleManager.CompareTitleIntoFile(fiszka.Title, PathManager.ToImprove())))
                {
                    // Tworzy/Uaktualnia plik w folderze toImprove z tematami fiszek do poprawy
                    TitleManager.AddDontExistTitle(PathManager.ToImprove(), fiszka.Title);
                }

                DisplayAlert("Koniec", "Zakończyłeś fiszkę w " + (((fiszka.correct) * 100) / lastIndex).ToString() + " %", "OK" );

                inproveMode = false;
                Navigation.PopToRootAsync(true);
            }
            else
                Navigation.PopToRootAsync(true);
        }
    }
}