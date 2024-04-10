using SQLite;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CommunityToolkit.Mvvm.ComponentModel;

namespace _1TheDebtBook.Models
{
    public partial class dTransaction : ObservableObject
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }

        [ObservableProperty]
        private DateTime dTransactionDate = DateTime.Now;
        [ObservableProperty]
        private double amount;
        [ForeignKey("DebtorId")]
        public int DebtorId { get; set; }
    }
}
