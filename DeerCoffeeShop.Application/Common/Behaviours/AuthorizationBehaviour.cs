using DeerCoffeeShop.Application.Common.Exceptions;
using DeerCoffeeShop.Application.Common.Interfaces;
using DeerCoffeeShop.Application.Common.Security;
using MediatR;
using System.Reflection;

namespace DeerCoffeeShop.Application.Common.Behaviours
{
    public class AuthorizationBehaviour<TRequest, TResponse>(
        ICurrentUserService currentUserService) : IPipelineBehavior<TRequest, TResponse>
       where TRequest : notnull
    {
        public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
        {
            IEnumerable<AuthorizeAttribute> authorizeAttributes = request.GetType().GetCustomAttributes<AuthorizeAttribute>();

            if (authorizeAttributes.Any())
            {
                // Must be authenticated user
                if (currentUserService.UserId == null)
                {
                    throw new UnauthorizedAccessException();
                }

                // Role-based authorization
                IEnumerable<AuthorizeAttribute> authorizeAttributesWithRoles = authorizeAttributes.Where(a => !string.IsNullOrWhiteSpace(a.Roles));

                if (authorizeAttributesWithRoles.Any())
                {
                    foreach (string[]? roles in authorizeAttributesWithRoles.Select(a => a.Roles.Split(',')))
                    {
                        bool authorized = false;
                        foreach (string? role in roles)
                        {
                            bool isInRole = await currentUserService.IsInRoleAsync(role.Trim());
                            if (isInRole)
                            {
                                authorized = true;
                                break;
                            }
                        }

                        // Must be a member of at least one role in roles
                        if (!authorized)
                        {
                            throw new ForbiddenAccessException();
                        }
                    }
                }

                // Policy-based authorization
                IEnumerable<AuthorizeAttribute> authorizeAttributesWithPolicies = authorizeAttributes.Where(a => !string.IsNullOrWhiteSpace(a.Policy));
                if (authorizeAttributesWithPolicies.Any())
                {
                    foreach (string? policy in authorizeAttributesWithPolicies.Select(a => a.Policy))
                    {
                        bool authorized = await currentUserService.AuthorizeAsync(policy);

                        if (!authorized)
                        {
                            throw new ForbiddenAccessException();
                        }
                    }
                }
            }

            // User is authorized / authorization not required
            return await next();
        }
    }
}
