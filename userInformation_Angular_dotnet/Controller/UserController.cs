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
        private readonly APIResponse _APIResponse;

        public UserController(ApplicationDbContext dbContext, IRegistration registration, IMapper mapper)
        {
            _dbcontext = dbContext;
            _registration = registration;
            _mapper = mapper;
            _APIResponse = new();
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]

        public async Task<ActionResult<APIResponse>> GetAllUser()
        {
            try
            {
                IEnumerable<Registration> UserList = await _registration.GetAllUserListAsync();
                _APIResponse.Result = _mapper.Map<List<RegistrationRequestDTO>>(UserList);
                _APIResponse.success = true;
                _APIResponse.StatusCode = HttpStatusCode.OK;
                return Ok(_APIResponse);
            }
            catch (Exception ex)
            {
                _APIResponse.success = false;
                _APIResponse.ErrorMessages = new List<string>() { ex.Message };
            }
            return _APIResponse;
        }

        [HttpPost(Name ="createNewUser")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<APIResponse>> CreateNewUser([FromBody] RegistrationRequestDTO regDTO)
        {
            try
            {
                var userList = await _registration.GetAllUserListAsync(u => u.UserName.ToLower() == regDTO.UserName.ToLower());
                if (userList != null && userList.Count > 0)
                {
                    ModelState.AddModelError("ErrorMsg", "The user is already registered!");
                    return BadRequest(ModelState);
                }

                if (regDTO == null)
                {
                    return BadRequest(regDTO);
                }

                // hashing password before saving
                string HashedPassword = BCrypt.Net.BCrypt.HashPassword(regDTO.Password);

                Registration registration = _mapper.Map<Registration>(regDTO);
                registration.Password = HashedPassword;

                // Map the RegistrationRequestDTO to the Registration entity
                await _registration.CreateNewUser(registration);
                _APIResponse.Result = _mapper.Map<RegistrationRequestDTO>(regDTO);
                _APIResponse.success = true;
                _APIResponse.StatusCode = HttpStatusCode.OK;
                return CreatedAtAction(nameof(CreateNewUser), new { id = registration.Id }, _APIResponse);
            }
            catch (Exception ex) {
                _APIResponse.success = false;
                _APIResponse.ErrorMessages = new List<string> { ex.Message };
            }
            return _APIResponse;
        }

        [HttpDelete("{id:int}", Name ="delete")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<APIResponse>> DeleteUserAsync(int id)
        {
            try
            {
                // Get user by id (await the method as it's async)
                var user = await _registration.GetUserById(id);
                if (user == null)
                {
                    _APIResponse.StatusCode = HttpStatusCode.NotFound;
                    _APIResponse.success = false;
                    _APIResponse.ErrorMessages.Add("User not found.");
                    return NotFound(_APIResponse);
                }

                // Attempt to delete the user (await the method as it's async)
                bool isDeleted = await _registration.DeleteUserAsync(id);
                if (isDeleted)
                {
                    // Return success response if user is deleted
                    _APIResponse.StatusCode = HttpStatusCode.OK;
                    _APIResponse.success = true;
                    _APIResponse.Result = "User deleted successfully.";
                    return Ok(_APIResponse);
                }

                // In case of failure during deletion
                _APIResponse.StatusCode = HttpStatusCode.InternalServerError;
                _APIResponse.success = false;
                _APIResponse.ErrorMessages.Add("An error occurred while deleting the user.");
                return StatusCode((int)HttpStatusCode.InternalServerError, _APIResponse);
            }
            catch (Exception ex)
            {
                // Exception handling
                _APIResponse.StatusCode = HttpStatusCode.InternalServerError;
                _APIResponse.success = false;
                _APIResponse.ErrorMessages.Add(ex.Message);
                return StatusCode((int)HttpStatusCode.InternalServerError, _APIResponse);
            }
        }

        [HttpPut("{id:int}", Name = "update")]
        [ProducesResponseType(typeof(APIResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(APIResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(APIResponse), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(APIResponse), StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<APIResponse>> UpdateUserAsync(int id, [FromBody] RegistrationUpdateDTO updatedUser)
        {
            try
            {
                // Ensure valid user data
                if (updatedUser == null || updatedUser.Id != id)
                {
                    _APIResponse.StatusCode = HttpStatusCode.BadRequest;
                    _APIResponse.success = false;
                    _APIResponse.ErrorMessages.Add("Invalid user data.");
                    return BadRequest(_APIResponse);
                }

                // Get existing user by ID
                var user = await _registration.GetUserById(id);
                if (user == null)
                {
                    _APIResponse.StatusCode = HttpStatusCode.NotFound;
                    _APIResponse.success = false;
                    _APIResponse.ErrorMessages.Add("User not found.");
                    return NotFound(_APIResponse);
                }

                // Check if the password needs to be updated
                if (!string.IsNullOrEmpty(updatedUser.Password))
                {
                    // Hash the new password
                    string hashedPassword = BCrypt.Net.BCrypt.HashPassword(updatedUser.Password);
                    updatedUser.Password = hashedPassword; // Set the hashed password back to updatedUser
                }

                // Map updated data to existing user
                _mapper.Map(updatedUser, user);  // Ensure mapping is configured properly

                // Call repository method to update user
                bool isUpdated = await _registration.UpdateUserAsync(user);
                if (isUpdated)
                {
                    _APIResponse.StatusCode = HttpStatusCode.OK;
                    _APIResponse.success = true;
                    _APIResponse.Result = "User updated successfully.";
                    return Ok(_APIResponse);
                }

                // Handle failure during update
                _APIResponse.StatusCode = HttpStatusCode.InternalServerError;
                _APIResponse.success = false;
                _APIResponse.ErrorMessages.Add("An error occurred while updating the user.");
                return StatusCode((int)HttpStatusCode.InternalServerError, _APIResponse);
            }
            catch (Exception ex)
            {
                // Exception handling
                _APIResponse.StatusCode = HttpStatusCode.InternalServerError;
                _APIResponse.success = false;
                _APIResponse.ErrorMessages.Add(ex.Message);

                return StatusCode((int)HttpStatusCode.InternalServerError, _APIResponse);
            }
        }


        [HttpGet("{id:int}", Name = "getUserById")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(APIResponse))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<APIResponse>> GetUserById(int id)
        {
            try
            {
                // Check for invalid ID
                if (id <= 0)
                {
                    _APIResponse.StatusCode = HttpStatusCode.BadRequest;
                    _APIResponse.success = false;
                    _APIResponse.ErrorMessages.Add("Invalid user ID.");
                    return BadRequest(_APIResponse);
                }

                // Retrieve the user by ID
                var editedUser = await _registration.GetUserById(id);

                // Check if the user was found
                if (editedUser == null)
                {
                    _APIResponse.StatusCode = HttpStatusCode.NotFound;
                    _APIResponse.success = false;
                    _APIResponse.ErrorMessages.Add("User not found.");
                    return NotFound(_APIResponse);
                }

                // Map the user to DTO and prepare the response
                var userDto = _mapper.Map<RegistrationRequestDTO>(editedUser);
                _APIResponse.Result = userDto;
                _APIResponse.success = true;
                _APIResponse.StatusCode = HttpStatusCode.OK;

                return Ok(_APIResponse);
            }
            catch (Exception ex)
            {
                // Handle exceptions
                _APIResponse.StatusCode = HttpStatusCode.InternalServerError;
                _APIResponse.success = false;
                _APIResponse.ErrorMessages.Add(ex.Message);

                return StatusCode((int)HttpStatusCode.InternalServerError, _APIResponse);
            }
        }

    }
}
