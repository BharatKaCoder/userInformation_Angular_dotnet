using System.Linq.Expressions;
using ICC_Champion_Trophy_2025.Model;
using ICC_Champion_Trophy_2025.Model.DTO;

namespace ICC_Champion_Trophy_2025.Repository.IRepository
{
    public interface IPlayer
    {
        Task<List<PlayerDTO>> GetPlayerList(Expression<Func<Players, bool>>? filter=null);
        Task AddNewPlayer(Players entity);
        Task<bool> DeletePlayerFromTeam(int id);
        Task SaveAsync(Players entity);
    }
}
