using System.Net;
using Azure;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using userInformation_Angular_dotnet.Models;
using userInformation_Angular_dotnet.Models.DTO;
using userInformation_Angular_dotnet.Repository.IRepository;

namespace userInformation_Angular_dotnet.Controller
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthUserController : ControllerBase
    {
        private readonly IUserRepository _userRepository;
        protected APIResponse _APIResponse; 
        public AuthUserController(IUserRepository userRepository)
        {
            _userRepository = userRepository;
            _APIResponse = new APIResponse();
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginRequestDTO loginRequestDTO)
        {

            if (loginRequestDTO == null || string.IsNullOrEmpty(loginRequestDTO.UserName) || string.IsNullOrEmpty(loginRequestDTO.Password))
            {
                _APIResponse.StatusCode = HttpStatusCode.BadRequest;
                _APIResponse.success = false;
                _APIResponse.ErrorMessages.Add("User name and password required!");
                return BadRequest(_APIResponse);
            }


            if (string.IsNullOrEmpty(loginRequestDTO.UserName) || string.IsNullOrEmpty(loginRequestDTO.Password))
            {
                _APIResponse.StatusCode = HttpStatusCode.BadRequest;
                _APIResponse.success = false;
                _APIResponse.ErrorMessages.Add("User name and password required!");
                return BadRequest(_APIResponse);
            }

            var loginRes = await _userRepository.Login(loginRequestDTO);
            bool isPasswordValid = BCrypt.Net.BCrypt.Verify(loginRequestDTO.Password, loginRes.User.Password);
            if (loginRes == null || loginRes.User == null || string.IsNullOrEmpty(loginRes.Token) || !isPasswordValid)
            {
                _APIResponse.StatusCode = HttpStatusCode.BadRequest;
                _APIResponse.success = false;
                _APIResponse.ErrorMessages.Add("User or password is incorrect.");
                return BadRequest(_APIResponse);
            }

            _APIResponse.StatusCode = HttpStatusCode.OK;
            _APIResponse.success = true;
            _APIResponse.Result = new { loginRes.User.UserName , loginRes.Token }; // Return the login response
            return Ok(_APIResponse);
        }
    }
}
