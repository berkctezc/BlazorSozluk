using System.Security.Claims;
using Microsoft.AspNetCore.Mvc;

namespace BlazorSozluk.Api.WebApi.Controllers;

public class BaseController : ControllerBase
{
    public Guid UserId => new(HttpContext.User.FindFirst(ClaimTypes.NameIdentifier)?.Value);
}