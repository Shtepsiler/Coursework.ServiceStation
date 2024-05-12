using AutoMapper;
using JOBS.BLL.DTOs.Respponces;
using JOBS.DAL.Data;
using JOBS.DAL.Entities;
using MediatR;

namespace JOBS.BLL.Operations.Jobs.Queries
{
    public class GetJobByIdQuery : IRequest<JobDTO>
    {
        public Guid Id { get; set; }

    }
    public class GetJobByIdQueryHendler : IRequestHandler<GetJobByIdQuery, JobDTO>
    {
        private readonly ServiceStationDContext _context;
        private readonly IMapper mapper;

        public GetJobByIdQueryHendler(ServiceStationDContext context, IMapper mapper)
        {
            _context = context;
            this.mapper = mapper;
        }

        public async Task<JobDTO> Handle(GetJobByIdQuery request, CancellationToken cancellationToken)
        {
            try
            {
                return mapper.Map<Job, JobDTO>(await _context.Jobs.FindAsync(request.Id, cancellationToken));
            }
            catch (Exception ex) { throw ex; }


        }
    }
}
