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

    public class UnitPayRollTransactionsByEmployee
    {

        private readonly ITestOutputHelper output;

        public UnitPayRollTransactionsByEmployee(ITestOutputHelper output)
        {
            this.output = output;
        }
        [Fact]
        public async Task TestAddUpdatDelete()
        {
            PayRollTransactionsByEmployeeModule PayRollTransactionsByEmployeeMod = new PayRollTransactionsByEmployeeModule();

            PayRollTransactionsByEmployeeView view = new PayRollTransactionsByEmployeeView()
            {

                Employee = 1,
                PayRollTransactionCode = 11,
                Amount = 21.22M,
                TaxPercentOfGross = 0,
                AdditionalAmount = 0,
                PayRollGroupCode = 1,
                BenefitOption = 1,
                PayRollType="Earnings",
                TransactionType="E"
            };
            NextNumber nnNextNumber = await PayRollTransactionsByEmployeeMod.PayRollTransactionsByEmployee.Query().GetNextNumber();

            view.PayRollTransactionsByEmployeeNumber = nnNextNumber.NextNumberValue;

            PayRollTransactionsByEmployee payRollTransactionsByEmployee = await PayRollTransactionsByEmployeeMod.PayRollTransactionsByEmployee.Query().MapToEntity(view);

            PayRollTransactionsByEmployeeMod.PayRollTransactionsByEmployee.AddPayRollTransactionsByEmployee(payRollTransactionsByEmployee).Apply();

            PayRollTransactionsByEmployee newPayRollTransactionsByEmployee = await PayRollTransactionsByEmployeeMod.PayRollTransactionsByEmployee.Query().GetEntityByNumber(view.PayRollTransactionsByEmployeeNumber);

            Assert.NotNull(newPayRollTransactionsByEmployee);

            newPayRollTransactionsByEmployee.Amount = 21.23M;

            PayRollTransactionsByEmployeeMod.PayRollTransactionsByEmployee.UpdatePayRollTransactionsByEmployee(newPayRollTransactionsByEmployee).Apply();

            PayRollTransactionsByEmployeeView updateView = await PayRollTransactionsByEmployeeMod.PayRollTransactionsByEmployee.Query().GetViewById(newPayRollTransactionsByEmployee.PayRollTransactionsByEmployeeId);

            if (updateView.Amount != 21.23M)
            {
                Assert.True(false);
            }


            PayRollTransactionsByEmployeeMod.PayRollTransactionsByEmployee.DeletePayRollTransactionsByEmployee(newPayRollTransactionsByEmployee).Apply();
            PayRollTransactionsByEmployee lookupPayRollTransactionsByEmployee = await PayRollTransactionsByEmployeeMod.PayRollTransactionsByEmployee.Query().GetEntityById(view.PayRollTransactionsByEmployeeId);

            Assert.Null(lookupPayRollTransactionsByEmployee);
        }



    }
}
