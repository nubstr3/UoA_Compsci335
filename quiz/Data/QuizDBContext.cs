using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using quiz.Models;

namespace quiz.Data
{
    public class QuizDBContext : DbContext
    {
        public QuizDBContext() { }

        public QuizDBContext(DbContextOptions<QuizDBContext> options) : base(options) { }

        public DbSet<Mark> Marks { get; set; }
        public DbSet<Staff> Staff { get; set; }
        public DbSet<Student> Students { get; set; }


    }
}
