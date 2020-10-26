namespace Git.Services
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    using Data;
    using ViewModels.Repositories;

    public class RepositoriesService : IRepositoriesService
    {
        private readonly ApplicationDbContext db;

        public RepositoriesService(ApplicationDbContext db)
        {
            this.db = db;
        }

        public void Create(string name, string type, string userId)
        {
            var repository = new Repository
            {
                Name = name,
                IsPublic = type == "Public" ? true : false,
                CreatedOn = DateTime.UtcNow,
                OwnerId = userId,
            };

            this.db.Repositories.Add(repository);
            this.db.SaveChanges();
        }

        public IEnumerable<RepositoryDetailsViewModel> GetAll()
            => this.db.Repositories
            .Where(x => x.IsPublic == true)
            .Select(x => new RepositoryDetailsViewModel
            {
                Id = x.Id,
                Name = x.Name,
                Owner = x.Owner.Username,
                CreatedOn = x.CreatedOn,
                CommitsCount = x.Commits.Count
            })
            .ToList();

        public RepositoryViewModel GetRepository(string id)
            => this.db.Repositories
            .Where(x => x.Id == id)
            .Select(x => new RepositoryViewModel 
            { 
                Id = x.Id,
                Name = x.Name
            })
            .FirstOrDefault();
    }
}
