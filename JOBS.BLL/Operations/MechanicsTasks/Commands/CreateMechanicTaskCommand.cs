using JOBS.DAL.Data;
using JOBS.DAL.Entities;
using MediatR;

namespace JOBS.BLL.Operations.MechanicsTasks.Commands;

public record CreateMechanicTaskCommand : IRequest<Guid>
{
    public Guid MechanicId { get; set; }
    public Guid? JobId { get; set; }
    public string Task { get; set; }
    public string Status { get; set; }
}

public class CreateMechanicTaskCommandHandler : IRequestHandler<CreateMechanicTaskCommand, Guid>
{
    private readonly ServiceStationDContext _context;

    public CreateMechanicTaskCommandHandler(ServiceStationDContext context)
    {
        _context = context;
    }

    public async Task<Guid> Handle(CreateMechanicTaskCommand request, CancellationToken cancellationToken)
    {
        var entity = new DAL.Entities.MechanicsTasks()
        {
            MechanicId = request.MechanicId,
            JobId = request.JobId,
            Task = request.Task,
            Status = request.Status,

        };

        await _context.MechanicsTasks.AddAsync(entity);


        await _context.SaveChangesAsync(cancellationToken);

        return entity.Id;
    }
}
