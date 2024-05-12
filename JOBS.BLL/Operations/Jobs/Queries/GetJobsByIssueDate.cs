﻿using AutoMapper;
using JOBS.BLL.DTOs.Respponces;
using JOBS.DAL.Data;
using JOBS.DAL.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace JOBS.BLL.Operations.Jobs.Queries
{
    public class GetJobsByIssueDate : IRequest<IEnumerable<JobDTO>>
    {
        public DateTime IssueDate { get; set; }
    }
    public class GetJobsByIssueDateHendler : IRequestHandler<GetJobsByIssueDate, IEnumerable<JobDTO>>
    {
        private readonly ServiceStationDContext _context;
        private readonly IMapper _mapper;

        public GetJobsByIssueDateHendler(ServiceStationDContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<IEnumerable<JobDTO>> Handle(GetJobsByIssueDate request, CancellationToken cancellationToken)
        {
            return _mapper.Map<IEnumerable<Job>, IEnumerable<JobDTO>>(await _context.Jobs.Where(p => p.IssueDate.Date == request.IssueDate.Date).ToListAsync());
        }
    }
}
