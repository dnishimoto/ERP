using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using lssWebApi2.CommentDomain;
using lssWebApi2.EntityFramework;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace lssWebApi2.Controllers
{
    [Route("api/[controller]")]
    public class CommentController : Controller
    {
        [HttpPost]
        [Route("View")]
        [ProducesResponseType(typeof(CommentView), StatusCodes.Status200OK)]
        public async Task<IActionResult> AddComment([FromBody]CommentView view)
        {
            CommentModule invMod = new CommentModule();

            NextNumber nnComment = await invMod.Comment.Query().GetNextNumber();

            view.CommentNumber = nnComment.NextNumberValue;

            Comment comment = await invMod.Comment.Query().MapToEntity(view);

            invMod.Comment.AddComment(comment).Apply();

            CommentView newView = await invMod.Comment.Query().GetViewByNumber(view.CommentNumber);


            return Ok(newView);

        }

        [HttpDelete]
        [Route("View")]
        [ProducesResponseType(typeof(CommentView), StatusCodes.Status200OK)]
        public async Task<IActionResult> DeleteComment([FromBody]CommentView view)
        {
            CommentModule invMod = new CommentModule();
            Comment comment = await invMod.Comment.Query().MapToEntity(view);
            invMod.Comment.DeleteComment(comment).Apply();

            return Ok(view);
        }

        [HttpPut]
        [Route("View")]
        [ProducesResponseType(typeof(CommentView), StatusCodes.Status200OK)]
        public async Task<IActionResult> UpdateComment([FromBody]CommentView view)
        {
            CommentModule invMod = new CommentModule();

            Comment comment = await invMod.Comment.Query().MapToEntity(view);


            invMod.Comment.UpdateComment(comment).Apply();

            CommentView retView = await invMod.Comment.Query().GetViewById(comment.CommentId);


            return Ok(retView);

        }

        [HttpGet]
        [Route("View/{CommentId}")]
        [ProducesResponseType(typeof(CommentView), StatusCodes.Status200OK)]

        public async Task<IActionResult> GetCommentView(long commentId)
        {
            CommentModule invMod = new CommentModule();

            CommentView view = await invMod.Comment.Query().GetViewById(commentId);
            return Ok(view);
        }
        // GET: api/<controller>
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/<controller>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<controller>
        [HttpPost]
        public void Post([FromBody]string value)
        {
        }

        // PUT api/<controller>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/<controller>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
