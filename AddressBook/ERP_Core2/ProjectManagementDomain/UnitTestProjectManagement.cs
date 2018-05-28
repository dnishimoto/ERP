using ERP_Core2.EntityFramework;
using MillenniumERP.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace ERP_Core2.ProjectManagementDomain
{
    


        public class UnitTestProjectManagement
        {
            UnitOfWork unitOfWork = new UnitOfWork();
      
        [Fact]
            public void TestGetMileStones()
            {
            int projectId = 1;

            Task<IQueryable<ProjectManagementMilestone>> resultTask = unitOfWork.projectManagementProjectRepository.GetMilestones(1);

            foreach (var item in resultTask.Result)
            {
                Console.WriteLine($"{item.MilestoneName}");
            }
            //Assert.Equal(4, Add(2, 2));
            }

        }

}
