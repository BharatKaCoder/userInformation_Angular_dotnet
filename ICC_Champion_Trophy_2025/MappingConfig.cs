using AutoMapper;
using ICC_Champion_Trophy_2025.Model;
using ICC_Champion_Trophy_2025.Model.DTO;

namespace ICC_Champion_Trophy_2025
{
    public class MappingConfig :Profile
    {
        public MappingConfig()
        {
            CreateMap<Teams, TeamsDTO>().ReverseMap();
        }
    }
}
