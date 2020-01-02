using lssWebApi2.AddressBookDomain;
using lssWebApi2.EmployeeDomain;
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

namespace lssWebApi2.PayRollDomain
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
            long employee = 1;
            PayRollTransactionsByEmployeeModule PayRollTransactionsByEmployeeMod = new PayRollTransactionsByEmployeeModule();
            int earningCode = 4;
            string earningType = "E";
            PayRollEarningsView viewEarnings = await PayRollTransactionsByEmployeeMod.PayRollEarnings.Query().GetViewByEarningCode(earningCode, earningType);


            PayRollTransactionsByEmployeeView view = new PayRollTransactionsByEmployeeView()
            {

                Employee = employee,
                PayRollTransactionCode = viewEarnings.EarningCode,
                Amount = 3026M,
                TaxPercentOfGross = 0,
                AdditionalAmount = 0,
                PayRollGroupCode = 1,
                BenefitOption = 1,
                PayRollType = "Earnings",
                TransactionType=viewEarnings.EarningType
            };

            NextNumber nnNextNumber = await PayRollTransactionsByEmployeeMod.PayRollTransactionsByEmployee.Query().GetNextNumber();

            view.PayRollTransactionsByEmployeeNumber = nnNextNumber.NextNumberValue;

            PayRollTransactionsByEmployee payRollTransactionsByEmployee = await PayRollTransactionsByEmployeeMod.PayRollTransactionsByEmployee.Query().MapToEntity(view);

            PayRollTransactionsByEmployeeMod.PayRollTransactionsByEmployee.AddPayRollTransactionsByEmployee(payRollTransactionsByEmployee).Apply();

            PayRollTransactionsByEmployee newPayRollTransactionsByEmployee = await PayRollTransactionsByEmployeeMod.PayRollTransactionsByEmployee.Query().GetEntityByNumber(view.PayRollTransactionsByEmployeeNumber);

            Assert.NotNull(newPayRollTransactionsByEmployee);

            newPayRollTransactionsByEmployee.Amount = 3026.23M;

            PayRollTransactionsByEmployeeMod.PayRollTransactionsByEmployee.UpdatePayRollTransactionsByEmployee(newPayRollTransactionsByEmployee).Apply();

            PayRollTransactionsByEmployeeView updateView = await PayRollTransactionsByEmployeeMod.PayRollTransactionsByEmployee.Query().GetViewById(newPayRollTransactionsByEmployee.PayRollTransactionsByEmployeeId);

            if (updateView.Amount != 3026.23M)
            {
                Assert.True(false);
            }


           PayRollTransactionsByEmployeeMod.PayRollTransactionsByEmployee.DeletePayRollTransactionsByEmployee(newPayRollTransactionsByEmployee).Apply();
            PayRollTransactionsByEmployee lookupPayRollTransactionsByEmployee = await PayRollTransactionsByEmployeeMod.PayRollTransactionsByEmployee.Query().GetEntityById(view.PayRollTransactionsByEmployeeId);

            Assert.Null(lookupPayRollTransactionsByEmployee);


            //Add liability

            int deductionLiabilities = 1;
            string deductionLiabilitiesType = "L";
            PayRollDeductionLiabilitiesView viewLiabilities = await PayRollTransactionsByEmployeeMod.PayRollDeductionLiabilities.Query().GetViewByDeductionLiabilitiesCode(deductionLiabilities, deductionLiabilitiesType);
            Assert.NotNull(viewLiabilities);

            EmployeeView viewEmployee= await PayRollTransactionsByEmployeeMod.Employee.Query().GetViewById(employee);

            PayRollTransactionsByEmployeeView view2 = new PayRollTransactionsByEmployeeView()
            {

                Employee = employee,
                PayRollTransactionCode = viewLiabilities.DeductionLiabilitiesCode,
                Amount = viewLiabilities.Percentage??0 * viewEmployee.SalaryPerPayPeriod,
                TaxPercentOfGross = 0,
                AdditionalAmount = 0,
                PayRollGroupCode = 1,
                BenefitOption = 1,
                PayRollType = "LIABILITIES",
                TransactionType = viewLiabilities.DeductionLiabilitiesType
            };
            NextNumber nnNextNumber2 = await PayRollTransactionsByEmployeeMod.PayRollTransactionsByEmployee.Query().GetNextNumber();

            view2.PayRollTransactionsByEmployeeNumber = nnNextNumber2.NextNumberValue;

            PayRollTransactionsByEmployee payRollTransactionsByEmployee2 = await PayRollTransactionsByEmployeeMod.PayRollTransactionsByEmployee.Query().MapToEntity(view2);

            PayRollTransactionsByEmployeeMod.PayRollTransactionsByEmployee.AddPayRollTransactionsByEmployee(payRollTransactionsByEmployee2).Apply();

            PayRollTransactionsByEmployee newPayRollTransactionsByEmployee2 = await PayRollTransactionsByEmployeeMod.PayRollTransactionsByEmployee.Query().GetEntityByNumber(view2.PayRollTransactionsByEmployeeNumber);

            PayRollTransactionsByEmployeeMod.PayRollTransactionsByEmployee.DeletePayRollTransactionsByEmployee(newPayRollTransactionsByEmployee2).Apply();
            PayRollTransactionsByEmployee lookupPayRollTransactionsByEmployee2 = await PayRollTransactionsByEmployeeMod.PayRollTransactionsByEmployee.Query().GetEntityById(view2.PayRollTransactionsByEmployeeId);

            Assert.Null(lookupPayRollTransactionsByEmployee);

        }



    }
}
