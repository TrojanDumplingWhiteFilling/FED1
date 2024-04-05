using _1TheDebtBook.Data;
using CommunityToolkit.Mvvm.ComponentModel;
using System.Collections.ObjectModel;
using CommunityToolkit.Mvvm.Input;
using _1TheDebtBook.Models;
using _1TheDebtBook.Pages;


namespace _1TheDebtBook.ViewModels;
[QueryProperty(nameof(Debtor), "debtorId")]
public partial class OverviewViewModel : ObservableObject
{
    [ObservableProperty]
    private ObservableCollection<dTransaction> _transactions;

    private readonly Database _database;

    MainViewModel TransactionViewModel;
    public OverviewViewModel(MainViewModel TransactionViewModel)
    {
        Transactions = new ObservableCollection<dTransaction>();
        _database = new Database();
        this.TransactionViewModel = TransactionViewModel;
        _ = Initialize();
    }
    private async Task Initialize()
    {
        var dTransactionViews = await _database.GetTransactions();
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
            // Assuming the transaction model has a DebtorId field or similar to link the transaction to the debtor
            Amount = InputAmount,
            DebtorId = this.debtorId // You need to know which debtor the transaction is for
        };

        await _database.AddTransaction(transaction);
        Transactions.Add(transaction);

        // Update the debtor's total amount in MainViewModel
        TransactionViewModel.UpdateDebtorAmount(transaction.DebtorId, transaction.Amount);
    }
}