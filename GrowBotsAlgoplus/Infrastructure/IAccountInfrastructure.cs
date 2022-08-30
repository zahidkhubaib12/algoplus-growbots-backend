using GrowBotsAlgoplus.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace GrowBotsAlgoplus.Infrastructure
{
    public interface IAccountInfrastructure
    {
        Task<Users> GetUserByEmail(string email);
        Task<bool> PasswordReset(Users users);
        Task<Users> GetUserById(int UserId);
        Task<List<Users>> GetUsersList();
        Task<int> RegisterUser(Users User);
        Task<bool> UpdateUser(Users User);

    }
}
