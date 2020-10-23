namespace BattleCards.Services
{
    using System.Collections.Generic;
    using System.Linq;

    using Data;
    using Models;
    using ViewModels.Cards;

    public class CardsService : ICardsService
    {
        private readonly ApplicationDbContext db;

        public CardsService(ApplicationDbContext db)
        {
            this.db = db;
        }

        public void AddCardToUser(int cardId, string userId)
        {
            if (this.db.UserCards.Any(x=> x.CardId == cardId && x.UserId == userId))
            {
                return;
            }

            var userCard = new UserCard
            {
                CardId = cardId,
                UserId = userId
            };

            this.db.UserCards.Add(userCard);
            this.db.SaveChanges();
        }

        public int Create(string name, string imageUrl, string keyword, int attack, int health, string description)
        {
            var card = new Card
            {
                Name = name,
                ImageUrl = imageUrl,
                Keyword = keyword,
                Attack = attack,
                Health = health,
                Description = description
            };

            this.db.Cards.Add(card);
            this.db.SaveChanges();

            return card.Id;
        }

        public IEnumerable<CardViewModel> GetAll()
            => this.db.Cards.Select(x => new CardViewModel
            {
                Id = x.Id,
                Name = x.Name,
                Attack = x.Attack,
                Health = x.Health,
                Description = x.Description,
                Image = x.ImageUrl,
                Keyword = x.Keyword
            })
            .ToList();

        public IEnumerable<CardViewModel> GetCardsByUserId(string id)
            => this.db.UserCards
                .Where(x => x.UserId == id)
                .Select(x => new CardViewModel
                {
                    Id = x.Card.Id,
                    Name = x.Card.Name,
                    Attack = x.Card.Attack,
                    Description = x.Card.Description,
                    Health = x.Card.Health,
                    Image = x.Card.ImageUrl,
                    Keyword = x.Card.Keyword
                })
                .ToList();

        public void RemoveCardFromUserCollection(string userId, int cardId)
        {
            var userCard = this.db.UserCards
                .Where(x => x.UserId == userId && x.CardId == cardId)
                .FirstOrDefault();

            if (userCard == null)
            {
                return;
            }

            this.db.UserCards.Remove(userCard);
            this.db.SaveChanges();
        }
    }
}
