using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using lssWebApi2.Services;

namespace lssWebApi2.CommentDomain
{
    public class CommentModule
    {
        private UnitOfWork unitOfWork;
        public FluentComment Comment;
        public CommentModule()
        {
            unitOfWork = new UnitOfWork();
            Comment = new FluentComment(unitOfWork);
        }
    }
}
