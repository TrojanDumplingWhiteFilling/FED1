using SQLite;
using _1TheDebtBook.Models;
using System.Transactions;

namespace _1TheDebtBook.Data
{
    internal class Database
    {
        private readonly SQLiteAsyncConnection _connection;
        public Database()
        {
            var dataDir = FileSystem.AppDataDirectory;
            var databasePath = Path.Combine(dataDir, "DeptBook.db");

            //string _dbEncryptionKey = SecureStorage.GetAsync("dbEncryptionKey").Result;

            //if(string.isnullorempty(_dbencryptionkey))
            //{
            //    guid g = guid.newguid();
            //    _dbencryptionkey = g.tostring();
            //    securestorage.setasync("dbencryptionkey", _dbencryptionkey);
            //}
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
            try
            {
                return await _connection.Table<Debtor>().ToListAsync();
            }
            catch (SQLite.SQLiteException ex)
            {
                Console.WriteLine(ex.Message);
                return new List<Debtor>();
            }


        }
        public async Task<Debtor> GetDebtor(int id)
        {
            var query = _connection.Table<Debtor>().Where(t => t.Id == id);
            return await query.FirstOrDefaultAsync();
        }
        public async Task<int> AddDebtorAsync(Debtor debtor, dTransaction Amount)
        {
            try
            {
                await _connection.InsertAsync(debtor);
                var id = await _connection.Table<Debtor>().OrderByDescending(d => d.Id).FirstOrDefaultAsync();
                Amount.DebtorId = id.Id;   // Set the debtor ID for the transaction

                await _connection.InsertAsync(Amount);
                return id.Id;
            }
            catch (SQLite.SQLiteException ex)
            {
                Console.WriteLine(ex.Message);
                return 0;
            }

        }
        public async Task<int> DeleteDebtor(Debtor item)
        {
            try
            {
                return await _connection.DeleteAsync(item);
            }
            catch (SQLite.SQLiteException ex)
            {
                Console.WriteLine(ex.Message);
                return 0;
            }

        }
        public async Task<int> UpdateDebtor(Debtor item)
        {
            try
            {
                return await _connection.UpdateAsync(item);
            }
            catch (SQLite.SQLiteException ex)
            {
                Console.WriteLine(ex.Message);
                return 0;
            }
        }

        // Methods for Transactions
        public async Task<List<dTransaction>> GetTransactions()
        {
            try
            {
                return await _connection.Table<dTransaction>().ToListAsync();
            }
            catch (SQLite.SQLiteException ex)
            {
                Console.WriteLine(ex.Message);
                return new List<dTransaction>();
            }

        }
        public async Task<dTransaction> GetTransaction(int id)
        {
            var query = _connection.Table<dTransaction>().Where(t => t.Id == id);
            return await query.FirstOrDefaultAsync();
        }
        public async Task<int> AddTransaction(dTransaction item)
        {
            try
            {
                return await _connection.InsertAsync(item);
            }
            catch (SQLite.SQLiteException ex)
            {
                Console.WriteLine(ex.Message);
                return 0;
            }

        }
        public async Task<int> DeleteTransaction(dTransaction item)
        {
            try
            {
                return await _connection.DeleteAsync(item);
            }
            catch (SQLite.SQLiteException ex)
            {
                Console.WriteLine(ex.Message);
                return 0;
            }
        }
        public async Task<int> UpdateTransaction(dTransaction item)
        {
            try
            {
                return await _connection.UpdateAsync(item);
            }
            catch (SQLite.SQLiteException ex)
            {
                Console.WriteLine(ex.Message);
                return 0;
            }
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
            try
            {
                await _connection.DeleteAllAsync<Debtor>();
                await _connection.DeleteAllAsync<dTransaction>();
            }
            catch (SQLite.SQLiteException ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
    }
}