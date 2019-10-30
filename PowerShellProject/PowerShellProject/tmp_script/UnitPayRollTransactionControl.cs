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

    public class UnitPayRollTransactionControl
    {

        private readonly ITestOutputHelper output;

        public UnitPayRollTransactionControl(ITestOutputHelper output)
        {
            this.output = output;
        }
        [Fact]
        public async Task TestAddUpdatDelete()
        {
            PayRollTransactionControlModule PayRollTransactionControlMod = new PayRollTransactionControlModule();

           PayRollTransactionControlView view = new PayRollTransactionControlView()
            {
                    Description = "PayRollTransactionControl Test",
                    PayRollTransactionControlCode=99

            };
            NextNumber nnNextNumber = await PayRollTransactionControlMod.PayRollTransactionControl.Query().GetNextNumber();

            view.PayRollTransactionControlNumber = nnNextNumber.NextNumberValue;

            PayRollTransactionControl payRollTransactionControl = await PayRollTransactionControlMod.PayRollTransactionControl.Query().MapToEntity(view);

            PayRollTransactionControlMod.PayRollTransactionControl.AddPayRollTransactionControl(payRollTransactionControl).Apply();

            PayRollTransactionControl newPayRollTransactionControl = await PayRollTransactionControlMod.PayRollTransactionControl.Query().GetEntityByNumber(view.PayRollTransactionControlNumber);

            Assert.NotNull(newPayRollTransactionControl);

            newPayRollTransactionControl.Description = "PayRollTransactionControl Test Update";

            PayRollTransactionControlMod.PayRollTransactionControl.UpdatePayRollTransactionControl(newPayRollTransactionControl).Apply();

            PayRollTransactionControlView updateView = await PayRollTransactionControlMod.PayRollTransactionControl.Query().GetViewById(newPayRollTransactionControl.PayRollTransactionControlId);

            Assert.Same(updateView.Description, "PayRollTransactionControl Test Update");

			PayRollTransactionControlMod.PayRollTransactionControl.DeletePayRollTransactionControl(newPayRollTransactionControl).Apply();
            PayRollTransactionControl lookupPayRollTransactionControl= await PayRollTransactionControlMod.PayRollTransactionControl.Query().GetEntityById(view.PayRollTransactionControlId);

            Assert.Null(lookupPayRollTransactionControl);
        }
       
      

    }
}
