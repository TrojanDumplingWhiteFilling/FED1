using _1TheDebtBook.Pages;
using _1TheDebtBook.ViewModels;
using _1TheDebtBook.Helpers;
using _1TheDebtBook.Data;
using System.Collections.ObjectModel;


namespace _1TheDebtBook
{
    public partial class MainPage : ContentPage
    {

        public MainPage(MainViewModel vm)
        {
            InitializeComponent();
            BindingContext = vm;
        }
    }
}
