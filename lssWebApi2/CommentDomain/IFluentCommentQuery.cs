using lssWebApi2.EntityFramework;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace lssWebApi2.CommentDomain
{
    public interface IFluentCommentQuery
    {

        Task<Comment> MapToEntity(CommentView inputObject);
        Task<IList<Comment>> MapToEntity(IList<CommentView> inputObjects);
        Task<CommentView> MapToView(Comment inputObject);
        Task<NextNumber> GetNextNumber();
        Task<CommentView> GetViewById(long ? commentId);
        Task<CommentView> GetViewByNumber(long commentNumber);
        Task<Comment> GetEntityById(long ? commentId);
        Task<Comment> GetEntityByNumber(long commentNumber);
    }
}