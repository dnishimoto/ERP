﻿using lssWebApi2.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace lssWebApi2.InventoryDomain
{
    public interface IFluentInventorySupplier
    {
     
        IFluentInventorySupplierQuery Query();
    }
}
