using lssWebApi2.EntityFramework;
using System.Collections.Generic;

namespace lssWebApi2.CommentDomain
{
    public interface IFluentComment
    {
        IFluentCommentQuery Query();
        IFluentComment Apply();
        IFluentComment AddComment(Comment comment);
        IFluentComment UpdateComment(Comment comment);
        IFluentComment DeleteComment(Comment comment);
        IFluentComment UpdateComments(IList<Comment> newObjects);
        IFluentComment AddComments(List<Comment> newObjects);
        IFluentComment DeleteComments(List<Comment> deleteObjects);
    }
}
