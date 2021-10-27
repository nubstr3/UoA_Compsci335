using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace quiz.Models
{
    public class Staff
    {
        [Key]
        public int Id { get; set; }
        public string Password { get; set; }

    }
}
