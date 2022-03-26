using Diff_API_Task.BAL;
using Diff_API_Task.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Diff_API_Task.Controllers
{
    public class TaskController : Controller
    {
        private readonly IDiffTaskProvider _diffTaskProvider;
        public TaskController(IDiffTaskProvider diffTaskProvider)
        {
            this._diffTaskProvider = diffTaskProvider;
        }

        [HttpPut("v1/diff/{id}/left")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult> UpdateLeft(int id, [FromBody]RequestModel req)
        {
            try
            {
                if (req == null || string.IsNullOrEmpty(req.Data))
                {
                    var details = ProblemDetailsFactory.CreateProblemDetails(HttpContext, 400, "Bad request", null, "There is some problem with the request");
                    return StatusCode(400, details);
                }
                else
                {
                    await _diffTaskProvider.AddUpdateDiffAsync(id,req,"left");
                    return StatusCode(StatusCodes.Status201Created);
                }

            }
            catch (Exception)
            {
                var details = ProblemDetailsFactory.CreateProblemDetails(HttpContext, 500, "Internal Server Error", null, "Some internal error happened. Please contact system administrator.");
                return StatusCode(500, details);
            }
        }

        [HttpPut("v1/diff/{id}/right")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult> UpdateRight(int id, [FromBody]RequestModel req)
        {
            try
            {
                if (string.IsNullOrEmpty(req.Data))
                {
                    var details = ProblemDetailsFactory.CreateProblemDetails(HttpContext, 400, "Bad request", null, "There is some problem with the request");
                    return StatusCode(404, details);
                }
                else
                {
                    await _diffTaskProvider.AddUpdateDiffAsync(id, req, "right");
                    return StatusCode(StatusCodes.Status201Created);
                }
            }
            catch (Exception)
            {
                var details = ProblemDetailsFactory.CreateProblemDetails(HttpContext, 500, "Internal Server Error", null, "Some internal error happened. Please contact system administrator.");
                return StatusCode(500, details);
            }
        }

        [HttpGet("v1/diff/{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<ResponseModel>> GetResult(int id)
        {
            try
            {
                return await _diffTaskProvider.GetDataAsync(id);
            }
            catch (KeyNotFoundException)
            {
                var details = ProblemDetailsFactory.CreateProblemDetails(HttpContext, 404, "NotFound", null, "Data not found.");
                return StatusCode(404, details);
            }
            catch (Exception ex)
            {
                var details = ProblemDetailsFactory.CreateProblemDetails(HttpContext, 500, "Internal Server Error", null, "Some internal error happened. Please contact system administrator.");
                return StatusCode(500, details);
            }
        }
    }
}
