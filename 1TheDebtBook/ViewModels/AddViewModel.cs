using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Microsoft.Maui.Platform;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SQLite;
using _1TheDebtBook.Data;
using _1TheDebtBook.Models;

namespace _1TheDebtBook.ViewModels
{
    public partial class AddViewModel : ObservableObject
    {
        [ObservableProperty]
        Debtor debtor;

        MainViewModel debtorsViewModel;
        public AddViewModel(MainViewModel? debtorsViewModel)
        {
            _database = new Database();
            this.debtorsViewModel = debtorsViewModel!;
            debtor = null!;
        }

        [ObservableProperty]
        [NotifyPropertyChangedFor(nameof(ViewName))]
        public string? name;
        public string? ViewName => Name;

        [ObservableProperty]
        [NotifyPropertyChangedFor(nameof(ViewAmount))]
        double amount;
        public double ViewAmount => Amount;

        [RelayCommand]
        public async Task AddDebtorView()
        {
            Debtor debtor = new Debtor
            {
                Name = ViewName ?? string.Empty,
                Amount = Amount
            };

            dTransaction dTrans = new dTransaction()
            {
                Amount = Amount
            };

            await _database.AddDebtorAsync(debtor, dTrans);
            debtorsViewModel.Debtors.Add(debtor);
        }

        private readonly Database _database;
    }
}

