using Instagroom.Contracts;
using Instagroom.Model;
using SQLite;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Instagroom.Services
{
    class UserDataService : IUserDataService
    {
        private SQLiteAsyncConnection _dbConnection;
        public User CurrentUser { get; set; }

        public async Task<bool> AddUserAsync(User newUser)
        {
            try
            {
                await _dbConnection.InsertAsync(newUser);
                return true;
            }
            catch(Exception ex)
            {
                return false;
            }
        }

        public async Task<User> GetUserByUsernameAsync(string username)
        {
            var user = await _dbConnection.GetAsync<User>(u => (u.Username == username));
            return user;
        }
        public UserDataService(ISQLiteConnectionService connectionService)
        {
            _dbConnection = connectionService.GetConnection();
            _dbConnection.CreateTableAsync<User>();
        }
    }
}
