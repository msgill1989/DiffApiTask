using Diff_API_Task.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Diff_API_Task.BAL
{
    public interface IDiffTaskProvider
    {
        Task AddUpdateDiffAsync(int id, RequestModel req, string table);
        Task<ResponseModel> GetDataAsync(int id);
    }
}
