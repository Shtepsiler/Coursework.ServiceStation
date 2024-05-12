using JOBS.DAL.Data;
using JOBS.DAL.Entities;
using MediatR;

namespace JOBS.BLL.Operations.Jobs.Commands;

public record CreateJobCommand : IRequest<Guid>
{
    /*    public int? ManagerId { get; set; }*/
    public Guid ModelId { get; set; }
    public Guid ClientId { get; set; }
    public DateTime IssueDate { get; set; }
    public string Description { get; set; }
}

public class CreateJobHandler : IRequestHandler<CreateJobCommand, Guid>
{
    private readonly ServiceStationDContext _context;

    public CreateJobHandler(ServiceStationDContext context)
    {
        _context = context;
    }

    public async Task<Guid> Handle(CreateJobCommand request, CancellationToken cancellationToken)
    {
        var entity = new Job()
        {
            /* ManagerId = request.ManagerId,*/
            ModelId = request.ModelId,
            ClientId = request.ClientId,
            IssueDate = request.IssueDate,
            Description = request.Description
        };

        await _context.Jobs.AddAsync(entity);


        await _context.SaveChangesAsync(cancellationToken);

        return entity.Id;
    }

}
