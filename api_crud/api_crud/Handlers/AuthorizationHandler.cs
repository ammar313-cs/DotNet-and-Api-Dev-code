using api_crud.Configuration;
using api_crud.Handlers;
using jsonCrud.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.Controllers;


public class DynamicRoleAuthorizationHandler : AuthorizationHandler<DynamicRoleRequirement>
{
    private readonly UserDBHandler _userDbHandler;

    private readonly ILogger<DynamicRoleAuthorizationHandler> _logger;

    private readonly IHttpContextAccessor _httpContextAccessor;

   // private readonly IDictionary<string, Dictionary<string, List<string>>> _controllerAccessConfig;

   private readonly ControllerAccessConfig _controllerAccessConfig1;

    public DynamicRoleAuthorizationHandler(UserDBHandler userDb,
        IHttpContextAccessor httpContextAccessor,
        ILogger<DynamicRoleAuthorizationHandler> logger,
        //IDictionary<string, Dictionary<string, List<string>>> controllerAccessConfig,
        ControllerAccessConfig controllerAccessConfig1)
    {
        _userDbHandler = userDb;
        _logger = logger;
        _httpContextAccessor = httpContextAccessor;
       // _controllerAccessConfig = controllerAccessConfig;
       _controllerAccessConfig1 = controllerAccessConfig1 ?? throw new ArgumentNullException(nameof(controllerAccessConfig1));

       // logger.LogInformation($"ControllerAccessConfig is null: {_controllerAccessConfig == null}");
    }

    protected override async Task HandleRequirementAsync(AuthorizationHandlerContext context, DynamicRoleRequirement requirement)
    {
        var httpContext = _httpContextAccessor.HttpContext;

        var endpoint = httpContext?.GetEndpoint();
        if (endpoint == null)
        {
            context.Fail(); // Endpoint not found, authorization fails
            return;
        }

        // Get the action descriptor from the endpoint metadata
        if (!(endpoint.Metadata.GetMetadata<ControllerActionDescriptor>() is ControllerActionDescriptor actionDescriptor))
        {
            context.Fail(); // Action descriptor not found, authorization fails
            return;
        }

        var controllerName = actionDescriptor.ControllerName;
        var actionName = actionDescriptor.ActionName;

        // Check user's roles
        var user = context.User;
        var userRoles = (await _userDbHandler.GetUserByUsername(user.Identity.Name)).Role;

        if (userRoles.Contains("Admin"))
        {
            context.Succeed(requirement); // Admin can access all actions
            return;
        }

        // Check if the controller is defined in the configuration
        if (  _controllerAccessConfig1.ControllerRoles.TryGetValue(controllerName, out var controllerRoles))
        {
            // Check if the user's role is defined in the configuration for this controller
            

                var RoleName = userRoles;

                if (controllerRoles.TryGetValue(RoleName, out var allowedActions))
                {
                    // Check if the user's role allows the current action
                    if (allowedActions.Contains(actionName))
                    {
                        context.Succeed(requirement);
                        return;
                    }
                }
            
        }

        // If the user does not have any of the required roles or is trying to access an unauthorized feature, authorization fails.
        context.Fail();
    }
}
