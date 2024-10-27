using System.Security.Claims;
using Microsoft.AspNetCore.Http;

namespace LFM.WorkStream.Core.Utils;

public class UserHelper(IHttpContextAccessor httpContextAccessor) : IUserHelper
{
    public string GetUserId()
    {
        return httpContextAccessor.HttpContext?.User.FindFirst(ClaimTypes.NameIdentifier)?.Value 
               ?? throw new Exception("Could not find authenticated user");;
    }
}