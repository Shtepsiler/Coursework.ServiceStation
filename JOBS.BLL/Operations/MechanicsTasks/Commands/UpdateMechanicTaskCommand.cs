using JOBS.DAL.Data;
using JOBS.DAL.Exceptions;
using MediatR;

namespace JOBS.BLL.Operations.MechanicsTasks.Commands;

public record UpdateMechanicTaskCommand : IRequest
{
    public Guid Id { get; set; }
    public Guid MechanicId { get; set; }
    public Guid? JobId { get; set; }
    public string Task { get; set; }
}

public class UpdateMechanicTaskCommandHandler : IRequestHandler<UpdateMechanicTaskCommand>
{
    private readonly ServiceStationDContext _context;

    public UpdateMechanicTaskCommandHandler(ServiceStationDContext context)
    {
        _context = context;
    }

    public async Task Handle(UpdateMechanicTaskCommand request, CancellationToken cancellationToken)
    {
        var entity = await _context.MechanicsTasks
            .FindAsync(new object[] { request.Id }, cancellationToken);

        if (entity == null)
        {
            throw new NotFoundException(nameof(DAL.Entities.MechanicsTasks), request.Id);
        }
        entity.MechanicId = request.MechanicId;
        entity.JobId = request.JobId;
        entity.Task = request.Task;


        await _context.SaveChangesAsync(cancellationToken);
    }
}
