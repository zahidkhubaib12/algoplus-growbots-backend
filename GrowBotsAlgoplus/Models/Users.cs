using System;
using System.ComponentModel.DataAnnotations;

namespace GrowBotsAlgoplus.Models
{
    public class Users
    {        
        public Guid Id { get; set; }        
        public int UserId { get; set; }
        [Required][EmailAddress] public string Email { get; set; }
        [Required] public string FirstName { get; set; }
        public string LastName { get; set; }
        public string IdentificationNumber { get; set; }
        public string Address1 { get; set; }
        public string PostalCode { get; set; }
        public string PasswordHash { get; set; }
        [Required] public string PhoneNumber { get; set; }
        public bool Active { get; set; }
    }
}
