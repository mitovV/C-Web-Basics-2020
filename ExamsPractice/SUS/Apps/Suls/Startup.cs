namespace Suls
{
    using System.Collections.Generic;

    using Data;
    using Services;
    using SUS.HTTP;
    using SUS.MvcFramework;

    using Microsoft.EntityFrameworkCore;

    public class Startup : IMvcApplication
    {
        public void Configure(List<Route> routeTable)
        {
            new SulsDbContext().Database.Migrate();
        }

        public void ConfigureServices(IServiceCollection serviceCollection)
        {
            serviceCollection.Add<IUsersService, UsersService>();
            serviceCollection.Add<IProblemService, ProblemService>();
            serviceCollection.Add<ISubmissionsService, SubmissionsService>();
        }
    }
}
