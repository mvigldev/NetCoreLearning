using Microsoft.AspNetCore.Mvc;

namespace NetCoreLearning.SchoolLessons.API.Controllers
{
    /// <summary>
    /// Base api controller that contains the policy of minimum age
    /// </summary>
    /// <seealso cref="Microsoft.AspNetCore.Mvc.ControllerBase" />
    [Route("api/[controller]")]
    [ApiController]
    [Microsoft.AspNetCore.Authorization.Authorize(Policy = "Over18")]
    public abstract class BaseController : ControllerBase
    {
    }
}