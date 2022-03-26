using Diff_API_Task.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Diff_API_Task.DAL
{
    public class DiffTaskDBContext : DbContext, IDiffTaskDBContext
    {
        private readonly IOptions<DBOptions> _dbOptions;

        public DiffTaskDBContext(DbContextOptions<DiffTaskDBContext> options, IOptions<DBOptions> dbOptions = null) : base(options)
        {
            this._dbOptions = dbOptions;
        }

        public DbSet<LeftTable> LeftTable { get; set; }

        public DbSet<RightTable> RightTable { get; set; }

        public DatabaseFacade Db => Database;
    }
}
