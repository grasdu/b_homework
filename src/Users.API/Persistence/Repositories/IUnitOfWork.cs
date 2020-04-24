namespace Users.API.Persistence.Repositories
{
    using System.Threading.Tasks;
    public interface IUnitOfWork
    {
        Task CompleteAsync();
    }
}
