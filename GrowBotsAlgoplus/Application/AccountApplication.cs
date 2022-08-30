using GrowBotsAlgoplus.Connector;
using GrowBotsAlgoplus.Infrastructure;
using GrowBotsAlgoplus.Models;
using Microsoft.Extensions.Configuration;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace GrowBotsAlgoplus.Application
{
    public class AccountApplication : IAccountApplication
    {
        public AccountApplication(IConfiguration configuration, IAccountInfrastructure accountInfrastructure, IServiceConnector serviceConnector)
        {
            Configuration = configuration;
            AccountInfrastructure = accountInfrastructure;
            ServiceConnector = serviceConnector;
        }
        public IConfiguration Configuration { get; }
        public IAccountInfrastructure AccountInfrastructure { get; set; }
        public IServiceConnector ServiceConnector { get; set; }
       
        public async Task<Users> GetUserByEmail(string email)
        {
            return await AccountInfrastructure.GetUserByEmail(email);
        }

        public async Task<Users> GetUserById(int UserId)
        {
            return await AccountInfrastructure.GetUserById(UserId);
        }

        public async Task<bool> ForgotPassword(string email)
        {
            Users user = await AccountInfrastructure.GetUserByEmail(email);
            if (user != null)
            {
                Email message = new Email();

                message.Subject = "Forget Password Email";
                message.Body = "Please reset your password by clicking on this  " +this.Configuration["ResetPasswordUrl"] + " ";
                message.To = email;

                return await this.ServiceConnector.sendEmail(message);

               
            }
            else
            {
                return false;
            }

        }

        public async Task<List<Users>> GetUsersList()
        {
            return await AccountInfrastructure.GetUsersList();

        }

        public async Task<int> RegisterUser(Users User)
        {
            return await AccountInfrastructure.RegisterUser(User);
        }

        public async Task<bool> UpdateUser(Users User)
        {
            return await AccountInfrastructure.UpdateUser(User);
        }

        public async Task<bool> PasswordReset(Users users)
        {
            return await AccountInfrastructure.PasswordReset(users);
        }
    }
}
