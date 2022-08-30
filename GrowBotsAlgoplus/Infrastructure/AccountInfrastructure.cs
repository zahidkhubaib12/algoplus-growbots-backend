using GrowBotsAlgoplus.Infrastructure.Extension;
using GrowBotsAlgoplus.Models;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Threading.Tasks;

namespace GrowBotsAlgoplus.Infrastructure
{
    public class AccountInfrastructure : BaseInfrastructure, IAccountInfrastructure
    {
        public AccountInfrastructure(IConfiguration configuration) : base(configuration)
        {
        }

        #region Constants
        //Stored procedure names

        private const string GetUserByEmailStoredProcedureName = "GetUserByEmail";
        private const string GetUserByIdStoredProcedureName = "GetUserById";
        private const string GetUsersListStoredProcedureName = "GetUsersList";
        private const string RegisterUserStoredProcedureName = "RegisterUser";
        private const string UpdateUserStoredProcedureName = "UpdateUser";
        private const string PasswordResetStoredProcedureName = "PasswordReset";

        //Column names
        private const string UserIdColumnName = "UserId";
        private const string EmailColumnName = "Email";
        private const string FirstNameColumnName = "FirstName";
        private const string LastNameColumnName = "LastName";
        private const string PasswordColumnName = "Password";
        private const string PasswordHashColumnName = "PasswordHash";
        private const string IdentificationNumberColumnName = "IdentificationNumber";
        private const string Address1NumberColumnName = "Address1";
        private const string PostalCodeNumberColumnName = "PostalCode";
        private const string ActiveColumnName = "Active";

        //Parameter names
        private const string UserIdParameterName = "PUserId";
        private const string EmailParameterName = "PEmail";
        private const string PasswordParameterName = "PPassword";
        private const string FirstNameParameterName = "PFirstName";
        private const string LastNameParameterName = "PLastName";
        private const string PasswordHashParameterName = "PPasswordHash";
        private const string IdentificationNumberParameterName = "PIdentificationNumber";
        private const string PhoneNumberParameterName = "PPhoneNumber";
        private const string Address1ParameterName = "PAddress1";
        private const string PostalCodeParameterName = "PPostalCode";
        private const string ActiveCodeParameterName = "PActive";


        #endregion


        #region methods

       
       


        public async Task<Users> GetUserByEmail(string email)
        {
            Users user = new Users();

            var parameters = new List<DbParameter>
            {
                base.GetParameter(AccountInfrastructure.EmailParameterName, email)
            };

            using (var dataReader = await base.ExecuteReader(parameters, AccountInfrastructure.GetUserByEmailStoredProcedureName, CommandType.StoredProcedure))
            {
                if (dataReader != null && dataReader.HasRows)
                {
                    if (dataReader.Read())
                    {
                        user = new Users
                        {
                            UserId = dataReader.GetIntegerValue(AccountInfrastructure.UserIdColumnName),
                            Email = dataReader.GetStringValue(AccountInfrastructure.EmailColumnName),
                            FirstName = dataReader.GetStringValue(AccountInfrastructure.FirstNameColumnName),
                            LastName = dataReader.GetStringValue(AccountInfrastructure.LastNameColumnName),
                            PasswordHash = dataReader.GetStringValue(AccountInfrastructure.PasswordHashColumnName)

                        };
                    }
                    if (!dataReader.IsClosed)
                    {
                        dataReader.Close();
                    }

                }
            }


            return user;
        }

        public async Task<Users> GetUserById(int UserId)
        {
            Users user = new Users();

            var parameters = new List<DbParameter>
            {
                base.GetParameter(AccountInfrastructure.UserIdParameterName, UserId)
            };

            using (var dataReader = await base.ExecuteReader(parameters, AccountInfrastructure.GetUserByIdStoredProcedureName, CommandType.StoredProcedure))
            {
                if (dataReader != null && dataReader.HasRows)
                {
                    if (dataReader.Read())
                    {
                        user = new Users
                        {
                            UserId = dataReader.GetIntegerValue(AccountInfrastructure.UserIdColumnName),
                            Email = dataReader.GetStringValue(AccountInfrastructure.EmailColumnName),
                            FirstName = dataReader.GetStringValue(AccountInfrastructure.FirstNameColumnName),
                            LastName = dataReader.GetStringValue(AccountInfrastructure.LastNameColumnName),
                            PasswordHash = dataReader.GetStringValue(AccountInfrastructure.PasswordHashColumnName)

                        };
                    }
                    if (!dataReader.IsClosed)
                    {
                        dataReader.Close();
                    }

                }
            }


            return user;
        }

