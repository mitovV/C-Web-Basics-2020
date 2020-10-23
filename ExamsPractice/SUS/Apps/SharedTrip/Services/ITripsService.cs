namespace SharedTrip.Services
{
    using System;
    using System.Collections.Generic;

    using ViewModels.Trips;

    public interface ITripsService
    {
        void Add(string startingPoint, string endPoint, DateTime departureTime, string carImage, byte seats, string description);

        IEnumerable<TripViewModel> AllTrips();

        TripDetailsViewModel GetById(string id);

        bool AddUserToTrip(string userId, string tripId);
    }
}
