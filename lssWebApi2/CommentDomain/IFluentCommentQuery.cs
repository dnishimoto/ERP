using lssWebApi2.EntityFramework;
using System.Collections.Generic;
using System.Threading.Tasks;

public interface IFluentCommentQuery
    {

    Task<Comment> MapToEntity(CommentView inputObject);
    Task<List<Comment>> MapToEntity(List<CommentView> inputObjects);
    Task<CommentView> MapToView(Comment inputObject);
    Task<NextNumber> GetNextNumber();
    Task<CommentView> GetViewById(long commentId);
    Task<CommentView> GetViewByNumber(long commentNumber);
}