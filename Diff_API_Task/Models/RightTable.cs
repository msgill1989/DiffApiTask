using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Diff_API_Task.Models
{
    public class RightTable
    {
        [Key]
        public int Id { get; set; }
        public string DIffData { get; set; }
    }
}
