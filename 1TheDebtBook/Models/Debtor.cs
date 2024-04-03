using CommunityToolkit.Mvvm.ComponentModel;
using SQLite;

namespace _1TheDebtBook.Models
{
    public partial class Debtor : ObservableObject
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }

        [ObservableProperty]
        private string? name;

        [ObservableProperty]
        private double amount;
    }
}
