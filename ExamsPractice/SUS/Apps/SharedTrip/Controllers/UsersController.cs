namespace SharedTrip.Controllers
{
    using SUS.HTTP;
    using SUS.MvcFramework;

    public class UsersController : Controller
    {
        public HttpResponse Login()
        {
            return this.View();
        }

        public HttpResponse Register()
        {
            return this.View();
        }
    }
}
