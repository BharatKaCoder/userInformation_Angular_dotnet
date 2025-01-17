using userInformation_Angular_dotnet.Models.DTO;

namespace userInformation_Angular_dotnet.Repository.IRepository
{
    public interface IUserRepository
    {
        bool IsUniqueUser(string UserName);

        Task <LoginResponseDTO> Login(LoginRequestDTO LoginRequest);
    }
}
