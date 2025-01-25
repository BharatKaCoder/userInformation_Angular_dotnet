using System.Linq.Expressions;
using AutoMapper;
using ICC_Champion_Trophy_2025.Model.DTO;
using ICC_Champion_Trophy_2025.Model;
using ICC_Champion_Trophy_2025.Repository.IRepository;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace ICC_Champion_Trophy_2025.Repository
{
    public class PlayerRepository : IPlayer
    {
        private readonly ApplicationDbContext _DBcontext;
        private readonly IMapper _mapper;
        public PlayerRepository(ApplicationDbContext dBcontext, IMapper mapper)
        {
            _DBcontext = dBcontext;
            _mapper = mapper;
        }

        public async Task<List<PlayerDTO>> GetPlayerList(Expression<Func<Players, bool>>? filter = null)
        {
            IQueryable<Players> query = _DBcontext.Players_new;
            if (filter != null)
            {
                query = query.Where(filter);
            }
            var players = await query.ToListAsync();
            var playerDTOList = _mapper.Map<List<PlayerDTO>>(players);
            return playerDTOList;
        }

        public async Task AddNewPlayer(Players entity)
        {
            await _DBcontext.Players_new.AddAsync(entity);
            await SaveAsync(entity);
        }

        public async Task<bool> DeletePlayerFromTeam(int id)
        {
            var player = await _DBcontext.Players_new.FindAsync(id);
            if (player == null)
            {
                return false;
            }
            _DBcontext.Players_new.Remove(player);
            await _DBcontext.SaveChangesAsync();
            return true;
        }

        public async Task<bool> UpdatePlayer(Players entity)
        {
            var player = await _DBcontext.Players_new.FindAsync(entity.Id);
            if (player == null)
            {
                return false;
            }
            try
            {
                await _DBcontext.SaveChangesAsync();
            }
            catch (DbUpdateException ex)
            {
                Console.WriteLine("Database update error", ex.Message);
                return false;
            }
            return true;
        }
        public async Task<Players> GetPlayerById(int id)
        {
            var player = await _DBcontext.Players_new.FindAsync(id);
            return player;
        }

        public async Task SaveAsync(Players entity)
        {
            await _DBcontext.SaveChangesAsync();
        }
    }
}
