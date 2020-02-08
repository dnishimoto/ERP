using lssWebApi2.EntityFramework;
using lssWebApi2.Enumerations;
using lssWebApi2.AccountReceivableDetailDomain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using Xunit.Abstractions;

namespace lssWebApi2.AccountReceivableDetailDomain
{

    public class UnitAccountReceivableDetail
    {

        private readonly ITestOutputHelper output;

        public UnitAccountReceivableDetail(ITestOutputHelper output)
        {
            this.output = output;
        }
        [Fact]
        public async Task TestAddUpdatDelete()
        {
            AccountReceivableDetailModule AccountReceivableDetailMod = new AccountReceivableDetailModule();

           AccountReceivableDetailView view = new AccountReceivableDetailView()
            {
                    Description = 'AccountReceivableDetail Test',
                    AccountReceivableDetailCode=99

            };
            NextNumber nnNextNumber = await AccountReceivableDetailMod.AccountReceivableDetail.Query().GetNextNumber();

            view.AccountReceivableDetailNumber = nnNextNumber.NextNumberValue;

            AccountReceivableDetail accountReceivableDetail = await AccountReceivableDetailMod.AccountReceivableDetail.Query().MapToEntity(view);

            AccountReceivableDetailMod.AccountReceivableDetail.AddAccountReceivableDetail(accountReceivableDetail).Apply();

            AccountReceivableDetail newAccountReceivableDetail = await AccountReceivableDetailMod.AccountReceivableDetail.Query().GetEntityByNumber(view.AccountReceivableDetailNumber);

            Assert.NotNull(newAccountReceivableDetail);

            newAccountReceivableDetail.Description = 'AccountReceivableDetail Test Update';

            AccountReceivableDetailMod.AccountReceivableDetail.UpdateAccountReceivableDetail(newAccountReceivableDetail).Apply();

            AccountReceivableDetailView updateView = await AccountReceivableDetailMod.AccountReceivableDetail.Query().GetViewById(newAccountReceivableDetail.AccountReceivableDetailId);

            Assert.Same(updateView.Description, 'AccountReceivableDetail Test Update');
              AccountReceivableDetailMod.AccountReceivableDetail.DeleteAccountReceivableDetail(newAccountReceivableDetail).Apply();
            AccountReceivableDetail lookupAccountReceivableDetail= await AccountReceivableDetailMod.AccountReceivableDetail.Query().GetEntityById(view.AccountReceivableDetailId);

            Assert.Null(lookupAccountReceivableDetail);
        }
       
      

    }
}
