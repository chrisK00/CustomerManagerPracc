using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CustomerManager.API.Exceptions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CustomerManager.API.Controllers
{
    public class BuggyController : BaseApiController
    {
     
        [Authorize]
        [HttpGet("auth")]
        public ActionResult<string> GetSecret()
        {
            return "secret";
        }

        [HttpGet("bad-request")]
        public ActionResult<string> GetBadRequest()
        {
            throw new AppException("really bad request");
        }

        [HttpGet("server-error")]
        public IActionResult GetServerError()
        {
            object hi = null;
            hi.ToString();
            return Ok(hi);
        }

        [HttpGet("not-found")]
        public IActionResult GetNotFound()
        {
            return NotFound("what are you actually looking for");
        }
    }
}
