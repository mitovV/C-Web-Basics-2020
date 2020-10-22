namespace Suls.Services
{
    using System.Collections.Generic;
    using System.Linq;

    using Data;
    using ViewModels.Problems;
    using ViewModels.Submissions;

    public class ProblemService : IProblemService
    {
        private readonly SulsDbContext db;

        public ProblemService(SulsDbContext db)
        {
            this.db = db;
        }

        public IEnumerable<HomePageProblemViewModel> GetAll()
         => this.db.Problems.Select(x => new HomePageProblemViewModel
                {
                    Id = x.Id,
                    Name = x.Name,
                    Count = x.Submissions.Count()
                })
                .ToList();

        public void Create(string name, ushort points)
        {
            var problem = new Problem
            {
                Name = name,
                Points = points
            };

            this.db.Problems.Add(problem);
            this.db.SaveChanges();
        }

        public ProblemViewModel GetById(string id)
            => this.db.Problems
                .Where(x => x.Id == id)
                .Select(x => new ProblemViewModel
                {
                    Name = x.Name,
                    Submissions = x.Submissions.Select(s => new SubmissionViewModel
                    {
                        Username = s.User.Username,
                        AchievedResult = s.AchievedResult,
                        CreatedOn = s.CreatedOn,
                        MaxPoints = x.Points,
                        SubmissionId = s.Id
                    })
                    .ToList()
                })
                .FirstOrDefault();

        public string GetNameById(string id)
            => this.db.Problems
            .Where(x => x.Id == id)
            .Select(x => x.Name)
            .FirstOrDefault();
    }
}
