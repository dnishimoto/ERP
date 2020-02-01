using lssWebApi2.Services;
using lssWebApi2.EntityFramework;
using lssWebApi2.Enumerations;
using lssWebApi2.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using lssWebApi2.AddressBookDomain;
using System.Collections;
using lssWebApi2.ProjectManagementDomain;
using lssWebApi2.MapperAbstract;

namespace lssWebApi2.ProjectManagementDomain
{
    public class FluentProjectManagementProjectQuery : MapperAbstract<ProjectManagementProject,ProjectManagementProjectView>, IFluentProjectManagementProjectQuery
    {
        private UnitOfWork _unitOfWork;
        public FluentProjectManagementProjectQuery(UnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public override async Task<ProjectManagementProject> MapToEntity(ProjectManagementProjectView inputObject)
        {

            ProjectManagementProject outObject = mapper.Map<ProjectManagementProject>(inputObject);
            await Task.Yield();
            return outObject;
        }

        public override async Task<IList<ProjectManagementProject>> MapToEntity(IList<ProjectManagementProjectView> inputObjects)
        {
            IList<ProjectManagementProject> list = new List<ProjectManagementProject>();

            foreach (var item in inputObjects)
            {
                ProjectManagementProject outObject = mapper.Map<ProjectManagementProject>(item);
                list.Add(outObject);
            }
            await Task.Yield();
            return list;

        }

        public override async Task<ProjectManagementProjectView> MapToView(ProjectManagementProject inputObject)
        {

            ProjectManagementProjectView outObject = mapper.Map<ProjectManagementProjectView>(inputObject);
            await Task.Yield();
            return outObject;
        }

        public async Task<RollupTaskToProjectView> GetTaskToProjectRollupViewById(long? projectId)
        {
            return await _unitOfWork.projectManagementProjectRepository.GetTaskToProjectRollupViewById(projectId);
        }


        public async Task<NextNumber> GetNextNumber()
        {
            return await _unitOfWork.nextNumberRepository.GetNextNumber(TypeOfProjectManagementProject.ProjectNumber.ToString());
        }
        public override async Task<ProjectManagementProjectView> GetViewById(long? projectManagementProjectId)
        {
            ProjectManagementProject detailItem = await _unitOfWork.projectManagementProjectRepository.GetEntityById(projectManagementProjectId);

            return await MapToView(detailItem);
        }
        public async Task<ProjectManagementProjectView> GetViewByNumber(long projectManagementProjectNumber)
        {
            ProjectManagementProject detailItem = await _unitOfWork.projectManagementProjectRepository.GetEntityByNumber(projectManagementProjectNumber);

            return await MapToView(detailItem);
        }

        public override async Task<ProjectManagementProject> GetEntityById(long? projectManagementProjectId)
        {
            return await _unitOfWork.projectManagementProjectRepository.GetEntityById(projectManagementProjectId);

        }
        public async Task<ProjectManagementProject> GetEntityByNumber(long projectManagementProjectNumber)
        {
            return await _unitOfWork.projectManagementProjectRepository.GetEntityByNumber(projectManagementProjectNumber);
        }


    }
}
