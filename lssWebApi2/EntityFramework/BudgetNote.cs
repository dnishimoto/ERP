using System;
using System.Collections.Generic;

namespace lssWebApi2.EntityFramework
{
    public partial class BudgetNote
    {
        public long BudgetNoteId { get; set; }
        public long BudgetId { get; set; }
        public string Note { get; set; }
        public DateTime Create { get; set; }

        public virtual Budget Budget { get; set; }

    }
}
