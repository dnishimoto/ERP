﻿using ERP_Core2.AddressBookDomain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace lssWebApi2.AddressBookDomain.Repository
{
    public interface IEmployeeRepository
    {
        Task<EmployeeView> GetEmployeeViewByEmployeeId(long employeeId);
    }
}
