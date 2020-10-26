namespace Git.ViewModels.Repositories
{
    using System;

    public class RepositoryDetailsViewModel : RepositoryViewModel
    {
        public string Owner { get; set; }

        public DateTime CreatedOn { get; set; }

        public int CommitsCount { get; set; }
    }
}
