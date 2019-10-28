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

        public UniPayRollPaySequence(ITestOutputHelper output)
        {
            this.output = output;
        }
        [Fact]
        public async Task TestAddUpdatDelete()
        {
            PayRollPaySequenceModule PayRollPaySequenceMod = new PayRollPaySequenceModule();

           PayRollPaySequenceView view = new PayRollPaySequenceView()
            {
                    Description = 'PayRollPaySequence Test',
                    PayRollPaySequenceCode=99

            };
            NextNumber nnNextNumber = await PayRollPaySequenceMod.PayRollPaySequenceQuery().GetNextNumber();

            view.PayRollPaySequenceNumber = nnNextNumber.NextNumberValue;

            PayRollPaySequence payRollPaySequence = await PayRollPaySequenceMod.PayRollPaySequence.Query().MapToEntity(view);

            PayRollPaySequenceMod.PayRollGroup.AddPayRollPaySequence(payRollPaySequence).Apply();

            PayRollPaySequence newPayRollPaySequence = await PayRollPaySequenceMod.PayRollPaySequence.Query().GetEntityByNumber(view.PayRollPaySequenceNumber);

            Assert.NotNull(newPayRollPaySequence);

            newPayRollPaySequence.Description = 'PayRollPaySequence Test Update';

            PayRollPaySequenceMod.PayRollGroup.UpdatePayRollPaySequence(newPayRollPaySequence).Apply();

            PayRollPaySequenceView updateView = await PayRollPaySequenceModPayRollPaySequence.Query().GetViewById(newPayRollPaySequence.PayRollPaySequenceId);

            Assert.Same(updateView.Description, 'PayRollPaySequence Test Update');
            int code = 99;
           

           PayRollPaySequenceView lookupByCode = await PayRollPaySequenceMod.PayRollPaySequence.Query().GetViewByCode(code);

            Assert.NotNull(lookupByCode);

            PayRollPaySequenceMod.PayRollPaySequence.DeletePayRollPaySequence(newPayRollPaySequence).Apply();
            PayRollPaySequence lookupPayRollPaySequence= await PayRollPaySequenceMod.PayRollPaySequence.Query().GetEntityById(view.PayRollPaySequenceId);

            Assert.Null(lookupPayRollPaySequence);
        }
       
      

    }
}