        public async Task<List<Users>> GetUsersList()
        {
            List<Users> userList = new List<Users>();
            Users user = new Users();

            var parameters = new List<DbParameter>
            {
                //base.GetParameter(AccountInfrastructure.UserIdParameterName, UserId)
            };

            using (var dataReader = await base.ExecuteReader(parameters, AccountInfrastructure.GetUsersListStoredProcedureName, CommandType.StoredProcedure))
            {
                if (dataReader != null && dataReader.HasRows)
                {
                    while (dataReader.Read())
                    {
                        user = new Users
                        {
                            UserId = dataReader.GetIntegerValue(AccountInfrastructure.UserIdColumnName),
                            Email = dataReader.GetStringValue(AccountInfrastructure.EmailColumnName),
                            FirstName = dataReader.GetStringValue(AccountInfrastructure.FirstNameColumnName),
                            LastName = dataReader.GetStringValue(AccountInfrastructure.LastNameColumnName),
                            PasswordHash = dataReader.GetStringValue(AccountInfrastructure.PasswordHashColumnName)

                        };

                        userList.Add(user);

                    }
                    if (!dataReader.IsClosed)
                    {
                        dataReader.Close();
                    }

                }
            }


            return userList;
        }

        public async Task<int> RegisterUser(Users users)
        {
            var applicationUserIdParamter = base.GetParameterOut(AccountInfrastructure.UserIdParameterName, SqlDbType.Int, users.UserId);

            var parameters = new List<DbParameter>
            {
                applicationUserIdParamter,
                base.GetParameter(AccountInfrastructure.EmailParameterName, users.Email),
                base.GetParameter(AccountInfrastructure.FirstNameParameterName, users.FirstName),
                base.GetParameter(AccountInfrastructure.LastNameParameterName, users.LastName),
                base.GetParameter(AccountInfrastructure.PasswordHashParameterName, users.PasswordHash),
                base.GetParameter(AccountInfrastructure.IdentificationNumberParameterName, users.IdentificationNumber),
                //base.GetParameter(AccountInfrastructure.PhoneNumberParameterName, users.PhoneNumber),
                base.GetParameter(AccountInfrastructure.Address1ParameterName, users.Address1),
                //base.GetParameter(AccountInfrastructure.PostalCodeParameterName, users.PostalCode)
            };
            await base.ExecuteNonQuery(parameters, AccountInfrastructure.RegisterUserStoredProcedureName, CommandType.StoredProcedure);
            users.UserId = Convert.ToInt32(applicationUserIdParamter.Value);

            return users.UserId;
        }

        public async Task<bool> UpdateUser(Users users)
        {
            var parameters = new List<DbParameter>
            {
                base.GetParameter(AccountInfrastructure.UserIdParameterName, users.UserId),
                base.GetParameter(AccountInfrastructure.EmailParameterName, users.Email),
                base.GetParameter(AccountInfrastructure.FirstNameParameterName, users.FirstName),
                base.GetParameter(AccountInfrastructure.LastNameParameterName, users.LastName)
            };
           
            var result  = await base.ExecuteNonQuery(parameters, AccountInfrastructure.UpdateUserStoredProcedureName, CommandType.StoredProcedure);
           

            return true;
        }

        public async Task<bool> PasswordReset(Users users)
        {
            var parameters = new List<DbParameter>
            {
                base.GetParameter(AccountInfrastructure.UserIdParameterName, users.UserId),
                base.GetParameter(AccountInfrastructure.PasswordHashParameterName, users.PasswordHash),
            };

            var result = await base.ExecuteNonQuery(parameters, AccountInfrastructure.PasswordResetStoredProcedureName, CommandType.StoredProcedure);


            return true;
        }



        #endregion
    }
}
