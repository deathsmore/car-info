using DVG.AP.Cms.CarInfo.Application.Contracts.Constant;
using DVG.AutoPortal.Core.Exceptions;
using Microsoft.AspNetCore.Mvc.Filters;
using Newtonsoft.Json;

namespace DVG.AP.Cms.CarInfo.Api.Authorization
{
    [AttributeUsage(AttributeTargets.Method)]
    public class HasPermissionAttribute : Attribute, IAsyncActionFilter
    {
        private readonly PermissionGrant[] _permissions;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="permission"></param>
        public HasPermissionAttribute(PermissionGrant[] permissions)
        {
            _permissions = permissions;
        }
        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            var permissionClaim = context.HttpContext.User.Claims.FirstOrDefault(c => c.Type == "permission")?.Value;
            var userPermissions = string.IsNullOrEmpty(permissionClaim) ? new List<UserPermission>() : JsonConvert.DeserializeObject<List<UserPermission>>(permissionClaim);
            if (HasPermission(userPermissions))
            {
                await next();
            }
            else
            {
                throw new ForbidException(ExceptionMessages.NotHavePermission);
            }
        }

        private bool HasPermission(List<UserPermission> userPermissions) 
        {
            if(userPermissions == null || !userPermissions.Any())
            {
                return false;
            }    

            foreach (var permission in _permissions)
            {
                if (userPermissions.Any(up => up.Id == permission))
                {
                    return true;
                }
            }
            return false;
        }
    }
}
