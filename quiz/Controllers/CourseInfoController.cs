using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.IO;
using System.Web;

namespace quiz.Controllers
{
    [Route("quizapi")]
    [ApiController]
    public class CourseInfoController : Controller
    {
        [HttpGet("GetCourseInfo/{NAME}")]
        public ActionResult GetCourseInfo(string name)
        {
            string path = Directory.GetCurrentDirectory();
            string courseDir = Path.Combine(path, "Courses");

            string courseFilename = Path.Combine(courseDir, name + ".html");
            if (System.IO.File.Exists(courseFilename))
            {
                string respHeader = "text/html";
                string filename = courseFilename;
                return PhysicalFile(filename, respHeader);
            }
            else
            {
                string errorMessage = "There is no course " + name + ".";
                return NotFound(errorMessage); 
            }
        }
    }


}
