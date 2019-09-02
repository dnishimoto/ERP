using System;
using System.Collections.Generic;

namespace lssWebApi2.EntityFramework
{
    public partial class PayRollW4
    {
        public long PayRollW4id { get; set; }
        public int Allowances { get; set; }
        public long Employee { get; set; }
        public bool? Married { get; set; }
        public bool? Single { get; set; }
        public string PayFrequency { get; set; }

    }
}