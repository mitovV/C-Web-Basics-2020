namespace SharedTrip.Services
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    using Data;
    using ViewModels.Trips;

    public class TripsService : ITripsService
    {
        private readonly ApplicationDbContext db;

        public TripsService(ApplicationDbContext db)
        {
            this.db = db;
        }

        public void Add(string startingPoint, string endPoint, DateTime departureTime, string carImage, byte seats, string description)
        {
            var trip = new Trip
            {
                StartPoint = startingPoint,
                EndPoint = endPoint,
                DepartureTime = departureTime,
                ImagePath = carImage,
                Seats = seats,
                Description = description,
            };

            this.db.Trips.Add(trip);
            this.db.SaveChanges();
        }

        public bool AddUserToTrip(string userId, string tripId)
        {
            var userTrip = this.db.UserTrips.Where(x => x.UserId == userId && x.TripId == tripId).FirstOrDefault();

            if (userTrip == null)
            {
                userTrip = new UserTrip
                {
                    UserId = userId,
                    TripId = tripId
                };

                this.db.UserTrips.Add(userTrip);
                this.db.SaveChanges();
            }

            return true;
        }

        public IEnumerable<TripViewModel> AllTrips()
         => this.db.Trips.Select(x => new TripViewModel
         {
             Id = x.Id,
             DepartureTime = x.DepartureTime,
             StartPoint = x.StartPoint,
             EndPoint = x.EndPoint,
             Seats = x.Seats
         })
            .ToList();

        public TripDetailsViewModel GetById(string id)
            => this.db.Trips
            .Where(x => x.Id == id)
            .Select(x => new TripDetailsViewModel
            {
                Id = x.Id,
                StartPoint = x.StartPoint,
                EndPoint = x.EndPoint,
                DepartureTime = x.DepartureTime,
                Seats = x.Seats,
                Description = x.Description,
                ImagePath = x.ImagePath
            })
            .FirstOrDefault();
    }
}
