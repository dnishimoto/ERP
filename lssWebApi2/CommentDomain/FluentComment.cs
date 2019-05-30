using ERP_Core2.AccountPayableDomain;
using ERP_Core2.Services;
using lssWebApi2.EntityFramework;
using System.Collections.Generic;

public class FluentComment :IFluentComment
    {
        private UnitOfWork unitOfWork = new UnitOfWork();
        private CreateProcessStatus processStatus;

        public FluentComment() { }
        public IFluentCommentQuery Query()
        {
            return new FluentCommentQuery(unitOfWork) as IFluentCommentQuery;
        }
        public IFluentComment Apply() {
            if (this.processStatus == CreateProcessStatus.Insert || this.processStatus == CreateProcessStatus.Update || this.processStatus == CreateProcessStatus.Delete)
            { unitOfWork.CommitChanges(); }
            return this as IFluentComment;
        }
        public IFluentComment AddComments(List<Comment> newObjects)
        {
            unitOfWork.commentRepository.AddObjects(newObjects);
            this.processStatus = CreateProcessStatus.Insert;
            return this as IFluentComment;
        }
        public IFluentComment UpdateComments(List<Comment> newObjects)
        {
            foreach (var item in newObjects)
            {
                unitOfWork.commentRepository.UpdateObject(item);
            }
            this.processStatus = CreateProcessStatus.Update;
            return this as IFluentComment;
        }
        public IFluentComment AddComment(Comment newObject) {
            unitOfWork.commentRepository.AddObject(newObject);
            this.processStatus = CreateProcessStatus.Insert;
            return this as IFluentComment;
        }
        public IFluentComment UpdateComment(Comment updateObject) {
            unitOfWork.commentRepository.UpdateObject(updateObject);
            this.processStatus = CreateProcessStatus.Update;
            return this as IFluentComment;

        }
        public IFluentComment DeleteComment(Comment deleteObject) {
            unitOfWork.commentRepository.DeleteObject(deleteObject);
            this.processStatus = CreateProcessStatus.Delete;
            return this as IFluentComment;
        }
   	public IFluentComment DeleteComments(List<Comment> deleteObjects)
        {
            unitOfWork.commentRepository.DeleteObjects(deleteObjects);
            this.processStatus = CreateProcessStatus.Delete;
            return this as IFluentComment;
        }
}