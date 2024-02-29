using System;
using System.Collections.Generic;
using System.Text;
using System.Threading; 
using System.Windows.Input;
using Xamarin.Forms;
using NativeMedia;
using System.Diagnostics;
using System.Linq;
using BookShop.mvvm.Model;
using Xamarin.Essentials;

namespace BookShop.mvvm.ViewModel {
    public class AdminAddBookViewModel : BaseViewModel {

        public AdminAddBookViewModel() {
            Statusesbook = GetStatuses();
        }

        string bookname="";
        public string Bookname {
            get
            {
                return bookname;
            }
            set
            {
                bookname = value;
                OnPropertyChanged();
            }
        }

        float? price;
        public float? Price {
            get
            {
                return price;
            }
            set
            {
                price = value;
                OnPropertyChanged();
            }
        }

        string typeofbook="";
        public string Typeofbook {
            get
            {
                return typeofbook;
            }
            set
            {
                typeofbook = value;
                OnPropertyChanged();
            }
        }

        string author = "";
        public string Author {
            get
            {
                return author;
            }
            set
            {
                author = value;
                OnPropertyChanged();
            }
        }

        int? count;

        public int? Count {
            get
            {
                return count;
            }
            set
            {
                count = value;
                OnPropertyChanged();
            }
        }

        string description="";
        public string Description {
            get
            {
                return description;
            }
            set
            {
                description = value;
                OnPropertyChanged();
            }
        }

        ImageSource imagePath;
        string fullpath = "";
        string filename = "";
        public ImageSource ImagePath {
            get
            {
                return imagePath;
            }
            set
            {
                imagePath = value;
                OnPropertyChanged();
            }
        }

        List<TypebookClass> statusesbook;
        public List<TypebookClass> Statusesbook {
            get
            {
                return statusesbook;
            }
            set
            {
                statusesbook = value;
                OnPropertyChanged();
            }
        }

        TypebookClass selectedstatus;
        public TypebookClass Selectedstatus {
            get
            {
                return selectedstatus;
            }
            set
            {
                selectedstatus = value;
                OnPropertyChanged();
            }
        }

        public ICommand OpenImageCommand {
            get
            {
                return new Command(async (e) => {
                    try {
                        var photo = await MediaPicker.PickPhotoAsync();
                        fullpath = photo.FullPath;
                        filename = photo.FileName;
                        Console.WriteLine(fullpath);
                        ImagePath = ImageSource.FromFile(photo.FullPath);
                    }catch(Exception ex) {
                        await App.Current.MainPage.DisplayAlert("Отмена", "Фото не было выбрано", "Ок");
                    }
                });
            }
        }

        public ICommand AddBookCommand {
            get
            {
                return new Command(async (e) => {
                    if(bookname!="" & price!=0 & Selectedstatus!=null & author!="" & count!=0 & description!="" & fullpath != "") {
                        if (Book.InsertNewBook(bookname, price, description, selectedstatus.Value, author, filename, count)) {
                            await SFTPService.UploadImage(fullpath, filename);
                            await Application.Current.MainPage.DisplayAlert("Успех!", "Книга была добавлена!", "Ок");
                        }
                        else {
                            await Application.Current.MainPage.DisplayAlert("Ошибка!", "Ошибка добавления книги!", "Ок");
                        }
                    }
                    else {
                        await Application.Current.MainPage.DisplayAlert("Ошибка!", "Заполните все данные!", "Ок");
                    }
                });
            }
        }

        public List<TypebookClass> GetStatuses() {
            return new List<TypebookClass> {
                new TypebookClass {Key=1, Value="Эпопея"},
                new TypebookClass {Key=2, Value="Эпос"},
                new TypebookClass {Key=3, Value="Роман"},
                new TypebookClass {Key=4, Value="Повесть"},
                new TypebookClass {Key=5, Value="Новелла"},
                new TypebookClass {Key=6, Value="Рассказ"},
                new TypebookClass {Key=7, Value="Cкетч"},
                new TypebookClass {Key=8, Value="Пьеса"},
                new TypebookClass {Key=9, Value="Очерк"},
                new TypebookClass {Key=10, Value="Эссе"},
                new TypebookClass {Key=11, Value="Ода"},
                new TypebookClass {Key=12, Value="Видения"},
                new TypebookClass {Key=13, Value="Баллада"}
            };
        }
    } 
}
