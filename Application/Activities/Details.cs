using System;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using Application.Errors;
using AutoMapper;
using Domain;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Persistence;

namespace Application.Activities
{
  public class Details
  {
    public class Query : IRequest<ActivityDto>
    {
      public Guid Id { get; set; }
    }

    public class Handler : IRequestHandler<Query, ActivityDto>
    {
      private readonly DataContext _context;
      private readonly IMapper _mapper;

      public Handler(DataContext context, IMapper mapper)
      {
        this._context = context;
        this._mapper = mapper;
      }

      public async Task<ActivityDto> Handle(Query request, CancellationToken cancellationToken)
      {
        var activity = await _context.Activities
          // .Include(x => x.UserActivities) // Substituted with lazy loading
          // .ThenInclude(x => x.AppUser)
          //.SingleOrDefaultAsync(x => x.Id == request.Id)
          .FindAsync(request.Id);

        if (activity == null)
          throw new RestException(HttpStatusCode.NotFound, new { activity = "Not Found" });

        var activityToReturn = _mapper.Map<Activity, ActivityDto>(activity);

        return activityToReturn;
      }
    }
  }
}