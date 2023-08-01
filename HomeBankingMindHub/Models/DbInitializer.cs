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
                    new Client { Email = "juan@gmail.com", FirstName="Juan", LastName="Rodriguez", Password="123456"},
                    new Client { Email = "mario@gmail.com", FirstName="Mario", LastName="Perez", Password="12asd56"},
                    new Client { Email = "victor@gmail.com", FirstName="Victor", LastName="Coronado", Password="123123123"}
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
                var account1 = context.Clients.FirstOrDefault(c => c.Email == "victor@gmail.com");
                var account2 = context.Clients.FirstOrDefault(c => c.Email == "mario@gmail.com");


                var accounts = new Account[] { };

                if (accountJuan != null)
                {
                    accounts = accounts.Append(new Account { ClientId = accountJuan.Id, CreationDate = DateTime.Now, Number = "VIN000", Balance = 1000000 }).ToArray();
                }

                if (account1 != null)
                {
                    accounts = accounts.Append(new Account { ClientId = account1.Id, CreationDate = DateTime.Now, Number = "VIN001", Balance = 10 }).ToArray();
                }

                if (account2 != null)
                {
                    accounts = accounts.Append(new Account { ClientId = account2.Id, CreationDate = DateTime.Now, Number = "VIN002", Balance = 500 }).ToArray();
                }

                foreach (Account account in accounts)
                {
                    context.Account.Add(account);
                }
                context.SaveChanges();
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
                var account0 = context.Account.FirstOrDefault(c => c.Number == "VIN000");
                var account1 = context.Account.FirstOrDefault(c => c.Number == "VIN001");
                var account2 = context.Account.FirstOrDefault(c => c.Number == "VIN002");

                if (account0 != null)
                {
                    var transactions = new Transaction[]
                    {
                    new Transaction { AccountId = account0.Id, Amount = 100000, Date = DateTime.Now.AddHours(-5), Description = "Transferencia recibida", Type = TransactionType.CREDIT.ToString() },
                    new Transaction { AccountId = account0.Id, Amount = 20000, Date = DateTime.Now.AddHours(-6), Description = "Transferencia recibida", Type = TransactionType.DEBIT.ToString() },
                    new Transaction { AccountId = account0.Id, Amount = -3000, Date = DateTime.Now.AddHours(-7), Description = "Compra mercado libre", Type = TransactionType.DEBIT.ToString() },

                    };
                    foreach (Transaction transaction in transactions)
                    {
                        context.Transactions.Add(transaction);
                    }
                    context.SaveChanges();
                }

                if (account1 != null)
                {
                    var transactions = new Transaction[]
                    {
                        new Transaction { AccountId = account1.Id, Amount = 10000, Date = DateTime.Now.AddHours(-5), Description = "Transferencia recibida", Type = TransactionType.CREDIT.ToString() },
                        new Transaction { AccountId = account1.Id, Amount = -2000, Date = DateTime.Now.AddHours(-6), Description = "Compra en tienda mercado libre", Type = TransactionType.DEBIT.ToString() },
                        new Transaction { AccountId = account1.Id, Amount = -3000, Date = DateTime.Now.AddHours(-7), Description = "Compra en tienda xxxx", Type = TransactionType.DEBIT.ToString() },

                };
                    foreach (Transaction transaction in transactions)
                    {
                        context.Transactions.Add(transaction);
                    }
                    context.SaveChanges();
                }

                if (account2 != null)
                {
                    var transactions = new Transaction[]
                    {
                        new Transaction { AccountId= account2.Id, Amount = -100000, Date= DateTime.Now.AddHours(-5), Description = "Transferencia realizada", Type = TransactionType.CREDIT.ToString() },
                        new Transaction { AccountId= account2.Id, Amount = -20000, Date= DateTime.Now.AddHours(-6), Description = "Transferencia realizada", Type = TransactionType.DEBIT.ToString() },

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
