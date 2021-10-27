using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace quiz.Models
{
    public class Mark
    {
        [Key]
        public int Id { get; set; }
        public float A1 { get; set; }
        public float A2 { get; set; }

    }
}
