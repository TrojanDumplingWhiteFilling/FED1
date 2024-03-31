using SQLite;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _1TheDebtBook.Models
{
    public class dTransaction
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
        public DateTime dTransactionDate { get; set; }=DateTime.Now;
        public double Amount { get; set; }
        [ForeignKey("DebtorId")]
        public int DebtorId { get; set; }
    }
}
