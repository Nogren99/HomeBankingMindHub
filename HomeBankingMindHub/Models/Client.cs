using System.Collections.Generic;

namespace HomeBankingMindHub.Models
{
    public class Client
    {
        public long Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public ICollection<Account> Accounts { get; set; }

        //atajo: prop tira un snipet con nombre y sus setters y getters
    }
}
