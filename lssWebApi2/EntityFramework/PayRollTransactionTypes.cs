using System;
using System.Collections.Generic;

namespace lssWebApi2.EntityFramework
{
    public partial class PayRollTransactionTypes
    {
        public long PayRollTransactionTypesId { get; set; }
        public int PayRollTranactionCode { get; set; }
        public string Description { get; set; }
        public long PayRollTransactionTypesNumber { get; set; }

    }
}