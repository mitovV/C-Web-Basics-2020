namespace Git.Controllers
{
    using Services;
    using SUS.HTTP;
    using SUS.MvcFramework;
    using ViewModels.Commits;

    public class CommitsController : Controller
    {
        private readonly IRepositoriesService repositoriesService;
        private readonly ICommitsService commitsService;

        public CommitsController(IRepositoriesService repositoriesService, ICommitsService commitsService)
        {
            this.repositoriesService = repositoriesService;
            this.commitsService = commitsService;
        }

        public HttpResponse All()
        {
            if (!this.IsUserSignedIn())
            {
                return this.Redirect("/Users/Login");
            }

            var viewModel = this.commitsService.GetAll(this.GetUserId());

            return this.View(viewModel);
        }

        public HttpResponse Create(string id)
        {
            if (!this.IsUserSignedIn())
            {
                return Redirect("/Users/Login");
            }

            var viewModel = this.repositoriesService.GetRepository(id);

            return this.View(viewModel);
        }

        [HttpPost]
        public HttpResponse Create(CreateCommitInputModel model)
        {
            if (!this.IsUserSignedIn())
            {
                return Redirect("/Users/Login");
            }

            if (string.IsNullOrEmpty(model.Description) || model.Description.Length < 5)
            {
                return this.Error("Description must not be less than 5 characters.");
            }

            if (string.IsNullOrEmpty(model.Id))
            {
                return this.Error("Repository is required.");
            }

            this.commitsService.Create(model.Description, this.GetUserId(), model.Id);

            return this.Redirect("/Repositories/All");
        }

        public HttpResponse Delete(string id)
        {
            if (!this.IsUserSignedIn())
            {
                return Redirect("/Users/Login");
            }

            this.commitsService.Delete(id);

            return this.Redirect("/Commits/All");
        }
    }
}
