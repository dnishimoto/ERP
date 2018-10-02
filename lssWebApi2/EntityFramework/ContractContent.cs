using System;
using System.Collections.Generic;

namespace lssWebApi2.entityframework
{
    public partial class ContractContent
    {
        public long ContractContentId { get; set; }
        public long ContractId { get; set; }
        public string Wbs { get; set; }
        public string TextMemo { get; set; }

        public virtual Contract Contract { get; set; }
    }
}
