using System.Net;
using AutoMapper;
using ICC_Champion_Trophy_2025.Model;
using ICC_Champion_Trophy_2025.Model.DTO;
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
        private readonly APIResponse _apiResponse;
        public TeamsController(ApplicationDbContext dbContext, IMapper mapper, ITeamRepository teamRepository)
        {
            _DBcontext = dbContext;
            _mapper = mapper;
            _apiResponse = new();
            _teamRepository = teamRepository;
        }

        [HttpGet("GetAllTeams")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<APIResponse>> GetAllTeams()
        {
            try
            {
                var TeamList = await _teamRepository.GetAllTeams();
                if (TeamList == null)
                {
                    _apiResponse.StatusCode = HttpStatusCode.NoContent;
                    _apiResponse.success = true;
                    _apiResponse.Message = "list is empty!";
                    _apiResponse.result = null;
                    return _apiResponse;
                }
                if (TeamList != null)
                {
                    _apiResponse.StatusCode = HttpStatusCode.OK;
                    _apiResponse.success = true;
                    _apiResponse.Message = "Data fetched successfully!";
                    _apiResponse.result = TeamList;
                    return Ok(_apiResponse);
                }
            }
            catch (Exception ex)
            {
                _apiResponse.success = false;
                _apiResponse.Error = new List<string> { ex.Message };
                return BadRequest(_apiResponse);
            }
            return _apiResponse;
        }
    }
}
