﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace lssWebApi2.Interfaces
{
    public interface IChartOfAccount
    {
        IChartOfAccount CreateChartOfAccountModel();
        IChartOfAccountQuery Query();
        IChartOfAccount Apply();
    }
}