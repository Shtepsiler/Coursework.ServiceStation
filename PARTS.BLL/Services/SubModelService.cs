using AutoMapper;
using PARTS.BLL.DTOs.Requests;
using PARTS.BLL.DTOs.Responses;
using PARTS.BLL.Services.Interaces;
using PARTS.DAL.Entities.Vehicle;
using PARTS.DAL.Interfaces;

namespace PARTS.BLL.Services
{
    public class SubModelService : GenericService<SubModel, SubModelRequest, SubModelResponse>, ISubModelService
    {
        public SubModelService(ISubModelRepository repository, IMapper mapper) : base(repository, mapper)
        {
        }
    }
}
