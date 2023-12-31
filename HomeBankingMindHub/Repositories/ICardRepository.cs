﻿using HomeBankingMindHub.Models;
using System.Collections.Generic;

namespace HomeBankingMindHub.Repositories
{
    public interface ICardRepository
    {

        IEnumerable<Card> GetAllCards();
        void Save(Card card);
        Card FindById(long id);
    }
}
