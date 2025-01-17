using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using userInformation_Angular_dotnet.Models.DTO;
using userInformation_Angular_dotnet.Repository.IRepository;

namespace userInformation_Angular_dotnet.Repository
{
    public class UserRepository : IUserRepository
    {
        private readonly ApplicationDbContext _db;
        private string SecreteKey;
        public UserRepository(ApplicationDbContext applicationDbContext, IConfiguration configuration)
        {
            _db = applicationDbContext;
            SecreteKey = configuration.GetValue<string>("ApiSetting:Secret");
        }
        public bool IsUniqueUser(string userName)
        {
            var user = _db.UserListTable.FirstOrDefault(x => x.UserName == userName);
            if (user == null)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public async Task<LoginResponseDTO>Login(LoginRequestDTO loginRequestDTO)
        {
            var users = _db.UserListTable.FirstOrDefault(u=>u.UserName.ToLower() == loginRequestDTO.UserName.ToLower());
            if (users == null)
            {
                return new LoginResponseDTO()
                {
                    Token = "",
                    User = null
                };
            }
            var TokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(SecreteKey);

            var tokenDescription = new SecurityTokenDescriptor
            {
                Subject = new System.Security.Claims.ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.Name, users.Id.ToString()),
                    new Claim(ClaimTypes.Role, users.Role)
                }),
                Expires = DateTime.UtcNow.AddDays(2),
                SigningCredentials = new(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };

            var token = TokenHandler.CreateToken(tokenDescription);

            LoginResponseDTO loginResponseDTO = new();
            {
                loginResponseDTO.Token = TokenHandler.WriteToken(token);
                loginResponseDTO.User = users;
            }
            return loginResponseDTO;
        }
    }
}
