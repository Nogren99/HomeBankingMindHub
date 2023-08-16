using HomeBankingMindHub.dtos;
using HomeBankingMindHub.Models;
using HomeBankingMindHub.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System;

namespace HomeBankingMindHub.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoansController : ControllerBase
    {

        private ILoanRepository _loanRepository;
        private IClientRepository _clientRepository;
        private IAccountRepository _accountRepository;
        private LoansController _loansController;
        private IClientLoanRepository _clientLoanRepository;
        private TransactionsController _transactionsController;
        public LoansController(ILoanRepository loanRepository, IClientLoanRepository clientLoanRepository , IClientRepository clientRepository , IAccountRepository accountRepository )
        {
            _loanRepository = loanRepository;
            _clientLoanRepository = clientLoanRepository;
            _clientRepository = clientRepository;
            _accountRepository = accountRepository;
        }


        [HttpGet]

        public IActionResult Get()
        {
            try
            {
                var loans = _loanRepository.GetAll();
                var loansDTO = new List<LoanDTO>();
                foreach (Loan loan in loans)
                {
                    var newLoanDTO = new LoanDTO
                    {
                        Id = loan.Id,
                        Name = loan.Name,
                        MaxAmount = loan.MaxAmount,
                        Payments = loan.Payments,  
                    };
                    loansDTO.Add(newLoanDTO);
                }
                return Ok(loansDTO);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpPost]

        public IActionResult Post([FromBody]LoanApplicationDTO loanAppDto)
        {
            try
            {
                string email = User.FindFirst("Client") != null ? User.FindFirst("Client").Value : string.Empty;
                if (email == string.Empty)
                {
                    return Forbid("Email vacio");
                }

                Client client = _clientRepository.FindByEmail(email);

                if (client == null)
                {
                    return Forbid("No existe el cliente");
                }

                var loan = _loanRepository.FindById(loanAppDto.LoanId);

                if (loan == null)
                {
                    return Forbid("No existe el prestamo");
                }

                if(loanAppDto.Amount == 0)
                {
                    return Forbid("El monto no puede ser 0");
                }

                var maxAutorizado = 99999; // ???
                if (loanAppDto.Amount > maxAutorizado)
                {
                    return Forbid("El monto super el maximo autorizado");
                }

                if (loanAppDto.Payments == null)
                {
                    Forbid("Las cuotas no pueden ser 0");
                }

                var account = _accountRepository.FinByNumber(loanAppDto.ToAccountNumber);

                if (account == null)
                {
                    Forbid("Cuenta destino inexistente");
                }

                if(account.ClientId != client.Id)
                {
                    return Forbid("La cuenta destino no pertenece al cliente");
                }




                return null;
            }
            catch (System.Exception)
            {

                throw;
            }
            
        }

    }
}
