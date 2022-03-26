using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Diff_API_Task.Models
{
    public class ResponseModel
    {
        public string DiffResultType { get; set; }
        public List<OffsetLength> Diffs { get; set; }
    }
}
