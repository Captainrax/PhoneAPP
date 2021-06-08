using PhoneApp.ViewModels;
using SQLite;
using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace PhoneApp.Data
{
    class LS_Database
    {
        static SQLiteAsyncConnection Database;

        public static readonly AsyncLazy<LS_Database> Instance = new AsyncLazy<LS_Database>(async () =>
        {
            var instance = new LS_Database();
            CreateTableResult result = await Database.CreateTableAsync<Counter>();
            return instance;
        });

        public LS_Database()
        {
            Database = new SQLiteAsyncConnection(LocalStorageConstants.DatabasePath, LocalStorageConstants.Flags);
        }
        // requests
        public Task<List<Counter>> GetItemsAsync()
        {
            return Database.Table<Counter>().ToListAsync();
        }

        //public Task<List<Counter>> GetItemsNotDoneAsync()
        //{
        //    // SQL queries are also possible
        //    return Database.QueryAsync<Counter>("SELECT * FROM [TodoItem] WHERE [Done] = 0");
        //}

        public Task<Counter> GetItemAsync(int id)
        {
            return Database.Table<Counter>().Where(i => i.ID == id).FirstOrDefaultAsync();
        }

        public Task<int> SaveItemAsync(Counter item)
        {
            if (item.ID != 0)
            {
                return Database.UpdateAsync(item);
            }
            else
            {
                return Database.InsertAsync(item);
            }
        }

        public Task<int> DeleteItemAsync(Counter item)
        {
            return Database.DeleteAsync(item);
        }
    }
    public class AsyncLazy<T> : Lazy<Task<T>>
    {
        readonly Lazy<Task<T>> instance;

        public AsyncLazy(Func<T> factory)
        {
            instance = new Lazy<Task<T>>(() => Task.Run(factory));
        }

        public AsyncLazy(Func<Task<T>> factory)
        {
            instance = new Lazy<Task<T>>(() => Task.Run(factory));
        }

        public TaskAwaiter<T> GetAwaiter()
        {
            return instance.Value.GetAwaiter();
        }
    }
}
