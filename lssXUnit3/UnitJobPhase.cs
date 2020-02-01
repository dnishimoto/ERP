using lssWebApi2.EntityFramework;
using lssWebApi2.Enumerations;
using lssWebApi2.JobPhaseDomain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using Xunit.Abstractions;

namespace lssWebApi2.JobPhaseDomain
{

    public class UnitJobPhase
    {

        private readonly ITestOutputHelper output;

        public UnitJobPhase(ITestOutputHelper output)
        {
            this.output = output;
        }
        [Fact]
        public async Task TestAddUpdatDelete()
        {
            JobPhaseModule JobPhaseMod = new JobPhaseModule();
            JobMaster jobMaster = await JobPhaseMod.JobMaster.Query().GetEntityById(3);
            Contract contract = await JobPhaseMod.Contract.Query().GetEntityById(5);
            JobCostType jobCostType = await JobPhaseMod.JobCostType.Query().GetEntityById(4);
     
           JobPhaseView view = new JobPhaseView()
            {
               JobMasterId=jobMaster.JobMasterId,
               JobDescription=jobMaster.JobDescription,
               ContractId=contract.ContractId,
               ContractTitle=contract.Title,
               PhaseGroup=1,
               Phase="Site Preparation",
               JobCostTypeId=jobCostType.JobCostTypeId,
               CostCode=jobCostType.CostCode
            };
            NextNumber nnNextNumber = await JobPhaseMod.JobPhase.Query().GetNextNumber();

            view.JobPhaseNumber = nnNextNumber.NextNumberValue;

            JobPhase jobPhase = await JobPhaseMod.JobPhase.Query().MapToEntity(view);

            JobPhaseMod.JobPhase.AddJobPhase(jobPhase).Apply();

            JobPhase newJobPhase = await JobPhaseMod.JobPhase.Query().GetEntityByNumber(view.JobPhaseNumber);

            Assert.NotNull(newJobPhase);

            newJobPhase.Phase = "JobPhase Test Update";

            JobPhaseMod.JobPhase.UpdateJobPhase(newJobPhase).Apply();

            JobPhaseView updateView = await JobPhaseMod.JobPhase.Query().GetViewById(newJobPhase.JobPhaseId);

            Assert.Same(updateView.Phase, "JobPhase Test Update");
              JobPhaseMod.JobPhase.DeleteJobPhase(newJobPhase).Apply();
            JobPhase lookupJobPhase= await JobPhaseMod.JobPhase.Query().GetEntityById(view.JobPhaseId);

            Assert.Null(lookupJobPhase);
        }
       
      

    }
}
