namespace Users.API.Persistence.Repositories
{
    using System.Threading.Tasks;
    using Users.API.Persistence.Contexts;

    public class UnitOfWork : IUnitOfWork
    {
        private readonly AppDbContext context;

        public UnitOfWork(AppDbContext context)
        {
            this.context = context;
        }

        public async Task CompleteAsync()
        {
            await this.context.SaveChangesAsync();
        }
    }
}

