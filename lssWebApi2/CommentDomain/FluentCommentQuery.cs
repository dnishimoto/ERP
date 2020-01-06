using lssWebApi2.AutoMapper;
using lssWebApi2.Services;
using lssWebApi2.EntityFramework;
using lssWebApi2.Enumerations;
using lssWebApi2.MapperAbstract;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace lssWebApi2.CommentDomain
{
    public class FluentCommentQuery : MapperAbstract<Comment,CommentView> ,IFluentCommentQuery
    {
        private UnitOfWork _unitOfWork = null;
        public FluentCommentQuery() { }
        public FluentCommentQuery(UnitOfWork unitOfWork) { _unitOfWork = unitOfWork; }

        public override async Task<Comment> MapToEntity(CommentView inputObject)
        {
     
            Comment outObject = mapper.Map<Comment>(inputObject);
            await Task.Yield();
            return outObject;
        }

        public override async Task<IList<Comment>> MapToEntity(IList<CommentView> inputObjects)
        {
            IList<Comment> list = new List<Comment>();

            foreach (var item in inputObjects)
            {
                Comment outObject = mapper.Map<Comment>(item);
                list.Add(outObject);
            }
            await Task.Yield();
            return list;

        }

        public override async Task<CommentView> MapToView(Comment inputObject)
        {

            CommentView outObject = mapper.Map<CommentView>(inputObject);
            await Task.Yield();
            return outObject;
        }


        public async Task<NextNumber> GetNextNumber()
        {
            return await _unitOfWork.commentRepository.GetNextNumber(TypeOfComment.CommentNumber.ToString());
        }
        public override async Task<Comment> GetEntityById(long ? commentId)
        {
            return await _unitOfWork.commentRepository.GetEntityById(commentId);
        }
        public async Task<Comment> GetEntityByNumber(long commentNumber)
        {
            return await _unitOfWork.commentRepository.GetEntityByNumber(commentNumber);
        }
        public override async Task<CommentView> GetViewById(long ? commentId)
        {
            Comment detailItem = await _unitOfWork.commentRepository.GetEntityById(commentId);

            return await MapToView(detailItem);
        }
        public async Task<CommentView> GetViewByNumber(long commentNumber)
        {
            Comment detailItem = await _unitOfWork.commentRepository.GetEntityByNumber(commentNumber);

            return await MapToView(detailItem);
        }

    }
}