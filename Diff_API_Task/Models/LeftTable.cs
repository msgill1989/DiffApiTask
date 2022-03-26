using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Diff_API_Task.Models
{
    public class LeftTable
    {
        [Key]
        public int Id { get; set; }

        public string DiffData { get; set; }
    }
}
