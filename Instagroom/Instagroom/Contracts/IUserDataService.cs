using Instagroom.Model;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Instagroom.Contracts
{
    public interface IUserDataService
    {
        User CurrentUser { get; set; }

        Task<User> GetUserByUsernameAsync(string username);
        Task<bool> AddUserAsync(User newUser);
    }
}
