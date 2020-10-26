namespace Git.Services
{
    using System.Collections.Generic;

    using ViewModels.Repositories;

    public interface IRepositoriesService
    {
        void Create(string name, string type, string userId);

        IEnumerable<RepositoryDetailsViewModel> GetAll();

        RepositoryViewModel GetRepository(string id);
    }
}
