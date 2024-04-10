using _1TheDebtBook.Data;
using CommunityToolkit.Mvvm.ComponentModel;
using System.Collections.ObjectModel;
using _1TheDebtBook.Pages;
using CommunityToolkit.Mvvm.Input;
using _1TheDebtBook.Models;


namespace _1TheDebtBook.ViewModels
{
    public partial class MainViewModel : ObservableObject
    {
        [ObservableProperty]
        private ObservableCollection<Debtor> _debtors;

        public MainViewModel()
        {
            Debtors = new ObservableCollection<Debtor>();
            _database = new Database();
            _ = Initialize();
        }

        private readonly Database _database;

        private async Task Initialize()
        {
            var debtorViews = await _database.GetDebtors();
            foreach (var debtorView in debtorViews)
            {
                Debtors.Add(debtorView);
            }
        }

        [RelayCommand]
        async Task Navigate() =>
            await AppShell.Current.GoToAsync(nameof(AddPage));

        [RelayCommand]
        public async Task ClearAllData()
        {
            await _database.ClearAllData();
        }

        [ObservableProperty]
        Debtor _selectedDebtor;


        [RelayCommand]
        public async Task ViewTransactions()
        {
            if (SelectedDebtor != null)
            {
               var transactions = await _database.GetTransactionsForDebtor(SelectedDebtor.Id);

               await AppShell.Current.GoToAsync($"{nameof(OverviewPage)}?DebtorId={SelectedDebtor.Id}");
            }
        }

        [RelayCommand]
        async Task NavigateOver() =>
            await AppShell.Current.GoToAsync(nameof(OverviewPage));
    }
}
