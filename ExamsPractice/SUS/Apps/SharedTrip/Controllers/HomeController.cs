namespace SharedTrip.Controllers
{
    using SUS.HTTP;
    using SUS.MvcFramework;

    public class HomeController : Controller
    {
        [HttpGet("/")]
        public HttpResponse Index()
        {
            if (!this.IsUserSignedIn())
            {
                //return this.Redirect();
            }

            return this.View();
        }
    }
}
