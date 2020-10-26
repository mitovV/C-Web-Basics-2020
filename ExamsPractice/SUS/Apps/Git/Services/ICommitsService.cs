namespace Git.Services
{
    using System.Collections.Generic;

    using ViewModels.Commits;

    public interface ICommitsService
    {
        void Create(string description, string creatorId, string repositoryId);

        void Delete(string id);

        IEnumerable<CommitDetailsViewModel> GetAll(string userId);
    }
}
