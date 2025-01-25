using System.Net;
using AutoMapper;
using ICC_Champion_Trophy_2025.Model;
using ICC_Champion_Trophy_2025.Model.DTO;
using ICC_Champion_Trophy_2025.Repository;
using ICC_Champion_Trophy_2025.Repository.IRepository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ICC_Champion_Trophy_2025.Controllers
{
    [Route("api/player")]
    [ApiController]
    public class PlayerController : ControllerBase
    {
        private readonly ApplicationDbContext _DBcontext;
        private readonly APIResponse _APIResponse;
        private readonly IPlayer _PlayerRepository;
        private readonly IMapper _Mapper;
        public PlayerController(ApplicationDbContext dBcontext, IPlayer playerRepository, IMapper mapper)
        {
            _DBcontext = dBcontext;
            _APIResponse = new APIResponse();
            _PlayerRepository = playerRepository;
            _Mapper = mapper;
        }

        [HttpGet("getplayerlist")]
        public async Task<ActionResult<APIResponse>> GetPlayerList()
        {
            try
            {
                var PlayerList = await _PlayerRepository.GetPlayerList();
                if (PlayerList != null)
                {
                    _APIResponse.success = true;
                    _APIResponse.StatusCode = HttpStatusCode.OK;
                    _APIResponse.result = PlayerList;
                    _APIResponse.Message = "Data fetched successfully!";
                    return Ok(_APIResponse);
                }
            }
            catch (Exception ex)
            {
                _APIResponse.success = false;
                _APIResponse.Error = new List<string> { ex.Message };
                return BadRequest(_APIResponse);
            }
            return _APIResponse;
        }

        [HttpPost("addnewplayer")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        public async Task<ActionResult<APIResponse>> AddNewPlayer([FromBody] PlayerDTO playerDTO)
        {
            try
            {
                var existingPlayers = await _PlayerRepository.GetPlayerList(u => u.PlayerName.ToLower() == playerDTO.PlayerName.ToLower());
                if (existingPlayers != null && existingPlayers.Count > 0)
                {
                    ModelState.AddModelError("ErrorMsg", "Player already exist !");
                    return BadRequest(ModelState);
                }
                if (playerDTO == null)
                {
                    return BadRequest(playerDTO);
                }
                Players players = _Mapper.Map<Players>(playerDTO);
                await _PlayerRepository.AddNewPlayer(players);
                _APIResponse.success = true;
                _APIResponse.Message = "Player added in team successfully !";
                _APIResponse.result = _Mapper.Map<Players>(playerDTO);
                _APIResponse.StatusCode = HttpStatusCode.Created;
                return CreatedAtAction(nameof(AddNewPlayer), new { id = players.Id }, _APIResponse);
            }
            catch (Exception ex)
            {
                _APIResponse.StatusCode = HttpStatusCode.BadGateway;
                _APIResponse.success = false;
                _APIResponse.Error = new List<string> { ex.Message };
            }
            return _APIResponse;
        }

        [HttpDelete("{id:int}", Name = "deleteplayerfromteam")]
        public async Task<ActionResult<APIResponse>> DeletePlayerFromTeam(int id)
        {
            try
            {
                var user = _PlayerRepository.GetPlayerList(p => p.Id == id);
                if (user == null)
                {
                    _APIResponse.StatusCode = HttpStatusCode.NotFound;
                    _APIResponse.success = false;
                    _APIResponse.Message = "No player found!";
                    return NotFound(_APIResponse);
                }
                bool isDeleted = await _PlayerRepository.DeletePlayerFromTeam(id);
                if (isDeleted)
                {
                    _APIResponse.StatusCode = HttpStatusCode.OK;
                    _APIResponse.success = true;
                    _APIResponse.Message = "User deleted successfully !";
                    return Ok(_APIResponse);
                }
                return _APIResponse;
            }
            catch (Exception ex)
            {
                _APIResponse.StatusCode = HttpStatusCode.InternalServerError;
                _APIResponse.success = false;
                _APIResponse.Error.Add(ex.Message);
                return StatusCode((int)HttpStatusCode.InternalServerError, _APIResponse);
            }
        }

    }
}
