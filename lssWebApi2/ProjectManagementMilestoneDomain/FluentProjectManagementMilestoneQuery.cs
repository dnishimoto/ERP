using lssWebApi2.AutoMapper;
using lssWebApi2.Services;
using lssWebApi2.EntityFramework;
using lssWebApi2.Enumerations;
using System.Collections.Generic;
using System.Threading.Tasks;
using lssWebApi2.MapperAbstract;
using lssWebApi2.ProjectManagementDomain;
using System.Linq;
using System;

namespace lssWebApi2.ProjectManagementMilestoneDomain
{
    public class FluentProjectManagementMilestoneQuery : MapperAbstract<ProjectManagementMilestone, ProjectManagementMilestoneView>, IFluentProjectManagementMilestoneQuery
    {
        private UnitOfWork _unitOfWork = null;
        public FluentProjectManagementMilestoneQuery() { }
        public FluentProjectManagementMilestoneQuery(UnitOfWork unitOfWork) { _unitOfWork = unitOfWork; }

        public async Task<IQueryable<ProjectManagementMilestone>> GetEntitiesByProjectId(long projectId)
        {
            try
            {
                IQueryable<ProjectManagementMilestone> query = await _unitOfWork.projectManagementMilestoneRepository.GetEntitiesByProjectId(projectId);
                return query;
            }
            catch (Exception ex)
            {
                throw new Exception("GetMilestones", ex);
            }
        }
        public override async Task<ProjectManagementMilestone> MapToEntity(ProjectManagementMilestoneView inputObject)
        {
            Mapper mapper = new Mapper();
            ProjectManagementMilestone outObject = mapper.Map<ProjectManagementMilestone>(inputObject);
            await Task.Yield();
            return outObject;
        }

        public override async Task<IList<ProjectManagementMilestone>> MapToEntity(IList<ProjectManagementMilestoneView> inputObjects)
        {
            IList<ProjectManagementMilestone> list = new List<ProjectManagementMilestone>();
            Mapper mapper = new Mapper();
            foreach (var item in inputObjects)
            {
                ProjectManagementMilestone outObject = mapper.Map<ProjectManagementMilestone>(item);
                list.Add(outObject);
            }
            await Task.Yield();
            return list;

        }

        public override async Task<ProjectManagementMilestoneView> MapToView(ProjectManagementMilestone inputObject)
        {
            Mapper mapper = new Mapper();
            ProjectManagementMilestoneView outObject = mapper.Map<ProjectManagementMilestoneView>(inputObject);
   
            ProjectManagementProject project = await _unitOfWork.projectManagementProjectRepository.GetEntityById(inputObject.ProjectId);

            outObject.ProjectName = project.ProjectName;
         
            return outObject;
        }


        public async Task<NextNumber> GetNextNumber()
        {
            return await _unitOfWork.nextNumberRepository.GetNextNumber(TypeOfProjectManagementMilestone.MilestoneNumber.ToString());
        }
        public override async Task<ProjectManagementMilestoneView> GetViewById(long? projectManagementMilestoneId)
        {
            ProjectManagementMilestone detailItem = await _unitOfWork.projectManagementMilestoneRepository.GetEntityById(projectManagementMilestoneId);

            return await MapToView(detailItem);
        }
        public async Task<ProjectManagementMilestoneView> GetViewByNumber(long projectManagementMilestoneNumber)
        {
            ProjectManagementMilestone detailItem = await _unitOfWork.projectManagementMilestoneRepository.GetEntityByNumber(projectManagementMilestoneNumber);

            return await MapToView(detailItem);
        }

        public override async Task<ProjectManagementMilestone> GetEntityById(long? projectManagementMilestoneId)
        {
            return await _unitOfWork.projectManagementMilestoneRepository.GetEntityById(projectManagementMilestoneId);

        }
        public async Task<ProjectManagementMilestone> GetEntityByNumber(long projectManagementMilestoneNumber)
        {
            return await _unitOfWork.projectManagementMilestoneRepository.GetEntityByNumber(projectManagementMilestoneNumber);
        }
        public async Task<RollupTaskToMilestoneView> GetTaskToMilestoneRollupViewById(long? milestoneId)
        {
            return await _unitOfWork.projectManagementMilestoneRepository.GetTaskToMilestoneRollupViewById(milestoneId);
        }
    }
}
