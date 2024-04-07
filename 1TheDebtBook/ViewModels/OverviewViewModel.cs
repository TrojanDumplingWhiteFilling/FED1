﻿using _1TheDebtBook.Data;
using CommunityToolkit.Mvvm.ComponentModel;
using System.Collections.ObjectModel;
using CommunityToolkit.Mvvm.Input;
using _1TheDebtBook.Models;
using _1TheDebtBook.Pages;
using System.Threading.Tasks;

namespace _1TheDebtBook.ViewModels;



[QueryProperty(nameof(Debtor), "debtorId")]
public partial class OverviewViewModel : ObservableObject
{
    private int _debtorId;
    public int DebtorId
    {
        get => _debtorId;
        set => SetProperty(ref _debtorId, value);
    }
    private readonly Database _database;
    public ObservableCollection<dTransaction> Transactions { get; }

    public OverviewViewModel()
    {
        _database = new Database();
        Transactions = new ObservableCollection<dTransaction>();
    }

    public async Task InitializeWithDebtor(int debtorId)
    {
        _debtorId = debtorId;
        var transactions = await _database.GetTransactionsForDebtor(_debtorId);
        foreach (var transaction in transactions)
        {
            Transactions.Add(transaction);
        }
    }

    [ObservableProperty]
    private double inputAmount;



    [RelayCommand]
    public async Task AddTransaction()
    {
        dTransaction transaction = new dTransaction
        {
            Amount = InputAmount,
            DebtorId = this.DebtorId // Correct the casing to match the property name
        };

        await _database.AddTransaction(transaction);
        Transactions.Add(transaction);

        // TransactionViewModel.UpdateDebtorAmount(transaction.DebtorId, transaction.Amount);
    }

}