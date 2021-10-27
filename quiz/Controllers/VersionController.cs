using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace quiz.Controllers
{
    [Route("quizapi")]
    [ApiController]
    public class VersionController : Controller
    {

        [HttpGet("GetVersion")]
        public ActionResult GetVersion()
        {
            return Ok("v1");
        }
    }

}