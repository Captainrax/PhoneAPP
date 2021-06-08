using PhoneApp.ViewModels;
using SQLite;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace PhoneApp.Data
{
    public class EmployeeDatabase
    {
        readonly SQLiteAsyncConnection database;

        public EmployeeDatabase(string dbPath)
        {
            database = new SQLiteAsyncConnection(dbPath);
            database.CreateTableAsync<Employee>().Wait();
        }

        public Task<List<Employee>> GetEmployeesAsync()
        {
            //Get all employees.
            return database.Table<Employee>().ToListAsync();
        }

        public Task<Employee> GetEmployeeAsync(int id)
        {
            // Get a specific employee.
            return database.Table<Employee>()
                            .Where(i => i.ID == id)
                            .FirstOrDefaultAsync();
        }

        public Task<int> SaveEmployeeAsync(Employee employee)
        {
            if (employee.ID != 0)
            {
                // Update an existing employee.
                return database.UpdateAsync(employee);
            }
            else
            {
                // Save a new employee.
                return database.InsertAsync(employee);
            }
        }

        public Task<int> DeleteEmployeeAsync(Employee employee)
        {
            // Delete an employee.
            return database.DeleteAsync(employee);
        }
    }
}
