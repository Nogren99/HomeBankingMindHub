using HomeBankingMindHub.dtos;
using HomeBankingMindHub.Models;
using HomeBankingMindHub.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System;
using System.Linq;
using System.Net;

namespace HomeBankingMindHub.Controllers
{
    [Route("api/[controller]")]
    [ApiController]

    public class AccountsController : ControllerBase

    {
        private IAccountRepository _accountRepository;
        public AccountsController(IAccountRepository accountRepository)
        {
            _accountRepository = accountRepository;
        }

        [HttpGet]

        public IActionResult Get()
        {
            try
            {
                var accounts = _accountRepository.GetAllAccounts();
                var accountsDTO = new List<AccountDTO>();
                foreach (Account account in accounts)
                {
                    var newAccountDTO = new AccountDTO
                    {
                        Id = account.Id,
                        Number = account.Number,
                        CreationDate = account.CreationDate,
                        Balance = account.Balance,

                        Transactions = account.Transactions.Select(t => new TransactionDTO
                        {
                            Id = t.Id,
                            description = t.Description,
                            Date = t.Date,
                            Type = t.Type,
                            Amount = t.Amount,

                        }).ToList(),

                    };
                    accountsDTO.Add(newAccountDTO);
                }
                return Ok(accountsDTO);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }



        [HttpGet("{id}")]

        public IActionResult Get(long id)

        {
            try
            {
                var account = _accountRepository.FindById(id);
                if (account == null)
                {
                    return Forbid();
                }

                var accountDTO = new AccountDTO

                {
                    Id = account.Id,
                    Number = account.Number,
                    CreationDate = account.CreationDate,
                    Balance = account.Balance,

                    Transactions = account.Transactions.Select(t => new TransactionDTO
                    {
                        Id = t.Id,
                        description= t.Description,
                        Date = t.Date,
                        Type = t.Type,
                        Amount = t.Amount,

                    }).ToList(),

                };
                return Ok(accountDTO);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }

        }

        [HttpGet()]

        public IActionResult GetByClient(long id)

        {
            try
            {
                var accounts = _accountRepository.GetAccountsByClient(id);
                if (accounts == null)
                {
                    return Forbid();
                }

                var accountsDTO = new List <AccountDTO>();
                

                foreach (Account account in accounts)
                {
                    var accountDTO = new AccountDTO
                    {
                        Id = account.Id,
                        Number = account.Number,
                        CreationDate = account.CreationDate,
                        Balance = account.Balance,

                        Transactions = account.Transactions.Select(t => new TransactionDTO
                        {
                            Id = t.Id,
                            description = t.Description,
                            Date = t.Date,
                            Type = t.Type,
                            Amount = t.Amount,

                        }).ToList()

                    };

                    accountsDTO.Add(accountDTO);

                }

                
                return Ok(accountsDTO);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }

        }

        [HttpPost]
        public AccountDTO Post(long clientId)
        {
            try
            {
                //Le creamos una nueva cuenta al usuario
                Random random = new Random();
                int numeroAleatorio = random.Next(10000000, 99999999); // Genera un número aleatorio de 8 dígitos


                Account newAccount = new Account
                {
                    Number = "VIN-" + numeroAleatorio.ToString(),
                    CreationDate = DateTime.Now,
                    Balance = 0,
                    ClientId = clientId,
                };

                _accountRepository.Save(newAccount);

                AccountDTO newAccDTO = new AccountDTO
                {
                    Id = newAccount.Id,
                    Balance = newAccount.Balance,
                    CreationDate = newAccount.CreationDate,
                    Number = newAccount.Number
                };
                return newAccDTO;
            }
            catch {
                return null;
            }
        }
        

    }
}
