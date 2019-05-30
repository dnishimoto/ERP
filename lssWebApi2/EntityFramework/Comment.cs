using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace lssWebApi2.EntityFramework
{
    public partial class Comment
    {
        public Comment() { }
        public long CommentId { get; set; }
        public long CommentNumber { get; set; }
        public long EntityId { get; set; }
        public string EntityType { get; set; }
        public string CommentContent { get; set; }
    }
}
