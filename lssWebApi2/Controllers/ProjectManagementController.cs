using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ERP_Core2.ProjectManagementDomain;
using lssWebApi2.EntityFramework;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace lssWebApi2.Controllers
{
    [Route("api/[controller]")]
    public class ProjectManagementController : Controller
    {
        // GET: api/<controller>
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }
        [HttpGet]
        [Route("GetMilestonesByProjectId/{projectId}")]
        [ProducesResponseType(typeof(List<ProjectManagementMilestoneView>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetMilestonesByProjectId(long projectId)
        {
            ProjectManagementModule pmMod = new ProjectManagementModule();
            IQueryable<ProjectManagementProject> query = await pmMod.ProjectManagement.Query().GetMilestones(projectId);
            int count = 0;
            List<ProjectManagementMilestoneView> list = new List<ProjectManagementMilestoneView>();
            foreach (var item in query)
            {
                foreach (ProjectManagementMilestones milestone in item.ProjectManagementMilestones)
                {
                    ProjectManagementMilestoneView view = await pmMod.ProjectManagement.Query().MaptoMilestoneView(milestone);
                    count++;
                    list.Add(view);
                }
            }
            return Ok(list);
        }
        [HttpGet]
        [Route("GetTasksByProjectId/{projectId}")]
        [ProducesResponseType(typeof(List<ProjectManagementTaskView>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetTasksByProjectId ( long projectId)
        { 
             

        ProjectManagementModule pmMod = new ProjectManagementModule();

        IQueryable<ProjectManagementTask> query = await pmMod.ProjectManagement.Query().GetTasksByProjectId(projectId);

        int count = 0;
            List<ProjectManagementTaskView> list = new List<ProjectManagementTaskView>();
            foreach (var item in query)
            {

                ProjectManagementTaskView view = await pmMod.ProjectManagement.Query().MaptoTaskView(item);
                count++;
                list.Add(view);

            }

        return Ok(list);

}

        // GET api/<controller>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<controller>
        [HttpPost]
        public void Post([FromBody]string value)
        {
        }

        // PUT api/<controller>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/<controller>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
