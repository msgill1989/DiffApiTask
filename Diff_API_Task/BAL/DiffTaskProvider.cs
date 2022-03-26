using Diff_API_Task.DAL;
using Diff_API_Task.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Diff_API_Task.BAL
{
    public class DiffTaskProvider : IDiffTaskProvider
    {
        private readonly IRepository _repository;
        public DiffTaskProvider(IRepository repository)
        {
            this._repository = repository;
        }

        public async Task AddUpdateDiffAsync(int id, RequestModel req, string table)
        {
            try
            {
                var diffData = req.Data;
                await _repository.AddUpdateDataAsync(id, diffData, table);
            }
            catch (Exception)
            {
                throw;
            }
            
        }

        public async Task<ResponseModel> GetDataAsync(int id)
        {
            try
            {
                int offset = 0;
                int length = 0;
                ResponseModel res = new ResponseModel();
                List<OffsetLength> lstOffset = new List<OffsetLength>(); 
                var leftData = await _repository.GetDataAsync(id, "left");
                var rightData = await _repository.GetDataAsync(id, "right");

                if (leftData == null || rightData == null)
                    throw new KeyNotFoundException();
                char[] leftArr = leftData.ToCharArray();
                char[] rightArr = rightData.ToCharArray();

                if (leftArr.Length != rightArr.Length)
                {
                    return new ResponseModel() { DiffResultType = "SizeDoNotMatch" };
                }

                for (int i = 0; i < leftArr.Length; i++)
                {
                    if (leftArr[i] != rightArr[i])
                    {
                        offset = i; 
                        for (int j = i; j <= leftArr.Length; j++)
                        {
                            if (j == leftArr.Length || leftArr[j] == rightArr[j])
                            {
                                lstOffset.Add(new OffsetLength() { Offset = offset, Length = length});
                                i = j-1;
                                length = 0;
                                break;
                            }
                            length++;
                        }
                    }
                }

                if (lstOffset.Count == 0)
                {
                    return new ResponseModel() { DiffResultType = "Equals" };
                }

                res.DiffResultType = "ContentDoNotMatch";
                res.Diffs = lstOffset;
                return res;
            }
            catch (Exception ex)
            {
                throw;
            }
        }

    }
}
