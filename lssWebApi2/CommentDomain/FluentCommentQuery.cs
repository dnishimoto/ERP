using ERP_Core2.AutoMapper;
using ERP_Core2.Services;
using lssWebApi2.EntityFramework;
using lssWebApi2.Enumerations;
using System.Collections.Generic;
using System.Threading.Tasks;

public class FluentCommentQuery:IFluentCommentQuery
    {
        private UnitOfWork _unitOfWork = null;
        public FluentCommentQuery() { }
        public FluentCommentQuery(UnitOfWork unitOfWork) { _unitOfWork = unitOfWork; }

 public async Task<Comment> MapToEntity(CommentView inputObject)
        {
            Mapper mapper = new Mapper();
            Comment outObject = mapper.Map<Comment>(inputObject);
            await Task.Yield();
            return outObject;
        }

  public async Task<List<Comment>> MapToEntity(List<CommentView> inputObjects)
        {
            List<Comment> list = new List<Comment>();
            Mapper mapper = new Mapper();
            foreach (var item in inputObjects)
            {
                Comment outObject = mapper.Map<Comment>(item);
                list.Add(outObject);
            }
            await Task.Yield();
            return list;

        }

 public async Task<CommentView> MapToView(Comment inputObject)
        {
            Mapper mapper = new Mapper();
            CommentView outObject = mapper.Map<CommentView>(inputObject);
            await Task.Yield();
            return outObject;
        }
      
        
  public async Task<NextNumber>GetNextNumber()
        {
            return await _unitOfWork.commentRepository.GetNextNumber(TypeOfNextNumberEnum.CommentNumber.ToString());
        }
    public async Task<Comment> GetEntityById(long commentId)
    {
        return await _unitOfWork.commentRepository.GetEntityById(commentId);
    }
    public async Task<Comment> GetEntityByNumber(long commentNumber)
    {
        return await _unitOfWork.commentRepository.GetEntityByNumber(commentNumber);
    }
 public async Task<CommentView> GetViewById(long commentId)
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