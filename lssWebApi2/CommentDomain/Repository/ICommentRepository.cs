
using lssWebApi2.EntityFramework;
using System.Threading.Tasks;

namespace lssWebApi2.CommentDomain
{
    public interface ICommentRepository
    {
        Task<Comment> GetEntityById(long _commentId);
        Task<Comment> GetEntityByNumber(long commentNumber);
    }
}
