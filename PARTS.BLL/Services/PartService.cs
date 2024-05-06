using AutoMapper;
using PARTS.BLL.DTOs.Requests;
using PARTS.BLL.DTOs.Responses;
using PARTS.BLL.Services.ClientPartBLL.Services;
using PARTS.BLL.Services.Interaces;
using PARTS.DAL.Entities.Item;
using PARTS.DAL.Interfaces;
using PARTS.DAL.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PARTS.BLL.Services
{
    public class PartService : GenericService<Part, PartRequest, PartResponse>, IPartService
    {
        public PartService(IPartRepository repository, IMapper mapper) : base(repository, mapper)
        {
        }
    }
}
