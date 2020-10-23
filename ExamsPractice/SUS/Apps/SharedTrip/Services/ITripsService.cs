namespace SharedTrip.Services
{
    using System;

    public interface ITripsService
    {
        void Add(string startingPoint, string endPoint, DateTime departureTime, string carImage, byte seats, string description);
    }
}
