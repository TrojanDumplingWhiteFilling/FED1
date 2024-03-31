using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SQLite;

namespace _1TheDebtBook.Models
{
    public class Debtor {
    [PrimaryKey, AutoIncrement]
    public int Id { get; set; }

    public string Name{ get; set; }

    public double Amount { get; set; }
    }
}
