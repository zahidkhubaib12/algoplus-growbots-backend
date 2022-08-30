using GrowBotsAlgoplus.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace GrowBotsAlgoplus.Application
{
    public interface IAccountApplication
    {

        Task<bool> ForgotPassword(string email);
        Task<Users> GetUserByEmail(string email);
        Task<Users> GetUserById(int UserId);
        Task<List<Users>> GetUsersList();
        Task<int> RegisterUser(Users User);
        Task<bool> UpdateUser(Users User);
        Task<bool> PasswordReset(Users users);

    }
}
