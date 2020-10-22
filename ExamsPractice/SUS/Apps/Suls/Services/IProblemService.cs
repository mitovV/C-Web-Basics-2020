namespace Suls.Services
{
    using System.Collections.Generic;

    using ViewModels.Problems;

    public interface IProblemService
    {
        void Create(string name, ushort points);

        ProblemViewModel GetById(string id);

        string GetNameById(string id);

        IEnumerable<HomePageProblemViewModel> GetAll();
    }
}
