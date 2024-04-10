using _1TheDebtBook.Data;
using CommunityToolkit.Mvvm.ComponentModel;
using System.Collections.ObjectModel;
using CommunityToolkit.Mvvm.Input;
using _1TheDebtBook.Models;
using _1TheDebtBook.Pages;


namespace _1TheDebtBook.ViewModels;

public partial class OverviewViewModel : ObservableObject
{
    private int DebtorId;
    [ObservableProperty]
    private ObservableCollection<dTransaction> _transactions;
    [ObservableProperty]
    private DateTime dTransactionDate;
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
    double inputAmount;


    [RelayCommand]
    public async Task AddTransaction()
    {
        dTransaction transaction = new dTransaction
        {
            Amount = InputAmount,
            DebtorId = _debtorsViewModel.SelectedDebtor.Id
        };
        _debtorsViewModel.SelectedDebtor.Amount += InputAmount;

        await _database.AddTransaction(transaction);
        Transactions.Add(transaction);
    }
}