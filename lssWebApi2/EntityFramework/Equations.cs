using System;
using System.Collections.Generic;

namespace lssWebApi2.EntityFramework
{
    public partial class Equations
    {
        public long Id { get; set; }
        public string Equation { get; set; }
        public string Queueid { get; set; }
        public string Evaluated { get; set; }
        public string Cellname { get; set; }

    }
}