namespace Git.Services
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    using Data;
    using ViewModels.Commits;

    public class CommitsService : ICommitsService
    {
        private readonly ApplicationDbContext db;

        public CommitsService(ApplicationDbContext db)
        {
            this.db = db;
        }

        public void Create(string description, string creatorId, string repositoryId)
        {
            var commit = new Commit
            {
                Description = description,
                CreatorId = creatorId,
                RepositoryId = repositoryId,
                CreatedOn = DateTime.UtcNow,
            };

            this.db.Commits.Add(commit);
            this.db.SaveChanges();
        }

        public void Delete(string id)
        {
            var commit = this.db.Commits.Find(id);

            this.db.Commits.Remove(commit);
            this.db.SaveChanges();
        }

        public IEnumerable<CommitDetailsViewModel> GetAll(string userId)
            => this.db.Commits.Where(x => x.CreatorId == userId).Select(x => new CommitDetailsViewModel
            {
                CreatedOn = x.CreatedOn,
                Id = x.Id,
                Description = x.Description,
                Name = x.Repository.Name
            })
            .ToList();
    }
}
