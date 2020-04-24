namespace Users.API.Persistence.Repositories
{
    using Microsoft.EntityFrameworkCore;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Users.API.Domain.Models;
    using Users.API.Domain.Repositories;
    using Users.API.Persistence.Contexts;

    public class UserRepository : BaseRepository, IUserRepository
    {
        public UserRepository(AppDbContext context) : base(context)
        {
        }

        public async Task<IEnumerable<User>> ListAsync()
        {
            return await context.Users.ToListAsync();
        }

        public async Task CreateAsync(User user)
        {
            await context.Users.AddAsync(user);
        }

    }
}
