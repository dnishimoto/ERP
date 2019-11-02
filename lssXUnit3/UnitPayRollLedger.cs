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

    public class UnitPayRollLedger
    {

        private readonly ITestOutputHelper output;

        public UnitPayRollLedger(ITestOutputHelper output)
        {
            this.output = output;
        }
        [Fact]

        public async Task TestTransactionsByEmployee()
        {
            PayRollLedgerModule PayRollLedgerMod = new PayRollLedgerModule();

            int payRollGroupCode = 1;
            long employee = 1;

            PayRollCurrentPaySequenceView payCurrentSequenceView = await PayRollLedgerMod.PayRollCurrentPaySequence.Query().GetViewByPayRollCode(payRollGroupCode);

            List<PayRollTransactionsByEmployeeView> listTransactionsViews = await PayRollLedgerMod.PayRollTransactionsByEmployee.Query().GetTransactionsByEmployeeViews(employee);

            List<PayRollLedger> listNewLedgers = new List<PayRollLedger>();

            foreach (var item in listTransactionsViews)
            {
                NextNumber nnNextNumber = await PayRollLedgerMod.PayRollLedger.Query().GetNextNumber();

                listNewLedgers.Add(new PayRollLedger

                {
                    EmployeeId = item.Employee,
                    PayRollTransactionCode = item.PayRollTransactionCode,
                    Amount = item.Amount,
                    PaidDate = payCurrentSequenceView.PayRollBeginDate,//DateTime.Now,
                    PayPeriodBegin = payCurrentSequenceView.PayRollBeginDate,
                    PayPeriodEnd = payCurrentSequenceView.PayRollEndDate,
                    PaySequence = payCurrentSequenceView.PaySequence,
                    PayRollType = item.PayRollType,
                    PayRollGroupCode = item.PayRollGroupCode ?? 0,
                    ReversingEntry = "N",
                    UpdateEntry = "N",
                    TransactionType=item.TransactionType,
                    PayRollLedgerNumber = nnNextNumber.NextNumberValue
                });
             
            }
            PayRollLedgerMod.PayRollLedger.AddPayRollLedgers(listNewLedgers).Apply();


            List<PayRollLedger> list = await PayRollLedgerMod.PayRollLedger.Query().GetEntitiesByPaySequence(employee, payCurrentSequenceView.PaySequence);
            list.ForEach(e => e.Amount+= 10M);
            decimal total = 0M;
            list.ForEach(e => total += e.Amount);

            PayRollLedgerMod.PayRollLedger.UpdatePayRollLedgers(list).Apply();

            List<PayRollLedger> listUpdate = await PayRollLedgerMod.PayRollLedger.Query().GetEntitiesByPaySequence(employee, payCurrentSequenceView.PaySequence);
            decimal totalUpdate = 0M;
            listUpdate.ForEach(e => totalUpdate += e.Amount);
            if (total != totalUpdate) Assert.True(false);

            PayRollLedgerMod.PayRollLedger.DeletePayRollLedgers(listUpdate).Apply();

        }

        [Fact]
        public async Task TestAddUpdatDelete()
        {
            PayRollLedgerModule PayRollLedgerMod = new PayRollLedgerModule();

            PayRollLedgerView view = new PayRollLedgerView()
            {
                EmployeeId = 1,
                PayRollTransactionCode = 4,
                PayRollType = "Earnings",
                Amount = 3026.1M,
                PaidDate = DateTime.Parse("10/8/2019"),
                PayPeriodBegin = DateTime.Parse("10/4/2019"),
                PayPeriodEnd = DateTime.Parse("10/8/2019"),
                PayRollGroupCode = 1,
                TransactionType="E"
            };
            NextNumber nnNextNumber = await PayRollLedgerMod.PayRollLedger.Query().GetNextNumber();

            view.PayRollLedgerNumber = nnNextNumber.NextNumberValue;

            PayRollLedger payRollLedger = await PayRollLedgerMod.PayRollLedger.Query().MapToEntity(view);

            PayRollLedgerMod.PayRollLedger.AddPayRollLedger(payRollLedger).Apply();

            PayRollLedger newPayRollLedger = await PayRollLedgerMod.PayRollLedger.Query().GetEntityByNumber(view.PayRollLedgerNumber);

            Assert.NotNull(newPayRollLedger);

            newPayRollLedger.PayRollType = "Earnings (update)";

            PayRollLedgerMod.PayRollLedger.UpdatePayRollLedger(newPayRollLedger).Apply();

            PayRollLedgerView updateView = await PayRollLedgerMod.PayRollLedger.Query().GetViewById(newPayRollLedger.PayRollLedgerId);

            Assert.Same(updateView.PayRollType, "Earnings (update)");

            PayRollLedgerMod.PayRollLedger.DeletePayRollLedger(newPayRollLedger).Apply();
            PayRollLedger lookupPayRollLedger = await PayRollLedgerMod.PayRollLedger.Query().GetEntityById(view.PayRollLedgerId);

            Assert.Null(lookupPayRollLedger);
        }



    }
}
