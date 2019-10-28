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

    public class UnitPayRollPaySequence
    {

        private readonly ITestOutputHelper output;

        public UnitPayRollPaySequence(ITestOutputHelper output)
        {
            this.output = output;
        }
        [Fact]
        public async Task TestAddUpdatDelete()
        {
            PayRollPaySequenceModule PayRollPaySequenceMod = new PayRollPaySequenceModule();

           PayRollPaySequenceView view = new PayRollPaySequenceView()
            {
                   PayRollBeginDate = DateTime.Parse("9/9/2019")
                   ,PayRollEndDate= DateTime.Parse("9/13/2019")
                   ,PayRollGroupCode = 1
                   ,Frequency="Bi-Weekly"

            };
            long maxSequencenumber = PayRollPaySequenceMod.PayRollPaySequence.Query().GetMaxSequenceNumber();

            view.PaySequence = maxSequencenumber + 1;

            NextNumber nnNextNumber = await PayRollPaySequenceMod.PayRollPaySequence.Query().GetNextNumber();

            view.PayRollPaySequenceNumber = nnNextNumber.NextNumberValue;

            PayRollPaySequence payRollPaySequence = await PayRollPaySequenceMod.PayRollPaySequence.Query().MapToEntity(view);

            PayRollPaySequenceMod.PayRollPaySequence.AddPayRollPaySequence(payRollPaySequence).Apply();

            PayRollPaySequence newPayRollPaySequence = await PayRollPaySequenceMod.PayRollPaySequence.Query().GetEntityByNumber(view.PayRollPaySequenceNumber);

            Assert.NotNull(newPayRollPaySequence);

            newPayRollPaySequence.Frequency="Bi-Week (update)";

            PayRollPaySequenceMod.PayRollPaySequence.UpdatePayRollPaySequence(newPayRollPaySequence).Apply();

            PayRollPaySequenceView updateView = await PayRollPaySequenceMod.PayRollPaySequence.Query().GetViewById(newPayRollPaySequence.PayRollPaySequenceId);

            Assert.Same(updateView.Frequency, "Bi-Week (update)");
       

            PayRollPaySequenceMod.PayRollPaySequence.DeletePayRollPaySequence(newPayRollPaySequence).Apply();
            PayRollPaySequence lookupPayRollPaySequence= await PayRollPaySequenceMod.PayRollPaySequence.Query().GetEntityById(view.PayRollPaySequenceId);

            Assert.Null(lookupPayRollPaySequence);
        }
       
      

    }
}
