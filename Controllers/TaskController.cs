using System;
using Microsoft.AspNetCore.Mvc;
using poprawa.DAL;
using poprawa.Exceptions;

namespace poprawa.Controllers
{
    [ApiController]
    [Route("api/tasks")]
    public class TaskController : ControllerBase
    {
        private readonly IDbService _dbService;

        public TaskController(IDbService dbService)
        {
            _dbService = dbService;
        }
        
        [HttpGet]
        [Route("{memberId}")]
        public IActionResult GetMemberTasks(int memberId)
        {
            IActionResult response;
            try
            {
                response = Ok(_dbService.GetMemberTasks(memberId));
            }
            catch (TeamMemberNotFoundException e)
            {
                response = NotFound(e.Message);
            }
            catch (Exception e)
            {
                response = BadRequest(e.Message);
            }
            
            return response;
        }

        [HttpDelete]
        [Route("{projectId}")]
        public IActionResult DeleteProject(int projectId)
        {
            IActionResult response;
            try
            {
                _dbService.DeleteProject(projectId);
                response = Ok();
            }
            catch (ProjectNotFoundExceptionException e)
            {
                response = NotFound(e.Message);
            }
            catch (Exception e)
            {
                response = BadRequest(e.Message);
            }

            return response;
        }
    }
}