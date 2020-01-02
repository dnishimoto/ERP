using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace lssWebApi2.Enumerations
{
    public enum CreateProcessStatus
    {
        Insert,
        Create,
       AlreadyExists,
        Update,
        Delete,
        Failed
    }
}
