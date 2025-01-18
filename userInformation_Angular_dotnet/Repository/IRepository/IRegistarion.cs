using System.Linq.Expressions;
using userInformation_Angular_dotnet.Models;

namespace userInformation_Angular_dotnet.Repository.IRepository
{
    public interface IRegistration
    {
        Task<List<Registration>> GetAllUserListAsync(Expression<Func<Registration, bool>>? filter = null);
        Task CreateNewUser(Registration entity);
        Task<Registration> GetUserById(int id);
        Task<bool> DeleteUserAsync(int id);
        Task<bool> UpdateUserAsync(Registration entity);
        Task SaveAsync(Registration entity);
    }
}
