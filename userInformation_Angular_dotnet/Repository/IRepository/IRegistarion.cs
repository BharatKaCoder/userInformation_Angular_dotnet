using System.Linq.Expressions;
using userInformation_Angular_dotnet.Models;

namespace userInformation_Angular_dotnet.Repository.IRepository
{
    public interface IRegistration
    {
        Task<List<Registration>> GetAllUserListAsync(Expression<Func<Registration, bool>>? filter = null);
        Task CreateNewUser(Registration entity);
        Task<Registration> GetUserById(Guid id);
        Task DeleteUser(Guid id);
        Task UpdateUser(Registration entity);
        Task SaveAsync(Registration entity);
    }
}
