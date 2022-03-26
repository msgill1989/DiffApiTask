using Diff_API_Task.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Diff_API_Task.DAL
{
    public interface IDiffTaskDBContext
    {
        DbSet<LeftTable> LeftTable { get; set; }

        DbSet<RightTable> RightTable { get; set; }

        DatabaseFacade Db { get; }

        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
    }
}
