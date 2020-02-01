using lssWebApi2.EntityFramework;
using lssWebApi2.Enumerations;
using lssWebApi2.JobCostLedgerDomain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using Xunit.Abstractions;

namespace lssWebApi2.JobCostLedgerDomain
{

    public class UnitJobCostLedger
    {

        private readonly ITestOutputHelper output;

        public UnitJobCostLedger(ITestOutputHelper output)
        {
            this.output = output;
        }
        [Fact]
        public async Task TestAddUpdatDelete()
        {
            JobCostLedgerModule JobCostLedgerMod = new JobCostLedgerModule();
            Contract contract = await JobCostLedgerMod.Contract.Query().GetEntityById(6);
            JobMaster jobMaster = await JobCostLedgerMod.JobMaster.Query().GetEntityById(40);
            JobPhase jobPhase = await JobCostLedgerMod.JobPhase.Query().GetEntityById(180);
            JobCostType jobCostType = await JobCostLedgerMod.JobCostType.Query().GetEntityById(2);

           JobCostLedgerView view = new JobCostLedgerView()
            {
                JobMasterId =jobMaster.JobMasterId,
                ContractId=contract.ContractId,
                EstimatedHours =0,
                EstimatedAmount =0,
                JobPhaseId =jobPhase.JobPhaseId,
                ActualHours=0,
                ActualCost =0,
                ProjectedHours =0,
                CommittedHours =0,
                CommittedAmount =100M,
                Description ="JC Ledger Detail",
                TransactionType ="PO",
                Source ="Job Costing",
                JobCostTypeId = jobCostType.JobCostTypeId
           };
            NextNumber nnNextNumber = await JobCostLedgerMod.JobCostLedger.Query().GetNextNumber();

            view.JobCostLedgerNumber = nnNextNumber.NextNumberValue;

            JobCostLedger jobCostLedger = await JobCostLedgerMod.JobCostLedger.Query().MapToEntity(view);

            JobCostLedgerMod.JobCostLedger.AddJobCostLedger(jobCostLedger).Apply();

            JobCostLedger newJobCostLedger = await JobCostLedgerMod.JobCostLedger.Query().GetEntityByNumber(view.JobCostLedgerNumber);

            Assert.NotNull(newJobCostLedger);

            newJobCostLedger.Description = "JobCostLedger Test Update";

            JobCostLedgerMod.JobCostLedger.UpdateJobCostLedger(newJobCostLedger).Apply();

            JobCostLedgerView updateView = await JobCostLedgerMod.JobCostLedger.Query().GetViewById(newJobCostLedger.JobCostLedgerId);

            Assert.Same(updateView.Description, "JobCostLedger Test Update");
              JobCostLedgerMod.JobCostLedger.DeleteJobCostLedger(newJobCostLedger).Apply();
            JobCostLedger lookupJobCostLedger= await JobCostLedgerMod.JobCostLedger.Query().GetEntityById(view.JobCostLedgerId);

            Assert.Null(lookupJobCostLedger);
        }
       
      

    }
}
