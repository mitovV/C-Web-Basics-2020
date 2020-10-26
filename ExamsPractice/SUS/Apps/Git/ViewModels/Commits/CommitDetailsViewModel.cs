namespace Git.ViewModels.Commits
{
    using System;

    public class CommitDetailsViewModel
    {
        public string Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public DateTime CreatedOn { get; set; }
    }
}
