﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace BookShop.mvvm.ViewModel {
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class TabbedPageContainer : TabbedPage {
        public TabbedPageContainer() {
            InitializeComponent();
        }
    }
}