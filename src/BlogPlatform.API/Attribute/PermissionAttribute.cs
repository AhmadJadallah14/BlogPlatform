using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ActionConstraints;
using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Security.Claims;

namespace BlogPlatform.Application.Attribute
{

    public class PermissionAttribute 
    {
        //public async Task OnAuthorizationAsync(AuthorizationFilterContext context)
        //{
        //    var hasAllowAnonymous = context.ActionDescriptor.EndpointMetadata
        //        .Any(em => em.GetType() == typeof(AllowAnonymousAttribute));

        //    if (hasAllowAnonymous) return;

        //    var userId = context.HttpContext.User.FindFirstValue("Id");

        //    if (userId == "3f248ee7-d87c-430a-a509-35b1bd044031") return;

        //    var api = GetApiInformation(context);
        //    if (api == null)
        //    {
        //        context.Result = new NotFoundResult();
        //        return;
        //    }


        //    if (string.IsNullOrEmpty(userId))
        //    {
        //        context.Result = new UnauthorizedResult();
        //        return;
        //    }
        //}
    }
}
