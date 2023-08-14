using HomeBankingMindHub.dtos;
using HomeBankingMindHub.Models;
using HomeBankingMindHub.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System;
using System.Security.Principal;

namespace HomeBankingMindHub.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CardsController : ControllerBase
    {
        private ICardRepository _cardRepository;

        public CardsController(ICardRepository cardRepository)
        {
            _cardRepository = cardRepository;
        }


        [HttpGet]

        public IActionResult Get()
        {
            try
            {
                var cards = _cardRepository.GetAllCards();//getcardsbyclient
                var cardsDTO = new List<CardDTO>();
                foreach (Card card in cards)
                {
                    var newCardDTO = new CardDTO
                    {
                        Id = card.Id,
                        CardHolder = card.CardHolder,
                        Type = card.Type,
                        Color = card.Color,
                        Number = card.Number,
                        Cvv = card.Cvv,
                        FromDate = card.FromDate,
                        ThruDate = card.ThruDate,

                    };
                    cardsDTO.Add(newCardDTO);
                }
                return Ok(cardsDTO);
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
                var card = _cardRepository.FindById(id);
                if (card == null)
                {
                    return Forbid();
                }

                var newCardDTO = new CardDTO
                {
                    Id = card.Id,
                    CardHolder = card.CardHolder,
                    Type = card.Type,
                    Color = card.Color,
                    Number = card.Number,
                    Cvv = card.Cvv,
                    FromDate = card.FromDate,
                    ThruDate = card.ThruDate,

                };
                return Ok(newCardDTO);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }

        }

        [HttpPost]
        public CardDTO Post(String userName, long ClientId, String Type , String Color)
        {
            try
            {
                //Le creamos una nueva tarjeta al usuario
                Random random = new Random();
                int numbers1 = random.Next(1000, 9999); // Genera un número aleatorio de 4 dígitos
                int numbers2 = random.Next(1000, 9999);
                int numbers3 = random.Next(1000, 9999);
                int numbers4 = random.Next(1000, 9999);



                var card = new Card
                {
                    CardHolder = userName,
                    Type = Type,
                    Color = Color,
                    Number = numbers1.ToString() + "-" + numbers2.ToString() + "-" + numbers3.ToString() + "-" + numbers4.ToString() + "-",
                    Cvv = random.Next(100, 999),
                    FromDate = DateTime.Now,
                    ThruDate = DateTime.Now.AddYears(6),
                    ClientId = ClientId,
                };

                _cardRepository.Save(card);

                var newCardDTO = new CardDTO
                {
                    Id = card.Id,
                    CardHolder = card.CardHolder,
                    Type = card.Type,
                    Color = card.Color,
                    Number = card.Number,
                    Cvv = card.Cvv,
                    FromDate = card.FromDate,
                    ThruDate = card.ThruDate,

                };
              
                return newCardDTO;
            }
            catch
            {
                return null;
            }
        }











    }
}
