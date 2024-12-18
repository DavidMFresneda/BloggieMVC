using Bloggie.Web.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Bloggie.Web.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly AuthDbContext _authDbContext;

        public UserRepository(AuthDbContext authDbContext)
        {
            this._authDbContext = authDbContext;
        }

        public async Task<IEnumerable<IdentityUser>> GetAll()
        {
            var users = await _authDbContext.Users.ToListAsync();

            //No mostramos el superAdmin
            users = users.Where(u => u.Id != "accd64f2-2690-43c0-a96a-fedbf83e9232").ToList();

            return users;
        }

        public Task<IdentityUser> CreateUser(IdentityUser newUser)
        {

            throw new NotImplementedException();
        }

    }
}
