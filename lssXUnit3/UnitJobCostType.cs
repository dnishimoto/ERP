using lssWebApi2.EntityFramework;
using lssWebApi2.Enumerations;
using lssWebApi2.JobCostTypeDomain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using Xunit.Abstractions;

namespace lssWebApi2.JobCostTypeDomain
{

    public class UnitJobCostType
    {

        private readonly ITestOutputHelper output;

        public UnitJobCostType(ITestOutputHelper output)
        {
            this.output = output;
        }
        [Fact]
        public async Task TestAddUpdatDelete()
        {
            JobCostTypeModule JobCostTypeMod = new JobCostTypeModule();

           JobCostTypeView view = new JobCostTypeView()
            {
                CostCode="MISC",
                Description ="Miscellaneous"
            };
            NextNumber nnNextNumber = await JobCostTypeMod.JobCostType.Query().GetNextNumber();

            view.JobCostTypeNumber = nnNextNumber.NextNumberValue;

            JobCostType jobCostType = await JobCostTypeMod.JobCostType.Query().MapToEntity(view);

            JobCostTypeMod.JobCostType.AddJobCostType(jobCostType).Apply();

            JobCostType newJobCostType = await JobCostTypeMod.JobCostType.Query().GetEntityByNumber(view.JobCostTypeNumber);

            Assert.NotNull(newJobCostType);

            newJobCostType.Description = "JobCostType Test Update";

            JobCostTypeMod.JobCostType.UpdateJobCostType(newJobCostType).Apply();

            JobCostTypeView updateView = await JobCostTypeMod.JobCostType.Query().GetViewById(newJobCostType.JobCostTypeId);

            Assert.Same(updateView.Description, "JobCostType Test Update");
              JobCostTypeMod.JobCostType.DeleteJobCostType(newJobCostType).Apply();
            JobCostType lookupJobCostType= await JobCostTypeMod.JobCostType.Query().GetEntityById(view.JobCostTypeId);

            Assert.Null(lookupJobCostType);
        }
       
      

    }
}
