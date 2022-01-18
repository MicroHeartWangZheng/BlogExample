using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApiResource.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class IdentityController : ControllerBase
    {
        [HttpGet("GetInit")]
        public string GetInit()
        {
            return "string";
        }

        [Authorize]
        [HttpGet("GetUser")]
        public IActionResult GetUser()
        {
            return new JsonResult(from  c in User.Claims select new {c.Type, c.Value});
        }

        [Authorize]
        [HttpGet("GetResource")]
        public IActionResult GetResource()
        {
            return new JsonResult(from c in User.Claims select new { c.Type, c.Value });
        }
    }
}
