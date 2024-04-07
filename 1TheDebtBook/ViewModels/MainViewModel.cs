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
        private static MainViewModel? _instance;
        public static MainViewModel Instance => _instance ??= new MainViewModel();
        private int _debtorId; // Backing field for DebtorId
        public int DebtorId
        {
            get => _debtorId;
            set => SetProperty(ref _debtorId, value);
        }

        [ObservableProperty]
        private ObservableCollection<Debtor> _debtors;

        [ObservableProperty]
        private Debtor? _selectedDebtor;

        public MainViewModel()
        {
            Debtors = new ObservableCollection<Debtor>();
            _database = new Database();
            _ = Initialize();
        }

        // Updates the amount of a debtor, from the overviewViewModel
        public void UpdateDebtorAmount(int debtorId, double amount)
        {
            var debtor = Debtors.FirstOrDefault(d => d.Id == debtorId);
            if (debtor != null)
            {
                debtor.Amount += amount; // Adds the new transaction amount to the existing amount
            }
        }

        private readonly Database _database;

        private async Task Initialize()
        {
            var debtorViews = await _database.GetDebtors();
            foreach (var debtorView in debtorViews)
            {
                Debtors.Add(debtorView);
            }

            var overviewViewModel = new OverviewViewModel();
        }

        [RelayCommand]
        async Task Navigate() =>
            await AppShell.Current.GoToAsync(nameof(AddPage));

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
        }

        [RelayCommand]
        public async Task ViewTransactions()
        {
            if (SelectedDebtor != null)
            {
                DebtorId = SelectedDebtor.Id; // Set DebtorId to the ID of the selected debtor
                var route = $"{nameof(OverviewPage)}?DebtorId={DebtorId}";
                await Shell.Current.GoToAsync(route);
            }
        }



        [RelayCommand]
        async Task NavigateOver() =>
            await AppShell.Current.GoToAsync(nameof(OverviewPage));
    }
}
