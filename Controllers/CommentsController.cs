using AutoMapper;
using demoProject.DtoModels;
using demoProject.Services;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace demoProject.Controllers
{
    [ApiController]
    [Route("api/posts/{postId}/comments")]
    public class CommentsController: ControllerBase
    {
        private readonly IDataService _dataService;
        private readonly IMapper _mapper;
        public CommentsController(IDataService dataService, IMapper mapper)
        {
            _dataService = dataService ?? throw new ArgumentNullException(nameof(dataService));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        [HttpGet("", Name = "GetComments")]
        public ActionResult<IEnumerable<CommentDto>> GetCommentsForPost([FromRoute] int postId) {
            var commentList = _dataService.GetCommentsForPost(postId);
            return Ok(_mapper.Map<IEnumerable<CommentDto>>(commentList));
        }

        [HttpPost]
        public IActionResult PostCommentsForPost([FromRoute] int postId, [FromBody] CommentForCreationDto comment)
        {
            var postList = _dataService.GetPosts();
            if (!postList.Exists(p => p.id == postId)) {
                return NotFound();
            }

            var commentDbModel = _mapper.Map<Models.CommentForCreation>(comment);
            commentDbModel.postId = postId;
            _dataService.CreateComment(commentDbModel);
            return StatusCode(201);
        }

        [HttpDelete("{commentId}")]
        public IActionResult DeleteCommentsForPost([FromRoute] int postId, [FromRoute] int commentId)
        {
            if (_dataService.DeleteCommentForPost(postId, commentId)) {
                return NoContent();
            }
            return Conflict();
        }
    }
}
