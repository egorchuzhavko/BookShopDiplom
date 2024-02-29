using System;
using System.Collections.Generic;
using System.Text;

namespace BookShop.mvvm.ViewModel {
    public class AdminStatisticsViewModel : BaseViewModel {
        int userscount;
        public int UsersCount {
            get { return userscount; } 
            set
            {
                userscount = value;
                OnPropertyChanged();
            }
        }

        int remainingbookscount;
        public int Remainingbookscount {
            get { return remainingbookscount; }
            set
            {
                remainingbookscount = value;
                OnPropertyChanged();
            }
        }

        int soldbookscount;
        public int Soldbookscount {
            get { return soldbookscount; }
            set
            {
                soldbookscount = value; 
                OnPropertyChanged();
            }
        }

        int allorderscount;
        public int Allorderscount {
            get { return allorderscount; }
            set
            {
                allorderscount = value;
                OnPropertyChanged();
            }
        }

        int activeorderscount;
        public int Activeorderscount {
            get { return activeorderscount; }
            set
            {
                activeorderscount = value;
                OnPropertyChanged();
            }
        }

        int rejectedorderscount;
        public int Rejectedorderscount {
            get { return rejectedorderscount; }
            set
            {
                rejectedorderscount = value;
                OnPropertyChanged();
            }
        }
    }
}
