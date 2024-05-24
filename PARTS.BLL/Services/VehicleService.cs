using AutoMapper;
using Azure.Core;
using PARTS.BLL.DTOs.Requests;
using PARTS.BLL.DTOs.Responses;
using PARTS.BLL.Services.Interaces;
using PARTS.DAL.Entities.Vehicle;
using PARTS.DAL.Interfaces;

namespace PARTS.BLL.Services
{
    public class VehicleService : GenericService<Vehicle, VehicleRequest, VehicleResponse>, IVehicleService
    { 
        public VehicleService(IVehicleRepository repository, IMapper mapper) : base(repository, mapper)
        {
        }

        public async Task<VehicleResponse> PostAsync(VehicleRequest request)
        {
            try
            {
                var entity = _mapper.Map<VehicleRequest, Vehicle>(request);
                await _repository.InsertAsync(entity);
                return _mapper.Map<Vehicle, VehicleResponse>(entity);
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
    }
}
