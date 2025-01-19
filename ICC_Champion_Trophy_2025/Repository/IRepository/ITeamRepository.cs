using System.Linq.Expressions;
using ICC_Champion_Trophy_2025.Model;
using ICC_Champion_Trophy_2025.Model.DTO;

namespace ICC_Champion_Trophy_2025.Repository.IRepository
{
    public interface ITeamRepository
    {
        Task<List<TeamsDTO>>GetAllTeams(Expression<Func<Teams, bool>> filter = null);
    }
}
