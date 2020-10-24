namespace SharedTrip.Controllers
{
    using System;
    using System.Globalization;

    using Services;
    using SUS.HTTP;
    using SUS.MvcFramework;
    using ViewModels.Trips;

    public class TripsController : Controller
    {
        private readonly ITripsService tripsService;

        public TripsController(ITripsService tripsService)
        {
            this.tripsService = tripsService;
        }

        public HttpResponse All()
        {
            if (!this.IsUserSignedIn())
            {
                return this.Redirect("/Users/Login");
            }

            var viewModel = this.tripsService.AllTrips();
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
        public HttpResponse Add(TripInputModel model)
        {
            if (!this.IsUserSignedIn())
            {
                return this.Redirect("/Users/Login");
            }

            if (string.IsNullOrEmpty(model.StartPoint))
            {
                return this.Error("Start point is required");
            }

            if (string.IsNullOrEmpty(model.EndPoint))
            {
                return this.Error("End point is required");
            }

            DateTime date;

            if (!DateTime.TryParseExact(model.DepartureTime, "dd.MM.yyyy HH:mm", CultureInfo.InvariantCulture, DateTimeStyles.None, out date))
            {
                return this.Error("Departure time should be in \"dd.MM.yyyy HH: mm\" format.");
            }

            if (model.Seats < 2 || model.Seats > 6)
            {
                return this.Error("Sears should be between 2 and 6.");
            }

            if (string.IsNullOrEmpty(model.Description) || model.Description.Length > 80)
            {
                return this.Error("Description is required and should be below 80 characters.");
            }

            this.tripsService.Add(model.StartPoint, model.EndPoint, date, model.ImagePath, model.Seats, model.Description);
            return this.Redirect("/Trips/All");
        }

        public HttpResponse Details(string tripId)
        {
            if (!this.IsUserSignedIn())
            {
                return this.Redirect("/Users/Login");
            }

            var viewModel = this.tripsService.GetById(tripId);

            return this.View(viewModel);
        }

        public HttpResponse AddUserToTrip(string tripId)
        {
            if (!this.IsUserSignedIn())
            {
                return Redirect("/Users/Login");
            }

            if (!this.tripsService.HasAvailableSeats(tripId))
            {
                return this.Error("No seats available.");
            }

            if (!this.tripsService.AddUserToTrip(this.GetUserId(), tripId))
            {
                return this.Redirect("/Trips/Details?tripId=" + tripId);
            }

            return this.Redirect("/Trips/All");
        }
    }
}
