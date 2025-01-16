using AutoMapper;
using userInformation_Angular_dotnet.Models;
using userInformation_Angular_dotnet.Models.DTO;

namespace userInformation_Angular_dotnet
{
    public class MappingConfig : Profile
    {
        public MappingConfig() 
        {
            CreateMap<Registration, RegistrationRequestDTO>().ReverseMap();
        }
    }
}
