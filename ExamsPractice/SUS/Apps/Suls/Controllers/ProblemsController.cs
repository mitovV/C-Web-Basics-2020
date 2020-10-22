namespace Suls.Controllers
{
    using Services;
    using SUS.HTTP;
    using SUS.MvcFramework;
    using ViewModels.Problems;

    public class ProblemsController : Controller
    {
        private readonly IProblemService problemService;

        public ProblemsController(IProblemService problemService)
        {
            this.problemService = problemService;
        }

        public HttpResponse Create()
        {
            if (!this.IsUserSignedIn())
            {
                return this.Redirect("/Users/Login");
            }

            return this.View();
        }

        [HttpPost]
        public HttpResponse Create(CrateProblemInputModel model)
        {
            if (string.IsNullOrEmpty(model.Name) || model.Name.Length < 5 || model.Name.Length > 20)
            {
                return this.Error("Name is required and should be between 5 and 20 characters.");
            }

            if (model.Points < 50 || model.Points > 300 )
            {
                return this.Error("Total Points should be between 50 and 300");
            }

            this.problemService.Create(model.Name, model.Points);
            return this.Redirect("/");
        }

        public HttpResponse Details(string id)
        {
            if (!this.IsUserSignedIn())
            {
                return this.Redirect("/Users/Login");
            }

            var problem = this.problemService.GetById(id);
            return this.View(problem);
        }
    }
}
