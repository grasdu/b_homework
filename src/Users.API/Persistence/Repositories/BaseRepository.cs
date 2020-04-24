namespace Users.API.Persistence.Repositories
{
    using Users.API.Persistence.Contexts;
    public abstract class BaseRepository
    {
        protected readonly AppDbContext context;
        public BaseRepository(AppDbContext context)
        {
            this.context = context;
        }

    }
}
