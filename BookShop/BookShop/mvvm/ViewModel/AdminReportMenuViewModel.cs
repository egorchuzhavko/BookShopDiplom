using System.Windows.Input;
using Xamarin.Forms;
using System.IO;
using Syncfusion.DocIO.DLS;
using Syncfusion.DocIO;
using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Wordprocessing;
using DocumentFormat.OpenXml;
using System;
using System.Collections.Generic;

namespace BookShop.mvvm.ViewModel
{
    class AdminReportMenuViewModel : BaseViewModel
    {
        public AdminReportMenuViewModel() {
            ReportTypes = GetTypes();
        }

        DateTime selectedDateFrom;
        public DateTime SelectedDateFrom {
            get
            {
                return selectedDateFrom;
            }
            set
            {
                selectedDateFrom = value;
                OnPropertyChanged();
            }
        }

        DateTime selectedDateTo;
        public DateTime SelectedDateTo {
            get
            {
                return selectedDateTo;
            }
            set
            {
                selectedDateTo = value;
                OnPropertyChanged();
            }
        }

        private TypeReport selectedType;
        public TypeReport SelectedType {
            get
            {
                return selectedType;
            }
            set
            {
                selectedType = value;
                OnPropertyChanged();
            }
        }

        List<TypeReport> reportTypes;
        public List<TypeReport> ReportTypes {
            get
            {
                return reportTypes;
            }
            set
            {
                reportTypes = value;
                OnPropertyChanged();
            }
        }

        public ICommand CreateReportCommand {
            get
            {
                return new Command(async (e) => {
                    if (selectedType != null)
                        await Xamarin.Forms.DependencyService.Get<ISave>().SaveAndView(selectedDateFrom, selectedDateTo, selectedType.Value);
                    else
                        await App.Current.MainPage.DisplayAlert("Ошибка!", "Вы должны выбрать параметр для формирования отчёта!", "Ок");
                });
            }
        }

        public List<TypeReport> GetTypes() {
            return new List<TypeReport> {
                new TypeReport {Key=2, Value="Пользователь"},
                new TypeReport {Key=1, Value="Книга"},
                new TypeReport {Key=3, Value="Заказ"},
                new TypeReport {Key=4, Value="Все"}
            };
        }

    }

    public class TypeReport {
        public int Key { get; set; }
        public string Value { get; set; }
    }
}
