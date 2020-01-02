﻿using lssWebApi2.AddressBookDomain;
using lssWebApi2.SupplierDomain;
using lssWebApi2.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace lssWebApi2.InventoryDomain
{
    public interface IFluentInventorySupplierQuery
    {
        Task<AddressBook> GetAddressBookbyEmail(EmailEntity email);
        Task<SupplierView> GetSupplierViewByEmail(EmailEntity email);
    }
}
