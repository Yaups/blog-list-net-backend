using Microsoft.AspNetCore.Mvc;

namespace blog_list_net_backend.Controllers
{
    [ApiController]
    [Route("diagnostics")]
    public class DiagnosticsController() : ControllerBase
    {
        [HttpGet("version")]
        public ActionResult<string> GetVersion()
        {
            return "shard";
        }
    }
}
