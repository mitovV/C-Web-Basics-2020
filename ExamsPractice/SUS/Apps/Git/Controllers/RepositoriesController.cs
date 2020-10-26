namespace Git.Controllers
{
    using Services;
    using SUS.HTTP;
    using SUS.MvcFramework;
    using ViewModels.Repositories;

    public class RepositoriesController : Controller
    {
        private readonly IRepositoriesService repositoriesService;

        public RepositoriesController(IRepositoriesService repositoriesService)
        {
            this.repositoriesService = repositoriesService;
        }

        public HttpResponse All()
        {
            var viewModel = this.repositoriesService.GetAll();

            return this.View(viewModel);
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
        public HttpResponse Create(CreateRepositoriesInputModel model)
        {
            if (!this.IsUserSignedIn())
            {
                return this.Redirect("/Users/Login");
            }

            if (string.IsNullOrEmpty(model.Name) || model.Name.Length < 3 || model.Name.Length > 10)
            {
                return this.Error("Name should be between 3 and 10 characters.");
            }

            if (string.IsNullOrEmpty(model.RepositoryType))
            {
                return this.Error("Repository type is required.");
            }

            this.repositoriesService.Create(model.Name, model.RepositoryType, this.GetUserId());

            return Redirect("/Repositories/All");
        }
    }
}
