
using ERP_Core2.AddressBookDomain;
using ERP_Core2.InventoryDomain;
using ERP_Core2.Services;
using lssWebApi2.CommentDomain;
using lssWebApi2.EntityFramework;
using lssWebApi2.InventoryDomain;
using lssWebApi2.InventoryDomain.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using Xunit.Abstractions;

namespace ERP_Core2.CommentDomain
{

    public class UnitComment
    {

        private readonly ITestOutputHelper output;

        public UnitComment(ITestOutputHelper output)
        {
            this.output = output;
        }
        [Fact]
        public async Task TestAddUpdatDeleteComment()
        {
           CommentModule CommentMod = new CommentModule();


            //Udc equipmentStatus = await CommentMod.Comment.Query().GetUdc("Comment_STATUS","InUse");


            CommentView view = new CommentView()
            {
            EntityId = 1,
            EntityType = nameof(AddressBook),
            CommentContent = "Added a comment to Address book"
            };
            NextNumber nnComment = await CommentMod.Comment.Query().GetNextNumber();

            view.CommentNumber = nnComment.NextNumberValue;

            Comment Comment = await CommentMod.Comment.Query().MapToEntity(view);

            CommentMod.Comment.AddComment(Comment).Apply();

            Comment newComment = await CommentMod.Comment.Query().GetEntityByNumber(view.CommentNumber);

            Assert.NotNull(newComment);

            newComment.CommentContent = "Testing Comment update";

            CommentMod.Comment.UpdateComment(newComment).Apply();

            CommentView updateView = await CommentMod.Comment.Query().GetViewById(newComment.CommentId);

            Assert.Same(updateView.CommentContent, "Testing Comment update");

            CommentMod.Comment.DeleteComment(newComment).Apply();
            Comment lookupComment = await CommentMod.Comment.Query().GetEntityById(view.CommentId);

            Assert.Null(lookupComment);
        }
        [Fact]
        public async Task TestCommentView()
        {
            CommentModule invMod = new CommentModule();

            long CommentId = 21;
            CommentView view = await invMod.Comment.Query().GetViewById(CommentId);

            Assert.NotNull(view);

        }
      

    }
}
