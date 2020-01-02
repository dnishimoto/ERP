using lssWebApi2.EntityFramework;
using lssWebApi2.Enumerations;
using lssWebApi2.SupervisorDomain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using Xunit.Abstractions;

namespace lssWebApi2.SupervisorDomain
{

    public class UnitSupervisor
    {

        private readonly ITestOutputHelper output;

        public UnitSupervisor(ITestOutputHelper output)
        {
            this.output = output;
        }
        [Fact]
        public async Task TestAddUpdatDelete()
        {
            SupervisorModule SupervisorMod = new SupervisorModule();
            AddressBook addressBook = await SupervisorMod.AddressBook.Query().GetEntityById(1);
            Udc udcTitle = await SupervisorMod.Udc.Query().GetEntityById(25);

            Supervisor parentSupervisor = await SupervisorMod.Supervisor.Query().GetEntityById(2);
            AddressBook parentAddressBook = await SupervisorMod.AddressBook.Query().GetEntityById(parentSupervisor?.AddressId);
            Udc parentUdcTitle = await SupervisorMod.Udc.Query().GetEntityById(parentSupervisor?.JobTitleXrefId);
            SupervisorView view = new SupervisorView()
            {
                AddressId = addressBook.AddressId,
                SupervisorName = addressBook.Name,
                SupervisorCode = "6785",
                JobTitleXrefId = udcTitle.XrefId,
                JobTitle = udcTitle.Value,
                ParentSupervisorId = parentSupervisor?.SupervisorId,
                ParentSupervisorName = parentAddressBook?.Name,
                ParentSupervisorAddressId = parentAddressBook?.AddressId,
                ParentSupervisorTitle = parentUdcTitle?.Value,
                ParentSupervisorCode = parentSupervisor?.SupervisorCode
            };
            NextNumber nnNextNumber = await SupervisorMod.Supervisor.Query().GetNextNumber();

            view.SupervisorNumber = nnNextNumber.NextNumberValue;

            Supervisor supervisor = await SupervisorMod.Supervisor.Query().MapToEntity(view);

            SupervisorMod.Supervisor.AddSupervisor(supervisor).Apply();

            Supervisor newSupervisor = await SupervisorMod.Supervisor.Query().GetEntityByNumber(view.SupervisorNumber);

            Assert.NotNull(newSupervisor);

            newSupervisor.SupervisorCode = "6785U";

            SupervisorMod.Supervisor.UpdateSupervisor(newSupervisor).Apply();

            SupervisorView updateView = await SupervisorMod.Supervisor.Query().GetViewById(newSupervisor.SupervisorId);

            Assert.Same(updateView.SupervisorCode,"6785U");
            SupervisorMod.Supervisor.DeleteSupervisor(newSupervisor).Apply();
            Supervisor lookupSupervisor = await SupervisorMod.Supervisor.Query().GetEntityById(updateView.SupervisorId);

            Assert.Null(lookupSupervisor);
        }



    }
}
