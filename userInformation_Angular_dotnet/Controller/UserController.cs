using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using userInformation_Angular_dotnet.Models.DTO;
using userInformation_Angular_dotnet.Repository.IRepository;
using Microsoft.EntityFrameworkCore;
using System.Net;
using AutoMapper;
using userInformation_Angular_dotnet.Models;

namespace userInformation_Angular_dotnet.Controller
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly ApplicationDbContext _dbcontext;
        private readonly IRegistration _registration;
        private readonly IMapper _mapper; // This is Automapper and installed this from Nupackage

        public UserController(ApplicationDbContext dbContext, IRegistration registration, IMapper mapper)
        {
            _dbcontext = dbContext;
            _registration = registration;
            _mapper = mapper;
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult> CreateNewUser([FromBody] RegistrationRequestDTO regDTO)
        {
            try
            {
                //var UserList = await _registration.GetAllUserList(u => u.UserName.ToLower() == regDTO.UserName.ToLower()) != null; ;
                //if (UserList)
                //{
                //    ModelState.AddModelError("ErrorMsg", "The user is already registered!");
                //    return BadRequest(ModelState);
                //}

                if (regDTO == null)
                {
                    return BadRequest(regDTO);
                }
                Registration registration = _mapper.Map<Registration>(regDTO);
                await _registration.CreateNewUser(registration);
                return Ok("User created successfully!");
            }
            catch (Exception ex) { 
                return BadRequest(ex.Message);
            }
        }

    }
}
