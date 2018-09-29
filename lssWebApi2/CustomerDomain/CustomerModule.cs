using ERP_Core2.AbstractFactory;
using ERP_Core2.FluentAPI;

namespace ERP_Core2.CustomerDomain
{


    public class CustomerModule : AbstractModule
    {

        public FluentCustomer Customer = new FluentCustomer();

      }
}
