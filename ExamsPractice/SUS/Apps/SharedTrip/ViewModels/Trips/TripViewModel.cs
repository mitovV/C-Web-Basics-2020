namespace SharedTrip.ViewModels.Trips
{
    using System;
    using System.Globalization;

    public class TripViewModel
    {
        public string Id { get; set; }

        public string StartPoint { get; set; }

        public string EndPoint { get; set; }

        public DateTime DepartureTime { get; set; }

        public string DepartureTimeAsString => DepartureTime.ToString(CultureInfo.GetCultureInfo("BG-bg"));

        public byte Seats { get; set; }

        public int TakenSeats { get; set; }

        public int AvailableSeats => this.Seats - this.TakenSeats;
    }
}
