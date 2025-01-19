using System.Linq;
using System.Linq.Expressions;
using AutoMapper;
using ICC_Champion_Trophy_2025.Model;
using ICC_Champion_Trophy_2025.Model.DTO;
using ICC_Champion_Trophy_2025.Repository.IRepository;
using Microsoft.EntityFrameworkCore;

namespace ICC_Champion_Trophy_2025.Repository
{
    public class TeamRepository : ITeamRepository
    {
        private readonly ApplicationDbContext _DBcontext;
        private readonly IMapper _mapper;
        public TeamRepository(ApplicationDbContext dbContext, IMapper mapper)
        {
            _DBcontext = dbContext;
            _mapper = mapper;
        }
        public async Task<List<TeamsDTO>> GetAllTeams(Expression<Func<Teams, bool>> filter=null)
        {
            IQueryable<Teams> query = _DBcontext.Teams;
            // Apply the filter if provided
            if (filter != null)
            {
                query = query.Where(filter);
            }
            var teamList = await query.ToListAsync();
            var teamDTOList = _mapper.Map<List<TeamsDTO>>(teamList);
            return teamDTOList;
        }
    }
}
