using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Fiszki.PageClasses;
using Fiszki.Classes;

namespace Fiszki
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class StatsPage : ContentPage
	{
        StatsManager stats;
		public StatsPage ()
		{
            InitializeComponent();
            stats = new StatsManager();
            if (stats.Stats.Count == 0)
            {
                lbl_nostats.Text = "Brak zakończynych fiszek";
                lbl_nostats.IsEnabled = true;
            }
            else
            {
                list_stats.ItemsSource = stats.Stats;
            }
		}
	}
}