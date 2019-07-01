using ERP_Core2.Services;
using lssWebApi2.EntityFramework;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using System.Linq;

namespace lssWebApi2.CommentDomain
{

    public class CommentView
    {
        public long CommentId { get; set; }
        public long CommentNumber { get; set; }
        public long EntityId { get; set; }
        public string EntityType { get; set; }
        public string CommentContent { get; set; }
    }

    public class CommentRepository : Repository<Comment>, ICommentRepository
    {
        ListensoftwaredbContext _dbContext;
        public CommentRepository(DbContext db) : base(db)
        {
            _dbContext = (ListensoftwaredbContext)db;
        }

        public async Task<Comment> GetEntityById(long commentId)
        {
            return await _dbContext.FindAsync<Comment>(commentId);
        }

        public void SetDb(DbContext db)
        {
            _dbContext = (ListensoftwaredbContext)db;
        }
        public async Task<Comment> GetEntityByNumber(long commentNumber)
        {
            var query = await (from detail in _dbContext.Comment
                               where detail.CommentNumber == commentNumber
                               select detail).FirstOrDefaultAsync<Comment>();
            return query;
        }
    }
}