using lssWebApi2.AccountPayableDomain;
using lssWebApi2.Services;
using lssWebApi2.EntityFramework;
using System.Collections.Generic;
using System;
using lssWebApi2.ProjectManagementMilestoneDomain;
using lssWebApi2.Enumerations;

namespace lssWebApi2.ProjectManagementMilestoneDomain
{

public class FluentProjectManagementMilestone :IFluentProjectManagementMilestone
    {
 private UnitOfWork unitOfWork;
        private CreateProcessStatus processStatus;

        public FluentProjectManagementMilestone(UnitOfWork paramUnitOfWork) { unitOfWork = paramUnitOfWork; }
        public IFluentProjectManagementMilestoneQuery Query()
        {
            return new FluentProjectManagementMilestoneQuery(unitOfWork) as IFluentProjectManagementMilestoneQuery;
        }
        public IFluentProjectManagementMilestone Apply() {
			try{
            if (this.processStatus == CreateProcessStatus.Insert || this.processStatus == CreateProcessStatus.Update || this.processStatus == CreateProcessStatus.Delete)
            { unitOfWork.CommitChanges(); }
            return this as IFluentProjectManagementMilestone;
		    }
            catch (Exception ex) { throw new Exception("Apply", ex); }
        }
        public IFluentProjectManagementMilestone AddProjectManagementMilestones(List<ProjectManagementMilestone> newObjects)
        {
            unitOfWork.projectManagementMilestoneRepository.AddObjects(newObjects);
            this.processStatus = CreateProcessStatus.Insert;
            return this as IFluentProjectManagementMilestone;
        }
        public IFluentProjectManagementMilestone UpdateProjectManagementMilestones(IList<ProjectManagementMilestone> newObjects)
        {
            foreach (var item in newObjects)
            {
                unitOfWork.projectManagementMilestoneRepository.UpdateObject(item);
            }
            this.processStatus = CreateProcessStatus.Update;
            return this as IFluentProjectManagementMilestone;
        }
        public IFluentProjectManagementMilestone AddProjectManagementMilestone(ProjectManagementMilestone newObject) {
            unitOfWork.projectManagementMilestoneRepository.AddObject(newObject);
            this.processStatus = CreateProcessStatus.Insert;
            return this as IFluentProjectManagementMilestone;
        }
        public IFluentProjectManagementMilestone UpdateProjectManagementMilestone(ProjectManagementMilestone updateObject) {
            unitOfWork.projectManagementMilestoneRepository.UpdateObject(updateObject);
            this.processStatus = CreateProcessStatus.Update;
            return this as IFluentProjectManagementMilestone;

        }
        public IFluentProjectManagementMilestone DeleteProjectManagementMilestone(ProjectManagementMilestone deleteObject) {
            unitOfWork.projectManagementMilestoneRepository.DeleteObject(deleteObject);
            this.processStatus = CreateProcessStatus.Delete;
            return this as IFluentProjectManagementMilestone;
        }
   	public IFluentProjectManagementMilestone DeleteProjectManagementMilestones(List<ProjectManagementMilestone> deleteObjects)
        {
            unitOfWork.projectManagementMilestoneRepository.DeleteObjects(deleteObjects);
            this.processStatus = CreateProcessStatus.Delete;
            return this as IFluentProjectManagementMilestone;
        }
    }
}
