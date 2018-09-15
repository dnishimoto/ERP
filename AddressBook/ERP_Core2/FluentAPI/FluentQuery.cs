using ERP_Core2.AbstractFactory;
using ERP_Core2.AccountPayableDomain;
using ERP_Core2.Interfaces;
using ERP_Core2.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static ERP_Core2.AccountPayableDomain.AccountsPayableModule;

namespace ERP_Core2.FluentAPI
{
    public class FluentQuery : AbstractErrorHandling, IQuery
    {
        public UnitOfWork unitOfWork = new UnitOfWork();
        public CreateProcessStatus processStatus;


        public IQuery Query()
        {
            return this as IQuery;
        }

    }
}
