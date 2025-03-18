using MediatR;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using System.Linq;
using System.Security.Claims;

public class AuthorizationBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
    where TRequest : IRequest<TResponse>, ISecuredRequest
{
    private readonly IHttpContextAccessor _httpContextAccessor;

    public AuthorizationBehavior(IHttpContextAccessor httpContextAccessor)
    {
        _httpContextAccessor = httpContextAccessor;
    }

    public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
    {
        var userRoles = _httpContextAccessor.HttpContext.User.ClaimRoles();

        if (userRoles == null) throw new AuthorizationException("Claims not found.");

        bool isNotMatchedRole = request.Roles.Any(role => !userRoles.Contains(role));

        if (isNotMatchedRole) throw new AuthorizationException("You are not authorized.");

        var response = await next();
        return response;
    }
}