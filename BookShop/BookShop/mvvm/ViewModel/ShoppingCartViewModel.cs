using BookShop.mvvm.Model;
using BookShop.mvvm.View;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;
using System.Linq;
using DocumentFormat.OpenXml;
using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Wordprocessing;
using System.IO;
using Xamarin.Essentials;

namespace BookShop.mvvm.ViewModel
{
    public class ShoppingCartViewModel : BaseViewModel
    {
        private ObservableCollection<BookOrder> books;
        public ObservableCollection<BookOrder> Books {
            get { 
                if(books == null) {
                    books = new ObservableCollection<BookOrder>();
                }
                return books; 
            }
            set
            {
                books = value;
                price = books.Sum(x => x.Book.Price * x.Count);
                OnPropertyChanged();
                OnPropertyChanged(nameof(Price));
            }
        }

        BookOrder selectedBook;
        public BookOrder SelectedBook {
            get { return selectedBook; }
            set
            {
                selectedBook = value;
                OnPropertyChanged();
            }
        }

        float price;
        public float Price {
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

        public void ChangeBooks(ObservableCollection<BookOrder> bks) {
            Books = bks;
        }

        public ICommand SelectionCommand => new Command(DisplayBook);

        private void DisplayBook() {
            if (SelectedBook != null) {
                var vm = new BookOnlyDetailsViewModel { SelectedBook = selectedBook };
                var bookdetailspage = new BookOnlyDetailsPage { BindingContext = vm };
                Application.Current.MainPage.Navigation.PushModalAsync(bookdetailspage);
                SelectedBook = null;
            }
        }

        bool flag;
        public bool ChangeBooksCount {
            get { return flag; }
            set
            {
                flag = value;
                OnPropertyChanged(nameof(Books));
            }
        }

        public ICommand MakeOrderCommand => new Command(MakeOrder);
        void MakeOrder() {
            if (books.Count != 0) {
                string nobooksres = "В интересующем вам количестве нет следующих книг: ";
                foreach (var book in books) {
                    if (!Book.CheckBookReadyForOrder(book.Book.Id, book.Count))
                        nobooksres += $"\n- {book.Book.Name};";
                }
                if (nobooksres != "В интересующем вам количестве нет следующих книг: ")
                    Application.Current.MainPage.DisplayAlert("Отказ!", nobooksres, "Ок");
                else {
                    int idOfOrder = Order.MakeOrder(App.ProfileViewModel.User.Id, price);
                    if (BookOrder.MakeBusyBooksByOrder(books, idOfOrder)) {
                        CreateDoc();
                        Application.Current.MainPage.DisplayAlert("Успех!", $"Вы успешно оформили заказ №{idOfOrder}. Вы можете отслеживать информацию и статус заказа в вашем профиле.", "Ок");
                        Books = new ObservableCollection<BookOrder>();
                    }
                }
            }
            else {
                Application.Current.MainPage.DisplayAlert("Ошибка!", "Ваша корзина пуста!", "Ок");
            }
        }

        public void CreateDoc() {
            try {
                Console.WriteLine("+");
                Order order = Order.FindLastUserOrder(App.ProfileViewModel.User.Id);
                string fileName = $"Заказ№{order.Id}.doc";
                string titleText = "Чек";
                string tovari = "";
                foreach (var book in order.BooksOrder) {
                    tovari += $"{book.Book.Name} - {book.Count} шт.\n";
                }
                string bodyText = $"\nТовары:\n{tovari}\n\nК оплате: {order.Price} рублей.";
                string documentsPath = System.Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);
                string filePath = Path.Combine(documentsPath, fileName);

                Console.WriteLine(filePath);

                using (WordprocessingDocument document = WordprocessingDocument.Create(filePath, WordprocessingDocumentType.Document)) {
                    // Добавляем основной документный раздел
                    MainDocumentPart mainPart = document.AddMainDocumentPart();

                    // Создаем новый документный элемент
                    Document documentElement = new Document();

                    // Создаем заголовок
                    Paragraph titleParagraph = new Paragraph();
                    Run titleRun = new Run();
                    Text titleTextElement = new Text(titleText);
                    titleRun.Append(titleTextElement);
                    titleParagraph.Append(titleRun);

                    // Добавляем заголовок в документ
                    documentElement.Body = new Body();
                    documentElement.Body.Append(titleParagraph);

                    // Создаем абзац с текстом
                    Paragraph bodyParagraph = new Paragraph();
                    Run bodyRun = new Run();
                    Text bodyTextElement = new Text(bodyText);
                    bodyRun.Append(bodyTextElement);
                    bodyParagraph.Append(bodyRun);

                    // Добавляем абзац в документ
                    documentElement.Body.Append(bodyParagraph);

                    mainPart.Document.Body.Append(documentElement);
                    mainPart.Document.Save();
                    document.Save();
                    Console.WriteLine("endendendendendendend");
                }
            }
            catch (Exception e) {
                Console.WriteLine("errorerrorerrorerrorerrorerror");
                Debug.WriteLine(e);
            }
            
        }
    }
}
