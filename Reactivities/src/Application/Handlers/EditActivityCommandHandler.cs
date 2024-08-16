using Application.Commands;
using Persistence;

namespace Application.Handlers;
public class EditActivityCommandHandler : IRequestHandler<EditActivityCommand, Result<bool>>
{
    private readonly DataContext _context;
    private readonly IMapper _mapper;


    public EditActivityCommandHandler(DataContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }
    public async Task<Result<bool>> Handle(EditActivityCommand request, CancellationToken cancellationToken)
    {
        var activity = await _context.Activities.FindAsync(request.Activity.Id, cancellationToken);

        if (activity is null) return null;

        activity = request.Activity;

        _context.Activities.Update(activity);

        return await _context.SaveChangesAsync(cancellationToken) > 0 ? Result<bool>.Success(true) : Result<bool>.Failure("Failed to update activity");

    }
}

