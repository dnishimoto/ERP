using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using lssWebApi2.ProjectManagementDomain;
using lssWebApi2.EntityFramework;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using lssWebApi2.ProjectManagementWorkOrderDomain;
using lssWebApi2.ProjectManagementTaskDomain;
using lssWebApi2.EmployeeDomain;
using lssWebApi2.ProjectManagementMilestoneDomain;

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

        
        [HttpDelete]
        [Route("DeleteWorkOrderToEmployee")]
        [ProducesResponseType(typeof(bool), StatusCodes.Status200OK)]
        public async Task<IActionResult> DeleteWorkOrderToEmployee([FromBody]List<ProjectManagementWorkOrderToEmployee> list)
        {
            try
            {
                ProjectManagementModule pmMod = new ProjectManagementModule();
                if (list.Count()>0)
                {
                    //long ? workOrderId = list.FirstOrDefault<ProjectManagementWorkOrderToEmployee>().WorkOrderId;
                    pmMod.WorkOrderToEmployee.DeleteProjectManagementWorkOrderToEmployees(list).Apply();
                }
                await Task.Yield();
                return Ok(true);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
                 
        [HttpDelete]
        [Route("DeleteWorkOrder/{workOrderId}")]
        [ProducesResponseType(typeof(ProjectManagementWorkOrderView), StatusCodes.Status200OK)]
        public async Task<IActionResult> DeleteWorkOrder(long workOrderId)
        {
            ProjectManagementModule pmMod = new ProjectManagementModule();
            ProjectManagementWorkOrder workOrder = await pmMod.WorkOrder.Query().GetEntityById(workOrderId);
            ProjectManagementWorkOrderView view = await pmMod.WorkOrder.Query().MapToView(workOrder);
            pmMod.WorkOrder.DeleteProjectManagementWorkOrder(workOrder).Apply();
            return Ok(view);
        }
       
        [HttpDelete]
        [Route("DeleteMilestone/{milestoneId}")]
        [ProducesResponseType(typeof(ProjectManagementProjectView), StatusCodes.Status200OK)]
        public async Task<IActionResult> DeleteMilestone(long milestoneId)
        {
            ProjectManagementModule pmMod = new ProjectManagementModule();
            ProjectManagementMilestone milestone = await pmMod.Milestone.Query().GetEntityById(milestoneId);
            ProjectManagementMilestoneView view = await pmMod.Milestone.Query().MapToView(milestone);
            pmMod.Milestone.DeleteProjectManagementMilestone(milestone).Apply();
            return Ok(view);
        }
      
        [HttpDelete]
        [Route("DeleteProject/{projectId}")]
        [ProducesResponseType(typeof(ProjectManagementProjectView), StatusCodes.Status200OK)]
        public async Task<IActionResult> DeleteProject(long projectId)
        {
            ProjectManagementModule pmMod = new ProjectManagementModule();
            ProjectManagementProject project = await pmMod.Project.Query().GetEntityById(projectId);
            ProjectManagementProjectView view = await pmMod.Project.Query().MapToView(project);
            pmMod.Project.DeleteProject(project).Apply();
           
            return Ok(view);
        }

        [HttpPost]
        [Route("CreateMilestone")]
        [ProducesResponseType(typeof(ProjectManagementMilestoneView), StatusCodes.Status200OK)]
        public async Task<IActionResult> CreateMilestone([FromBody]ProjectManagementMilestone milestone)
        {
            ProjectManagementModule pmMod = new ProjectManagementModule();

            NextNumber nnMileStone = await pmMod.Milestone.Query().GetNextNumber();

            milestone.MileStoneNumber = nnMileStone.NextNumberValue;
      
            pmMod.Milestone.AddProjectManagementMilestone(milestone).Apply();

            ProjectManagementMilestone queryMilestone = await pmMod.Milestone.Query().GetEntityByNumber(milestone.MileStoneNumber??0);

            ProjectManagementMilestoneView view = await pmMod.Milestone.Query().MapToView(queryMilestone);

            return Ok(view);
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
                long ? workOrderId = list.FirstOrDefault<ProjectManagementWorkOrderToEmployee>().WorkOrderId;
                pmMod.WorkOrderToEmployee.AddProjectManagementWorkOrderToEmployees(list).Apply();

                IEnumerable<EmployeeView> query =
                    await pmMod.Employee.Query().GetEntitiesByWorkOrderId(workOrderId??0);

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

            NextNumber nnWorkOrder = await pmMod.WorkOrder.Query().GetNextNumber();

            newWorkOrder.WorkOrderNumber = nnWorkOrder.NextNumberValue;
           

            pmMod.WorkOrder.AddProjectManagementWorkOrder(newWorkOrder).Apply();

            ProjectManagementWorkOrder workOrder = await pmMod.WorkOrder.Query().GetEntityByNumber(nnWorkOrder.NextNumberValue);

            ProjectManagementWorkOrderView view = await pmMod.WorkOrder.Query().MapToView(workOrder);

            return Ok(view);
        }
        [HttpPost]
        [Route("CreateProject")]
        [ProducesResponseType(typeof(ProjectManagementProjectView), StatusCodes.Status200OK)]
        public async Task<IActionResult> CreateProject([FromBody] ProjectManagementProject newProject)
        {
            ProjectManagementModule pmMod = new ProjectManagementModule();

            NextNumber nnProject = await pmMod.Project.Query().GetNextNumber();

            newProject.ProjectNumber = nnProject.NextNumberValue;
      
            pmMod.Project.AddProject(newProject).Apply();

            ProjectManagementProject projectSaved = await pmMod.Project.Query().GetEntityByNumber(nnProject.NextNumberValue);

            ProjectManagementProjectView view = await pmMod.Project.Query().MapToView(projectSaved);

            return Ok(view);

        }
        [HttpGet]
        [Route("GetTasksByMilestoneId/{milestoneId}")]
        [ProducesResponseType(typeof(List<ProjectManagementTaskView>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetTasksByMilestoneId(long milestoneId)
        {
            ProjectManagementModule pmMod = new ProjectManagementModule();

            IQueryable<ProjectManagementTask> query = await pmMod.Task.Query().GetEntitiesByMilestoneId(milestoneId);

            List<ProjectManagementTaskView> list = new List<ProjectManagementTaskView>();

       
            foreach (var item in query)
            {
                      ProjectManagementTaskView view = await pmMod.Task.Query().MapToView(item);
                    list.Add(view);

            }
            return Ok(list);
        }
        [HttpGet]
        [ProducesResponseType(typeof(List<ProjectManagementMilestoneView>), StatusCodes.Status200OK)]
        [Route("GetWorkOrdersByProjectId/{projectId}")]
        public async Task<IActionResult> GetWorkOrdersByProjectId(long projectId)
        {
            ProjectManagementModule pmMod = new ProjectManagementModule();

            IQueryable<ProjectManagementWorkOrder> query = await pmMod.WorkOrder.Query().GetEntitiesByProjectId(projectId);

            List<ProjectManagementWorkOrderView> list = new List<ProjectManagementWorkOrderView>();
            foreach (var workOrder in query)
            {
                ProjectManagementWorkOrderView view = await pmMod.WorkOrder.Query().MapToView(workOrder);
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
            IQueryable<ProjectManagementMilestone> query = await pmMod.Milestone.Query().GetEntitiesByProjectId(projectId);
            int count = 0;
            List<ProjectManagementMilestoneView> list = new List<ProjectManagementMilestoneView>();
            foreach (var item in query)
            {
                
                    ProjectManagementMilestoneView view = await pmMod.Milestone.Query().MapToView(item);
                    count++;
                    list.Add(view);
            
            }
            return Ok(list);
        }
        [HttpGet]
        [Route("GetTasksByProjectId/{projectId}")]
        [ProducesResponseType(typeof(List<ProjectManagementTaskView>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetTasksByProjectId ( long projectId)
        { 
             

        ProjectManagementModule pmMod = new ProjectManagementModule();

        IQueryable<ProjectManagementTask> query = await pmMod.Task.Query().GetEntitiesByProjectId(projectId);

        int count = 0;
            List<ProjectManagementTaskView> list = new List<ProjectManagementTaskView>();
            foreach (var item in query)
            {

                ProjectManagementTaskView view = await pmMod.Task.Query().MapToView(item);
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
