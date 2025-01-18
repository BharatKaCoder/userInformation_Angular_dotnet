using System.Linq.Expressions;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using userInformation_Angular_dotnet.Models;
using userInformation_Angular_dotnet.Repository.IRepository;

namespace userInformation_Angular_dotnet.Repository
{
    public class RegistartionRepo : IRegistration
    {
        private readonly ApplicationDbContext _DbContext;
        private readonly IMapper _mapper;

        public RegistartionRepo(ApplicationDbContext dbContext, IMapper mapper)
        {
            _DbContext = dbContext;
        }
        public async Task CreateNewUser(Registration entity)
        {
            await _DbContext.UserListTable.AddAsync(entity);
            await SaveAsync(entity);
        }

        public async Task<bool> DeleteUserAsync(int id)
        {
            var user = await _DbContext.UserListTable.FindAsync(id);
            if (user == null)
            {
                return false; // User not found
            }

            _DbContext.UserListTable.Remove(user);
            await _DbContext.SaveChangesAsync(); // Persist changes to DB
            return true; // Successfully deleted
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


        public async Task<Registration> GetUserById(int id)
        {
            var user = await _DbContext.UserListTable.FindAsync(id);
            return user; // Return user if found, otherwise null
        }

        public async Task SaveAsync(Registration entity)
        {
           await _DbContext.SaveChangesAsync();
        }

        public async Task<bool> UpdateUserAsync(Registration entity)
        {
            // Find user by ID
            var user = await _DbContext.UserListTable.FindAsync(entity.Id);
            if (user == null)
            {
                return false; // User not found
            }
            // Save changes to database
            try
            {
                await _DbContext.SaveChangesAsync();
            }
            catch (DbUpdateException ex)
            {
                // Log exception details for troubleshooting
                Console.WriteLine($"Database update error: {ex.Message}");
                return false; // Indicate failure in saving changes
            }

            return true; // Indicate success
        }


    }
}
