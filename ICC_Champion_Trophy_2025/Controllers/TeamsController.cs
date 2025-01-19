using AutoMapper;
using ICC_Champion_Trophy_2025.Model;
using ICC_Champion_Trophy_2025.Repository.IRepository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ICC_Champion_Trophy_2025.Controllers
{
    [Route("api/teams")]
    [ApiController]
    public class TeamsController : ControllerBase
    {
        private readonly ApplicationDbContext _DBcontext;
        private readonly IMapper _mapper;
        private readonly ITeamRepository _teamRepository;
        public TeamsController(ApplicationDbContext dbContext, IMapper mapper, ITeamRepository teamRepository)
        {
            _DBcontext = dbContext;
            _mapper = mapper;
            _teamRepository = teamRepository;
        }

        [HttpGet("GetAllTeams")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult> GetAllTeams()
        {
            try
            {
                var TeamList = await _teamRepository.GetAllTeams();
                return Ok(TeamList);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }
    }
}
