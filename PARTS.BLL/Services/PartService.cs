using AutoMapper;
using PARTS.BLL.DTOs.Requests;
using PARTS.BLL.DTOs.Responses;
using PARTS.BLL.Services.Interaces;
using PARTS.DAL.Entities.Item;
using PARTS.DAL.Interfaces;

namespace PARTS.BLL.Services
{
    public class PartService : GenericService<Part, PartRequest, PartResponse>, IPartService
    {
        public PartService(IPartRepository repository, IMapper mapper) : base(repository, mapper)
        {
        }

        public async Task<IEnumerable<PartResponse>> GetPartsByOrderId(Guid OrderId)
        {

            try
            {
                var entities = await _repository.GetAsync();
                entities = entities.Where(p=>p.Orders.Any(p=>p.Id == OrderId));
                return _mapper.Map<IEnumerable<Part>, IEnumerable<PartResponse>>(entities);
            }
            catch (Exception ex)
            {
                throw ex;
            }




        }
    }
}
