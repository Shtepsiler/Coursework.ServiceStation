using AutoMapper;
using PARTS.BLL.DTOs.Requests;
using PARTS.BLL.DTOs.Responses;
using PARTS.BLL.Services.Interaces;
using PARTS.DAL.Entities.Vehicle;
using PARTS.DAL.Interfaces;

namespace PARTS.BLL.Services
{
    public class MakeService : GenericService<Make, MakeRequest, MakeResponse>, IMakeService
    {
        public MakeService(IMakeRepository repository, IMapper mapper) : base(repository, mapper)
        {
        }
    }
}
