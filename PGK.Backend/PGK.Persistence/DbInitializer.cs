namespace PGK.Persistence
{
    public class DbInitializer
    {
        public static void Initialize(PGKDbContext context)
        {
            //context.Database.EnsureDeleted();
            context.Database.EnsureCreated();
        }
    }
}
