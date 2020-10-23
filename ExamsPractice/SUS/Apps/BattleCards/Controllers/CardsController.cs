namespace BattleCards.Controllers
{
    using System;

    using Services;
    using SUS.HTTP;
    using SUS.MvcFramework;
    using ViewModels.Cards;

    public class CardsController : Controller
    {
        private readonly ICardsService cardsService;

        public CardsController(ICardsService cardsService)
        {
            this.cardsService = cardsService;
        }

        public HttpResponse All()
        {
            if (!this.IsUserSignedIn())
            {
                return this.Redirect("/Users/Login");
            }

            var viewModel = this.cardsService.GetAll();

            return this.View(viewModel);
        }

        public HttpResponse Add()
        {
            if (!this.IsUserSignedIn())
            {
                return this.Redirect("/Users/Login"); 
            }

            return this.View();
        }

        [HttpPost]
        public HttpResponse Add(CreateCardInputModel model)
        {
            if (!this.IsUserSignedIn())
            {
                return this.Redirect("/Users/Login");
            }

            if (string.IsNullOrEmpty(model.Name) || model.Name.Length < 5 || model.Name.Length > 15)
            {
                return this.Error("Name should be between 5 and 15 characters long.");
            }

            if (string.IsNullOrEmpty(model.Image))
            {
                return this.Error("The image is required!");
            }

            if (!Uri.TryCreate(model.Image, UriKind.Absolute, out _))
            {
                return this.Error("Image url should be valid.");
            }

            if (string.IsNullOrEmpty(model.Keyword))
            {
                return this.Error("Keyword is required.");
            }

            if (model.Attack < 0 )
            {
                return this.Error("Attack should be non-negative integer.");
            }

            if (model.Health < 0)
            {
                return this.Error("Health should be non-negative integer.");
            }

            if (string.IsNullOrEmpty(model.Description) || model.Description.Length > 200)
            {
                return this.Error("Description is required and its length should be at most 200 characters.");
            }

            var userId = this.GetUserId();
            var cardId = this.cardsService.Create(model.Name,model.Image, model.Keyword, model.Attack, model.Health,model.Description);

            this.cardsService.AddCardToUser(cardId, userId);

            return this.Redirect("/Cards/All");
        }

        public HttpResponse Collection()
        {
            if (!this.IsUserSignedIn())
            {
                return this.Redirect("/Users/Login");
            }

            var viewModel = this.cardsService.GetCardsByUserId(this.GetUserId());

            return this.View(viewModel);
        }

        public HttpResponse AddToCollection(int cardId)
        {
            if (!this.IsUserSignedIn())
            {
                return Redirect("/Users/Login");
            }

            this.cardsService.AddCardToUser(cardId, this.GetUserId());
            return this.Redirect("/Cards/All");
        }

        public HttpResponse RemoveFromCollection(int cardId)
        {
            if (!this.IsUserSignedIn())
            {
                return this.Redirect("/Users/Login");
            }

            this.cardsService.RemoveCardFromUserCollection(this.GetUserId(), cardId);

            return this.Redirect("/Cards/Collection");
        }
    }
}
