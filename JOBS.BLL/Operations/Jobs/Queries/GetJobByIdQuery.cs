using AutoMapper;
using JOBS.BLL.DTOs.Respponces;
using JOBS.DAL.Data;
using JOBS.DAL.Entities;
using JOBS.DAL.Exceptions;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace JOBS.BLL.Operations.Jobs.Queries
{
    public class GetJobByIdQuery : IRequest<JobWithTasksDTO?>
    {
        public Guid Id { get; set; }

    }
    public class GetJobByIdQueryHendler : IRequestHandler<GetJobByIdQuery, JobWithTasksDTO?>
    {
        private readonly ServiceStationDBContext _context;
        private readonly IMapper mapper;

        public GetJobByIdQueryHendler(ServiceStationDBContext context, IMapper mapper)
        {
            _context = context;
            this.mapper = mapper;
        }

        public async Task<JobWithTasksDTO?> Handle(GetJobByIdQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var jobs = _context.Jobs.Include(x => x.Tasks).ToList();

                var job = jobs.Where(p => p.Id == request.Id).FirstOrDefault();
                if (job == null) throw new NotFoundException($"job {request.Id} not found");

                return mapper.Map<Job, JobWithTasksDTO?>(jobs.Where(p=>p.Id == request.Id).FirstOrDefault());
            }
            catch (Exception ex) { throw ex; }


        }
    }
}
