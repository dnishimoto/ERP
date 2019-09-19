using System;
using System.Collections.Generic;

namespace lssWebApi2.EntityFramework
{
    public partial class PayRollGroup
    {
        public long PayRollGroupId { get; set; }
        public int PayRollGroupCode { get; set; }
        public string Description { get; set; }
        public long PayRollGroupNumber { get; set; }

    }
}