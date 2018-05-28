using ERP_Core2.EntityFramework;
using MillenniumERP.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using Xunit.Abstractions;

namespace ERP_Core2.ProjectManagementDomain
{
    


        public class UnitTestProjectManagement
        {
            UnitOfWork unitOfWork = new UnitOfWork();
            private readonly ITestOutputHelper output;

        public UnitTestProjectManagement(ITestOutputHelper output)
        {
            this.output = output;
        }


        [Fact]
            public void TestGetMileStones()
            {
            int projectId = 1;

            Task<IQueryable<ProjectManagementProject>> resultTask = unitOfWork.projectManagementProjectRepository.GetMilestones(projectId);
            int count = 0;
            foreach (var item in resultTask.Result)
            {
                foreach (var milestone in item.ProjectManagementMilestones)
                {
                    output.WriteLine($"{milestone.MilestoneName}");
                    count++;
                }
            }
            Assert.True(count>0);
            }

        }

}
