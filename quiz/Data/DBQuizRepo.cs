using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using quiz.Models;

namespace quiz.Data
{
    public class DBQuizRepo : IQuizRepo
    {
        private readonly QuizDBContext _dbContext;
        public DBQuizRepo(QuizDBContext dbContext)
        {
            _dbContext = dbContext;
        }
        public IEnumerable<Mark> GetAllMarks()
        {
            IEnumerable<Mark> marks = _dbContext.Marks.ToList<Mark>();
            return marks;
        }
        public IEnumerable<Staff> GetAllStaff()
        {
            IEnumerable<Staff> staffs = _dbContext.Staff.ToList<Staff>();
            return staffs;
        }
        public IEnumerable<Student> GetAllStudents()
        {
            IEnumerable<Student> students = _dbContext.Students.ToList<Student>();
            return students;
        }
        public bool MarkExists(int id)
        {
            IEnumerable<Mark> marks = _dbContext.Marks.ToList<Mark>();
            IEnumerable<int> markIDs = marks.Select(e => e.Id);
            if (markIDs.Contains(id))
            {
                return true;
            }
            else
            {
                return false;
            }

        }
        public Mark AddMark(Mark mark)
        {
            EntityEntry<Mark> e = _dbContext.Marks.Add(mark);
            Mark m = e.Entity;
            _dbContext.SaveChanges();
            return m;
        }
        public Mark GetMarkByID(int id)
        {
            IEnumerable<Mark> marks = _dbContext.Marks.ToList<Mark>();
            Mark m = marks.First(e => e.Id == id);
            return m;
            
        }
        public void SaveChanges()
        {
            _dbContext.SaveChanges();
        }

        public bool ValidStaff(int id, string password)
        {
            Staff s = _dbContext.Staff.FirstOrDefault(e => e.Id == id && e.Password == password);
            if (s == null)
                return false;
            else
                return true;
        }
        public bool ValidStudent(int id, string password)
        {
            Student s = _dbContext.Students.FirstOrDefault(e => e.Id == id && e.Password == password);
            if (s == null)
                return false;
            else
                return true;
        }
    }
}
