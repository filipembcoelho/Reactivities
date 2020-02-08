using System;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Persistence;

namespace Infrastructure.Security
{
  public class IsHostRequirement : IAuthorizationRequirement
  {
    // Custom Auth policy


  }

  public class IsHostRequirementHandler : AuthorizationHandler<IsHostRequirement>
  {
    private readonly IHttpContextAccessor _httpContext;
    private readonly DataContext _context;

    public IsHostRequirementHandler(IHttpContextAccessor httpContext, DataContext context)
    {
      this._context = context;
      this._httpContext = httpContext;
    }

    protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, IsHostRequirement requirement)
    {
      var currentUserName = _httpContext.HttpContext.User?.Claims?.SingleOrDefault(x => x.Type == ClaimTypes.NameIdentifier)?.Value;

      var activityId = Guid.Parse(_httpContext.HttpContext.Request.RouteValues.SingleOrDefault(X => X.Key == "id").Value.ToString());

      var activity = _context.Activities.FindAsync(activityId).Result;

      var host = activity.UserActivities.FirstOrDefault(X => X.IsHost);

      if (host?.AppUser?.UserName == currentUserName)
        context.Succeed(requirement);

      return Task.CompletedTask;
    }
  }
}