using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using quiz.Models;

namespace quiz.Data
{
    public interface IQuizRepo
    {
        IEnumerable<Mark> GetAllMarks();
        IEnumerable<Staff> GetAllStaff();
        IEnumerable<Student> GetAllStudents();
        bool MarkExists(int id);
        Mark AddMark(Mark mark);
        Mark GetMarkByID(int id);
        void SaveChanges();
        public bool ValidStaff(int id, string password);
        public bool ValidStudent(int id, string password);
    }
}
