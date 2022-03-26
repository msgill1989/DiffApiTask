using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Diff_API_Task.DAL
{
    public class Repository : IRepository
    {
        private readonly IDiffTaskDBContext _dbContext;
        public Repository(IDiffTaskDBContext dbContext)
        {
            this._dbContext = dbContext;
        }

        public async Task AddUpdateDataAsync(int id, string diffData, string tableName)
        {
            try
            {
                switch (tableName)
                {
                    case "left":
                        var responseLeft = _dbContext.LeftTable.FirstOrDefault(x => x.Id == id);
                        if (responseLeft != null)
                        {
                            responseLeft.DiffData = diffData;
                        }
                        else
                        {
                            _dbContext.LeftTable.Add(new Models.LeftTable() { Id = id, DiffData = diffData });
                        }
                        await _dbContext.SaveChangesAsync();
                        break;
                    case "right":
                        var responseRight = _dbContext.RightTable.FirstOrDefault(x => x.Id == id);
                        if (responseRight != null)
                        {
                            responseRight.DIffData = diffData;
                        }
                        else
                        {
                            _dbContext.RightTable.Add(new Models.RightTable() { Id = id, DIffData = diffData });
                        }
                        await _dbContext.SaveChangesAsync();
                        break;
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<string> GetDataAsync(int id, string tableName)
        {
            try
            {
                string diffResponse = null;
                switch (tableName)
                {
                    case "left":
                        var leftResponse = await _dbContext.LeftTable.FindAsync(id); //Where(x => x.Id == id).FirstOrDefault();
                        if(leftResponse != null)
                        diffResponse = leftResponse.DiffData;
                        break;
                    case "right":
                        var rightResponse = await _dbContext.RightTable.FindAsync(id);
                        if(rightResponse != null)
                        diffResponse = rightResponse.DIffData;
                        break;
                }
                return diffResponse;
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
