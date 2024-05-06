using PARTS.BLL.DTOs.Requests;
using PARTS.BLL.DTOs.Responses;
using PARTS.DAL.Entities.Item;
using PARTS.DAL.Entities.Vehicle;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PARTS.BLL.Services.Interaces
{
    public interface IVehicleService : IGenericService<Vehicle, VehicleRequest, VehicleResponse>
    {
    }
}
