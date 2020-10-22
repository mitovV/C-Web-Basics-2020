namespace BattleCards
{
    using System.Threading.Tasks;

    using SUS.MvcFramework;

    public static class Program
    {
        public static async Task Main()
        {
            await Host.CreateHostAsync(new Startup());
        }
    }
}
