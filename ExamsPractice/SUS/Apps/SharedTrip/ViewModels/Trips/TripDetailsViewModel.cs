namespace SharedTrip.ViewModels.Trips
{
    public class TripDetailsViewModel : TripViewModel
    {
        public string ImagePath { get; set; }

        public string Description { get; set; }

        public string FormatedDate => this.DepartureTime.ToString("s");
    }
}
