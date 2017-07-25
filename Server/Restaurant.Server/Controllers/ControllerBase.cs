using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Restaurant.Server.Controllers
{
    public abstract class ControllerBase : Controller
    {
        protected IActionResult Error(IdentityResult result)
        {
            foreach (var identityError in result.Errors)
            {
                ModelState.AddModelError(identityError.Code, identityError.Description);
            }
            return BadRequest(ModelState);
        }
    }
}