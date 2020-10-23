namespace SharedTrip.ViewModels.Trips
{
    using System;

    public class TripViewModel
    {
        public string Id { get; set; }

        public string StartPonit { get; set; }

        public string EndPoint { get; set; }

        public DateTime DepartureTime { get; set; }

        public byte Seats { get; set; }
    }
}
