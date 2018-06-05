using ERP_Core2.EntityFramework;
using MillenniumERP.AddressBookDomain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERP_Core2.AbstractFactory
{
    public abstract partial class AbstractFactory
    {
        public abstract SupervisorView MapSupervisorView(Supervisor supervisor, Supervisor parentSupervisor);
        public abstract EmployeeView MapEmployeeView(Employee employee);
        public abstract SupplierView MapSupplierView(Supplier supplier);
        public abstract CarrierView MapCarrierView(Carrier carrier);
    }
    public abstract partial class AbstractFactory
    {
        public abstract BuyerView MapBuyerView(Buyer buyer);
    }
    public abstract class BusinessViewFactory : AbstractFactory
    {
    }
    
    public partial class ApplicationViewFactory : BusinessViewFactory
    {
        public override SupervisorView MapSupervisorView(Supervisor supervisor, Supervisor parentSupervisor)
        {
            return new SupervisorView(supervisor, parentSupervisor);
        }
        public override EmployeeView MapEmployeeView(Employee employee)
        {
            return new EmployeeView(employee);
        }
        public override SupplierView MapSupplierView(Supplier supplier)
        {
            return new SupplierView(supplier);
        }
        public override CarrierView MapCarrierView(Carrier carrier)
        {
            return new CarrierView(carrier);
        }
    }
    public partial class ApplicationViewFactory : BusinessViewFactory
    {
        public override BuyerView MapBuyerView(Buyer buyer)
        {
            return new BuyerView(buyer);
        }
    }
}
