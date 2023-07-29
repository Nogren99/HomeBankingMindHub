using System;
using System.Linq;

namespace HomeBankingMindHub.Models
{
    public class DbInitializer
    {
        public static void Initialize(HomeBankingContext context)
        {
            if (!context.Clients.Any())
            {
                var clients = new Client[]
                {
                    new Client { Email = "juan@gmail.com", FirstName="Juan", LastName="Rodriguez", Password="123456"}
                };

                foreach (Client client in clients)
                {
                    context.Clients.Add(client);
                }

                //guardamos
                context.SaveChanges();
            }
            /*
            else
            {
                var clients = new Client[]
                {
                    new Client { Email = "victor@gmail.com", FirstName="Victor", LastName="Coronado", Password="123123123"}
                };

                foreach (Client client in clients)
                {
                    context.Clients.Add(client);
                }

                //guardamos
                context.SaveChanges();
            }*/


            if (!context.Account.Any())
            {
                var accountJuan = context.Clients.FirstOrDefault(c => c.Email == "juan@gmail.com");
                if (accountJuan != null)
                {
                    var accounts = new Account[]
                    {
                        new Account {ClientId = accountJuan.Id, CreationDate = DateTime.Now, Number = string.Empty, Balance = 1000000 }
                    };
                    foreach (Account account in accounts)
                    {
                        context.Account.Add(account);
                    }
                    context.SaveChanges();

                }
            }
            /*
            else
            {
                var account1 = context.Clients.FirstOrDefault(c => c.Email == "victor@gmail.com");
                if (account1 != null)
                {
                    var accounts = new Account[]
                    {
                        new Account {ClientId = account1.Id, CreationDate = DateTime.Now, Number = "VIN001", Balance = 10 }
                    };
                    foreach (Account account in accounts)
                    {
                        context.Account.Add(account);
                    }
                    context.SaveChanges();

                }
            }*/

            if (!context.Transactions.Any())

            {

                var account1 = context.Account.FirstOrDefault(c => c.Number == "VIN001");

                if (account1 != null)

                {

                    var transactions = new Transaction[]

                    {

                        new Transaction { AccountId= account1.Id, Amount = 10000, Date= DateTime.Now.AddHours(-5), Description = "Transferencia reccibida", Type = TransactionType.CREDIT.ToString() },

                        new Transaction { AccountId= account1.Id, Amount = -2000, Date= DateTime.Now.AddHours(-6), Description = "Compra en tienda mercado libre", Type = TransactionType.DEBIT.ToString() },

                        new Transaction { AccountId= account1.Id, Amount = -3000, Date= DateTime.Now.AddHours(-7), Description = "Compra en tienda xxxx", Type = TransactionType.DEBIT.ToString() },

                    };

                    foreach (Transaction transaction in transactions)

                    {

                        context.Transactions.Add(transaction);

                    }

                    context.SaveChanges();



                }

            }

        }
    }
}
