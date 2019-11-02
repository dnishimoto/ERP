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

    public class UnitPayRollCurrentPaySequence
    {

        private readonly ITestOutputHelper output;

        public UnitPayRollCurrentPaySequence(ITestOutputHelper output)
        {
            this.output = output;
        }
        [Fact]
        public async Task TestAddUpdatDelete()
        {
            PayRollCurrentPaySequenceModule PayRollCurrentPaySequenceMod = new PayRollCurrentPaySequenceModule();

           PayRollCurrentPaySequenceView view = new PayRollCurrentPaySequenceView()
            {
                    Description = 'PayRollCurrentPaySequence Test',
                    PayRollCurrentPaySequenceCode=99

            };
            NextNumber nnNextNumber = await PayRollCurrentPaySequenceMod.PayRollCurrentPaySequence.Query().GetNextNumber();

            view.PayRollCurrentPaySequenceNumber = nnNextNumber.NextNumberValue;

            PayRollCurrentPaySequence payRollCurrentPaySequence = await PayRollCurrentPaySequenceMod.PayRollCurrentPaySequence.Query().MapToEntity(view);

            PayRollCurrentPaySequenceMod.PayRollCurrentPaySequence.AddPayRollCurrentPaySequence(payRollCurrentPaySequence).Apply();

            PayRollCurrentPaySequence newPayRollCurrentPaySequence = await PayRollCurrentPaySequenceMod.PayRollCurrentPaySequence.Query().GetEntityByNumber(view.PayRollCurrentPaySequenceNumber);

            Assert.NotNull(newPayRollCurrentPaySequence);

            newPayRollCurrentPaySequence.Description = 'PayRollCurrentPaySequence Test Update';

            PayRollCurrentPaySequenceMod.PayRollCurrentPaySequence.UpdatePayRollCurrentPaySequence(newPayRollCurrentPaySequence).Apply();

            PayRollCurrentPaySequenceView updateView = await PayRollCurrentPaySequenceMod.PayRollCurrentPaySequence.Query().GetViewById(newPayRollCurrentPaySequence.PayRollCurrentPaySequenceId);

            Assert.Same(updateView.Description, 'PayRollCurrentPaySequence Test Update');
              PayRollCurrentPaySequenceMod.PayRollCurrentPaySequence.DeletePayRollCurrentPaySequence(newPayRollCurrentPaySequence).Apply();
            PayRollCurrentPaySequence lookupPayRollCurrentPaySequence= await PayRollCurrentPaySequenceMod.PayRollCurrentPaySequence.Query().GetEntityById(view.PayRollCurrentPaySequenceId);

            Assert.Null(lookupPayRollCurrentPaySequence);
        }
       
      

    }
}
