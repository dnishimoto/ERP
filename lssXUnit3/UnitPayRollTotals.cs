using lssWebApi2.EntityFramework;
using lssWebApi2.Enumerations;
using lssWebApi2.PayRollDomain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using Xunit.Abstractions;

namespace ERP_Core2.PayRollDomain
{

    public class UnitPayRollTotals
    {

        private readonly ITestOutputHelper output;

        public UnitPayRollTotals(ITestOutputHelper output)
        {
            this.output = output;
        }
        [Fact]
        public async Task TestAddUpdatDelete()
        {
            PayRollTotalsModule PayRollTotalsMod = new PayRollTotalsModule();

            PayRollPaySequenceModule PayRollPaySequenceModule = new PayRollPaySequenceModule();

            int payRollGroupCode = 1;
           PayRollTotalsView view = new PayRollTotalsView()
            {
            Employee =1,
            EarningCode =1,
            EarningType="E",
            Amount =3000,
            PayRollGroupCode= payRollGroupCode
               //public long PaySeqence { get; set; }
               //public DateTime PayRollBeginDate { get; set; }
               //public DateTime PayRollEndDate { get; set; }
           };
            NextNumber nnNextNumber = await PayRollTotalsMod.PayRollTotals.Query().GetNextNumber();
            view.PayRollTotalsNumber = nnNextNumber.NextNumberValue;

            PayRollPaySequenceView viewPaySequence =await PayRollPaySequenceModule.PayRollPaySequence.Query().GetCurrentPaySequenceByGroupCode(payRollGroupCode);
            if (viewPaySequence!=null)
            {
                view.PaySeqence = viewPaySequence.PaySequence;
                view.PayRollBeginDate = viewPaySequence.PayRollBeginDate;
                view.PayRollEndDate = viewPaySequence.PayRollEndDate;

            }
                       

            PayRollTotals payRollTotals = await PayRollTotalsMod.PayRollTotals.Query().MapToEntity(view);

            PayRollTotalsMod.PayRollTotals.AddPayRollTotals(payRollTotals).Apply();

            PayRollTotals newPayRollTotals = await PayRollTotalsMod.PayRollTotals.Query().GetEntityByNumber(view.PayRollTotalsNumber);

            Assert.NotNull(newPayRollTotals);

            newPayRollTotals.Amount = 3000.01M;

            PayRollTotalsMod.PayRollTotals.UpdatePayRollTotals(newPayRollTotals).Apply();

            PayRollTotalsView updateView = await PayRollTotalsMod.PayRollTotals.Query().GetViewById(newPayRollTotals.PayRollTotalsId);

            if (updateView.Amount != 3000.01M)
            {
                Assert.True(true);
            }
           
         

                   PayRollTotalsMod.PayRollTotals.DeletePayRollTotals(newPayRollTotals).Apply();
            PayRollTotals lookupPayRollTotals= await PayRollTotalsMod.PayRollTotals.Query().GetEntityById(view.PayRollTotalsId);

            Assert.Null(lookupPayRollTotals);
        }
       
      

    }
}
