using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Diff_API_Task.DAL
{
    public interface IRepository
    {
        Task AddUpdateDataAsync(int id, string diffData, string tableName);
        Task<string> GetDataAsync(int id, string tableName);
    }
}
