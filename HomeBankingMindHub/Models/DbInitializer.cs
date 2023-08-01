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

            if (!context.Loans.Any())
            {
                //crearemos 3 prestamos Hipotecario, Personal y Automotriz
                var loans = new Loan[]
                {
                    new Loan { Name = "Hipotecario", MaxAmount = 500000, Payments = "12,24,36,48,60" },
                    new Loan { Name = "Personal", MaxAmount = 100000, Payments = "6,12,24" },
                    new Loan { Name = "Automotriz", MaxAmount = 300000, Payments = "6,12,24,36" },
                };

                foreach (Loan loan in loans)
                {
                    context.Loans.Add(loan);
                }

                context.SaveChanges();

                //ahora agregaremos los clientloan (Prestamos del cliente)
                //usaremos un cliente y le agregaremos un préstamo de cada item
                var client1 = context.Clients.FirstOrDefault(c => c.Email == "victor@gmail.com");
                if (client1 != null)
                {
                    //ahora usaremos los 3 tipos de prestamos
                    var loan1 = context.Loans.FirstOrDefault(l => l.Name == "Hipotecario");
                    if (loan1 != null)
                    {
                        var clientLoan1 = new ClientLoan
                        {
                            Amount = 400000,
                            ClientId = client1.Id,
                            LoanId = loan1.Id,
                            Payments = "60"
                        };
                        context.ClientLoans.Add(clientLoan1);
                    }

                    var loan2 = context.Loans.FirstOrDefault(l => l.Name == "Personal");
                    if (loan2 != null)
                    {
                        var clientLoan2 = new ClientLoan
                        {
                            Amount = 50000,
                            ClientId = client1.Id,
                            LoanId = loan2.Id,
                            Payments = "12"
                        };
                        context.ClientLoans.Add(clientLoan2);
                    }

                    var loan3 = context.Loans.FirstOrDefault(l => l.Name == "Automotriz");
                    if (loan3 != null)
                    {
                        var clientLoan3 = new ClientLoan
                        {
                            Amount = 100000,
                            ClientId = client1.Id,
                            LoanId = loan3.Id,
                            Payments = "24"
                        };
                        context.ClientLoans.Add(clientLoan3);
                    }

                    //guardamos todos los prestamos
                    context.SaveChanges();


                    //Loans para otro client:
                    var client2 = context.Clients.FirstOrDefault(c => c.Email == "juan@gmail.com");
                    if (client2 != null)
                    {
                        //ahora usaremos los 3 tipos de prestamos
                        var loan4 = context.Loans.FirstOrDefault(l => l.Name == "Hipotecario");
                        if (loan4 != null)
                        {
                            var clientLoan1 = new ClientLoan
                            {
                                Amount = 400000,
                                ClientId = client2.Id,
                                LoanId = loan4.Id,
                                Payments = "60"
                            };
                            context.ClientLoans.Add(clientLoan1);
                        }

                        var loan5 = context.Loans.FirstOrDefault(l => l.Name == "Personal");
                        if (loan5 != null)
                        {
                            var clientLoan2 = new ClientLoan
                            {
                                Amount = 50000,
                                ClientId = client2.Id,
                                LoanId = loan5.Id,
                                Payments = "12"
                            };
                            context.ClientLoans.Add(clientLoan2);
                        }


                        //guardamos todos los prestamos
                        context.SaveChanges();

                    }

                }

            }











        }
    }
}
