namespace BattleCards.Services
{
    using System.Collections.Generic;

    using ViewModels.Cards;

    public interface ICardsService
    {
        int Create(string name, string imageUrl, string keyword, int attack, int health, string description);

        void AddCardToUser(int cardId, string UserId);

        void RemoveCardFromUserCollection(string userId, int cardId);

        IEnumerable<CardViewModel> GetCardsByUserId(string id);

        IEnumerable<CardViewModel> GetAll();
    }
}
