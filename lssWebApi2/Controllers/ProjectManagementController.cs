using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ERP_Core2.AddressBookDomain;
using ERP_Core2.ProjectManagementDomain;
using lssWebApi2.EntityFramework;
using lssWebApi2.ProjectManagementDomain.Repository;
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
        [HttpPost]
        [Route("CreateWorkOrderToEmployee")]
        [ProducesResponseType(typeof(List<EmployeeView>), StatusCodes.Status200OK)]
        public async Task<IActionResult> CreateWorkOrderToEmployee([FromBody]List<ProjectManagementWorkOrderToEmployee> list)
        {
            List<EmployeeView> retList = null;
            ProjectManagementModule pmMod = new ProjectManagementModule();

            if (list.Count()>0)
            {
                long workOrderId = list.FirstOrDefault<ProjectManagementWorkOrderToEmployee>().WorkOrderId;
                pmMod.ProjectManagement.AddWorkOrderEmployee(list).Apply();

                IEnumerable<EmployeeView> query =
                    await pmMod.ProjectManagement.Query().GetEmployeeByWorkOrderId(workOrderId);

                retList = new List<EmployeeView>(query);

            }
            return Ok(retList);
        }
        [HttpPost]
        [Route("CreateWorkOrder")]
        [ProducesResponseType(typeof(ProjectManagementWorkOrderView), StatusCodes.Status200OK)]
        public async Task<IActionResult> CreateWorkOrder([FromBody]ProjectManagementWorkOrder newWorkOrder)
        {
            ProjectManagementModule pmMod = new ProjectManagementModule();

            NextNumber nnWorkOrder = await pmMod.ProjectManagement.Query().GetWorkOrderNumber();

            newWorkOrder.WorkOrderNumber = nnWorkOrder.NextNumberValue;
           

            pmMod.ProjectManagement.AddWorkOrder(newWorkOrder).Apply();

            ProjectManagementWorkOrder workOrder = await pmMod.ProjectManagement.Query().GetWorkOrderByNumber(nnWorkOrder.NextNumberValue);

            ProjectManagementWorkOrderView view = await pmMod.ProjectManagement.Query().MapToWorkOrderView(workOrder);

            return Ok(view);
        }
        [HttpPost]
        [Route("CreateProject")]
        [ProducesResponseType(typeof(ProjectManagementProjectView), StatusCodes.Status200OK)]
        public async Task<IActionResult> CreateProject([FromBody] ProjectManagementProject newProject)
        {
            ProjectManagementModule pmMod = new ProjectManagementModule();

            NextNumber nnProject = await pmMod.ProjectManagement.Query().GetProjectNumber();

            newProject.ProjectNumber = nnProject.NextNumberValue;
      
            pmMod.ProjectManagement.AddProject(newProject).Apply();

            ProjectManagementProject projectSaved = await pmMod.ProjectManagement.Query().GetProjectByNumber(nnProject.NextNumberValue);

            ProjectManagementProjectView view = await pmMod.ProjectManagement.Query().MapToProjectView(projectSaved);

            return Ok(view);

        }
        [HttpGet]
        [Route("GetTasksByMilestoneId/{milestoneId}")]
        [ProducesResponseType(typeof(List<ProjectManagementTaskView>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetTasksByMilestoneId(long milestoneId)
        {
            ProjectManagementModule pmMod = new ProjectManagementModule();

            IQueryable<ProjectManagementMilestones> query = await pmMod.ProjectManagement.Query().GetTasksByMilestoneId(milestoneId);

            List<ProjectManagementTaskView> list = new List<ProjectManagementTaskView>();

       
            foreach (var item in query)
            {
                foreach (var task in item.ProjectManagementTask)
                {
                    ProjectManagementTaskView view = await pmMod.ProjectManagement.Query().MapToTaskView(task);
                    list.Add(view);
                }
            }
            return Ok(list);
        }
        [HttpGet]
        [ProducesResponseType(typeof(List<ProjectManagementMilestoneView>), StatusCodes.Status200OK)]
        [Route("GetWorkOrdersByProjectId/{projectId}")]
        public async Task<IActionResult> GetWorkOrdersByProjectId(long projectId)
        {
            ProjectManagementModule pmMod = new ProjectManagementModule();

            IQueryable<ProjectManagementWorkOrder> query = await pmMod.ProjectManagement.Query().GetWorkOrdersByProjectId(projectId);

            List<ProjectManagementWorkOrderView> list = new List<ProjectManagementWorkOrderView>();
            foreach (var workOrder in query)
            {
                ProjectManagementWorkOrderView view = await pmMod.ProjectManagement.Query().MapToWorkOrderView(workOrder);
                list.Add(view);
            }
            return Ok(list);
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
