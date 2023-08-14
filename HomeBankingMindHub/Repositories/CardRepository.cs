using HomeBankingMindHub.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;

namespace HomeBankingMindHub.Repositories
{
    public class CardRepository : RepositoryBase<Card>, ICardRepository
    {
        public CardRepository(HomeBankingContext repositoryContext) : base(repositoryContext)
        {
        }

        Card ICardRepository.FindById(long id)
        {
            return FindByCondition(card => card.Id == id)
                .FirstOrDefault();
        }

        IEnumerable<Card> ICardRepository.GetAllCards()
        {
            return FindAll()
                .ToList();
        }

        void ICardRepository.Save(Card card)
        {
            Create(card);
            SaveChanges();
        }
    }
}
