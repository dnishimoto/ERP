﻿using ERP_Core2.AddressBookDomain;
using lssWebApi2.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace lssWebApi2.Interfaces
{
    public interface IFluentInventorySupplierQuery
    {
        AddressBook GetAddressBookbyEmail(Emails email);
        SupplierView GetSupplierViewByEmail(Emails email);
    }
}