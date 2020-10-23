namespace SharedTrip.Services
{
    using System;

    using Data;

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
    }
}
