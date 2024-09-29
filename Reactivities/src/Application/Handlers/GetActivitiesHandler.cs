﻿using Application.Queries;
using Microsoft.EntityFrameworkCore;
using Persistence.Context;

namespace Application.Handlers;
public class GetActivitiesHandler : IRequestHandler<GetActivitiesQuery, Result<List<Activity>>>
{
    private readonly DataContext _context;

    public GetActivitiesHandler(DataContext context)
    {
        _context = context;
    }

    public async Task<Result<List<Activity>>> Handle(GetActivitiesQuery request, CancellationToken cancellationToken) => 
        Result<List<Activity>>.Success(await _context.Activities.ToListAsync(cancellationToken: cancellationToken));

}
