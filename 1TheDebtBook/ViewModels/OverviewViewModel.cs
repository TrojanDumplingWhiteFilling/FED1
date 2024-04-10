using _1TheDebtBook.Data;
using CommunityToolkit.Mvvm.ComponentModel;
using System.Collections.ObjectModel;
using CommunityToolkit.Mvvm.Input;
using _1TheDebtBook.Models;
using _1TheDebtBook.Pages;
using PInvoke;


namespace _1TheDebtBook.ViewModels;

public partial class OverviewViewModel : ObservableObject
{
    private int DebtorId;
    [ObservableProperty]
    private ObservableCollection<dTransaction> _transactions;
    [ObservableProperty]
    private DateTime dTransactionDate = DateTime.Now;
    [ObservableProperty]
    private double amount;
    private readonly Database _database;
    MainViewModel _debtorsViewModel;
    public OverviewViewModel(MainViewModel debtorsViewModel)
    {
        _debtorsViewModel = debtorsViewModel;
        Transactions = new ObservableCollection<dTransaction>();
        _database = new Database();
        _ = Initialize();
    }
    private async Task Initialize()
    {
        var dTransactionViews = await _database.GetTransactionsForDebtor(_debtorsViewModel.SelectedDebtor.Id);
        foreach (var dTransactionView in dTransactionViews)
        {
            Transactions.Add(dTransactionView);
        }
    }

    [ObservableProperty]
    private double? inputAmount; // Nullable double to avoid binding failures when the input is empty




    [RelayCommand]
    public async Task AddTransaction()
    {
        if (InputAmount.HasValue)
        {
            dTransaction transaction = new dTransaction
            {
                Amount = InputAmount.Value,
                DebtorId = _debtorsViewModel.SelectedDebtor.Id,
                DTransactionDate = DTransactionDate
            };
            _debtorsViewModel.SelectedDebtor.Amount += InputAmount.Value;

            await _database.AddTransaction(transaction);
            Transactions.Add(transaction);
        }
        else
        {
            // Handle case where InputAmount is null
            Console.WriteLine("Error: InputAmount is null. Cannot add transaction.");
        }
    }

    [RelayCommand]
    public async void BackButtonPressed()
    {
        await AppShell.Current.GoToAsync(nameof(MainPage));
    }
}