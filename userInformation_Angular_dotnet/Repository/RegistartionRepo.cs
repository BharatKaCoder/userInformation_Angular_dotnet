using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using userInformation_Angular_dotnet.Models;
using userInformation_Angular_dotnet.Repository.IRepository;

namespace userInformation_Angular_dotnet.Repository
{
    public class RegistartionRepo : IRegistration
    {
        private readonly ApplicationDbContext _DbContext;

        public RegistartionRepo(ApplicationDbContext dbContext)
        {
            _DbContext = dbContext;
        }
        public async Task CreateNewUser(Registration entity)
        {
            await _DbContext.UserListTable.AddAsync(entity);
            await SaveAsync(entity);
        }

        public Task DeleteUser(Guid id)
        {
            throw new NotImplementedException();
        }

        public async Task<List<Registration>> GetAllUserListAsync(Expression<Func<Registration, bool>>? filter = null)
        {
            IQueryable<Registration> query = _DbContext.UserListTable;

            if (filter != null)
            {
                query = query.Where(filter);
            }

            return await query.ToListAsync();
        }


        public Task<Registration> GetUserById(Guid id)
        {
            throw new NotImplementedException();
        }

        public async Task SaveAsync(Registration entity)
        {
           await _DbContext.SaveChangesAsync();
        }

        public Task UpdateUser(Registration entit)
        {
            throw new NotImplementedException();
        }
    }
}
