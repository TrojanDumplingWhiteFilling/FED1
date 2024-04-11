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
        [ObservableProperty]
        private string name;
        [ObservableProperty]
        private double amount;
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
        public async Task DeleteDebtor(Debtor debtor)
        {
            await _database.DeleteDebtor(debtor);
            Debtors.Remove(debtor);
        }

        [RelayCommand]
        public async Task ClearAllData()
        {
            await _database.ClearAllData();
            Debtors.Clear();
        }

        [ObservableProperty]
        Debtor selectedDebtor = null;


        [RelayCommand]
        public async Task ViewTransactions()
        {
            // Navigate to TransactionsPage passing debtor's ID
            await AppShell.Current.GoToAsync(nameof(OverviewPage));
        }

        [RelayCommand]
        async Task Navigate() =>
            await AppShell.Current.GoToAsync(nameof(AddPage));

        [RelayCommand]
        async Task NavigateOver() =>
            await AppShell.Current.GoToAsync(nameof(OverviewPage));
    }
}
