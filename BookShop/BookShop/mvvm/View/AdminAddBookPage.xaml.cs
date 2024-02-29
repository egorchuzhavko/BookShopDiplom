using BookShop.mvvm.Model;
using NativeMedia;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace BookShop.mvvm.View
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class AdminAddBookPage : ContentPage
	{
		public AdminAddBookPage ()
		{
			InitializeComponent ();
			BindingContext = new BookShop.mvvm.ViewModel.AdminAddBookViewModel();
		}
    }
}