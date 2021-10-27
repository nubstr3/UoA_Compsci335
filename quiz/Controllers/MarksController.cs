using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.IO;
using quiz.Models;
using quiz.Dtos;
using quiz.Data;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;


namespace quiz.Controllers
{
    [Route("quizapi")]
    [ApiController]
    public class MarksController : Controller
    {
        private readonly IQuizRepo _repository;
        public MarksController(IQuizRepo repository)
        {
            _repository = repository;
        }

        [HttpGet("GetMarks")]
        public ActionResult<IEnumerable<MarkOut>> GetMarks()
        {
            IEnumerable<Mark> marks = _repository.GetAllMarks();
            IEnumerable<MarkOut> m = marks.Select(e => new MarkOut
            {
                id = e.Id,
                a1 = e.A1,
                a2 = e.A2
            });
            return Ok(m);
        } 
        [HttpGet("GetMarkByID/{ID}")]
        public ActionResult<MarkOut> GetMarkByID(int id)
        {
            IEnumerable<Mark> marks = _repository.GetAllMarks();
            IEnumerable<Mark> marks1 = marks.Where(e => e.Id == id);
            if (_repository.MarkExists(id))
            {
                IEnumerable<MarkOut> m = marks1.Select(e => new MarkOut { id = e.Id, a1 = e.A1, a2 = e.A2 });
                return Ok(m);
            }
            else
            {
                return NotFound("No record for the student with ID number " + id);
            }
        }

        [HttpPost("SetMark")]
        public ActionResult<Mark> SetMark(MarkIn mark)
        {
            IEnumerable<Mark> marks = _repository.GetAllMarks();
            if (_repository.MarkExists(mark.id))
            {
                Mark m = _repository.GetMarkByID(mark.id);
                m.A1 = mark.a1;
                m.A2 = mark.a2;
                _repository.SaveChanges();
                return Ok(m);
            }
            else
            {
                Mark m = new Mark { Id = mark.id, A1 = mark.a1, A2 = mark.a2 };
                Mark addedmark = _repository.AddMark(m);
                return Ok(addedmark);

            }
        }

        [Authorize(AuthenticationSchemes ="StaffAuthentication")]
        [Authorize(Policy ="StaffOnly")]
        [HttpGet("GetMarksAuth")]
        public ActionResult<IEnumerable<MarkOut>> GetMarksAuth()
        {
            IEnumerable<Mark> marks = _repository.GetAllMarks();
            IEnumerable<MarkOut> m = marks.Select(e => new MarkOut
            {
                id = e.Id,
                a1 = e.A1,
                a2 = e.A2
            });
            return Ok(m);
        }

        [Authorize(AuthenticationSchemes = "StudentAuthentication,StaffAuthentication", Policy = "AnyUser")]
        [HttpGet("GetMarkByIDAuth/{ID}")]
        public ActionResult<MarkOut> GetMarkByIDAuth(int id)
        {
            ClaimsIdentity ci = HttpContext.User.Identities.FirstOrDefault();
            Claim stu = ci.FindFirst("student");
            Claim sta = ci.FindFirst("staff");
            if (stu != null)
            {
                string stuId = stu.Value;
                int studentsId = int.Parse(stuId);

                IEnumerable<Mark> marks = _repository.GetAllMarks();
                IEnumerable<Mark> marks1 = marks.Where(e => e.Id == studentsId);
                IEnumerable<MarkOut> m = marks1.Select(e => new MarkOut { id = e.Id, a1 = e.A1, a2 = e.A2 });
                return Ok(m);

            }
            else
            {
                string staId = sta.Value;
                int studentsId = int.Parse(staId);

                if (_repository.MarkExists(id))
                {
                    IEnumerable<Mark> marks = _repository.GetAllMarks();
                    IEnumerable<Mark> marks1 = marks.Where(e => e.Id == id);
                    IEnumerable<MarkOut> m = marks1.Select(e => new MarkOut { id = e.Id, a1 = e.A1, a2 = e.A2 });
                    return Ok(m);
                }
                else
                {
                    return Ok("No record for the student with ID number " + id);
                }

            }

        }
        [Authorize(AuthenticationSchemes = "StaffAuthentication", Policy = "StaffOnly")]
        [HttpPost("SetMarkAuth")]
        public ActionResult<MarkOut> SetMarkAuth(MarkIn mark)
        {
            IEnumerable<Mark> marks = _repository.GetAllMarks();

            if (_repository.MarkExists(mark.id))
            {
                Mark m = _repository.GetMarkByID(mark.id);
                m.A1 = mark.a1;
                m.A2 = mark.a2;
                _repository.SaveChanges();

                Response.Headers.Add("Location", "http://localhost:8080/quizapi/GetMarkByIDAuth/" + mark.id);
                return Ok(m);
            }
            else
            {
                Mark m = new Mark { Id = mark.id, A1 = mark.a1, A2 = mark.a2 };
                Mark addedmark = _repository.AddMark(m);

                Response.Headers.Add("Location", "http://localhost:8080/quizapi/GetMarkByIDAuth/" + mark.id);
                return Ok(addedmark);

            }
        }
    }
}
