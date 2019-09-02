using System;
using System.Collections.Generic;

namespace lssWebApi2.EntityFramework
{
    public partial class Comment
    {
        public long CommentId { get; set; }
        public long EntityId { get; set; }
        public string EntityType { get; set; }
        public string CommentContent { get; set; }
        public long CommentNumber { get; set; }

    }
}