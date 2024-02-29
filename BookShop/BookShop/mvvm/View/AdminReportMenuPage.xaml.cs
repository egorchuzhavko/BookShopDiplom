using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace BookShop.mvvm.View
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class AdminReportMenuPage : ContentPage
	{
		public AdminReportMenuPage ()
		{
			InitializeComponent ();
			BindingContext = new mvvm.ViewModel.AdminReportMenuViewModel();
			DateTo.Date = DateTime.Now;
		}
	}
}