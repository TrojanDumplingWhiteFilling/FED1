using SQLite;
using _1TheDebtBook.Models;

namespace _1TheDebtBook.Data
{
    internal class Database
    {
        private readonly SQLiteAsyncConnection _connection;
        public Database()
        {
            var dataDir = FileSystem.AppDataDirectory;
            var databasePath = Path.Combine(dataDir, "DeptBook.db");
            var dbOptions = new SQLiteConnectionString(databasePath, true);

            _connection = new SQLiteAsyncConnection(dbOptions);
            _ = Initialise();
        }
        private async Task Initialise()
        {
            await _connection.CreateTableAsync<Debtor>();
            await _connection.CreateTableAsync<dTransaction>(); // Create Transaction table
        }
        public async Task<List<Debtor>> GetDebtors()
        {
            return await _connection.Table<Debtor>().ToListAsync();
        }
        public async Task<Debtor> GetDebtor(int id)
        {
            var query = _connection.Table<Debtor>().Where(t => t.Id == id);
            return await query.FirstOrDefaultAsync();
        }
        public async Task<int> AddDebtor(Debtor item)
        {
            return await _connection.InsertAsync(item);
        }
        public async Task<int> DeleteDebtor(Debtor item)
        {
            return await _connection.DeleteAsync(item);
        }
        public async Task<int> UpdateDebtor(int id, double amount)
        {
            var debtor = await GetDebtor(id);
            debtor.Amount += amount;
            return await _connection.UpdateAsync(debtor);
        }

        // Methods for Transactions
        public async Task<List<dTransaction>> GetTransactions()
        {
            return await _connection.Table<dTransaction>().ToListAsync();
        }
        public async Task<List<dTransaction>> GetTransaction(int id)
        {
            var query = _connection.Table<dTransaction>().Where(t => t.Id == id);
            return await query.ToListAsync();
        }
        public async Task<int> AddTransaction(dTransaction item)
        {
            return await _connection.InsertAsync(item);
        }
        public async Task<int> DeleteTransaction(dTransaction item)
        {
            return await _connection.DeleteAsync(item);
        }
        public async Task<int> UpdateTransaction(dTransaction item)
        {
            return await _connection.UpdateAsync(item);
        }

        // Method to get transactions for a specific debtor ID
        public async Task<List<dTransaction>> GetTransactionsForDebtor(int debtorId)
        {
            var query = _connection.Table<dTransaction>().Where(t => t.DebtorId == debtorId);
            return await query.ToListAsync();
        }
        // Method to clear all data from both tables
        public async Task ClearAllData()
        {
            await _connection.DeleteAllAsync<Debtor>();
            await _connection.DeleteAllAsync<dTransaction>();
        }
    }
}