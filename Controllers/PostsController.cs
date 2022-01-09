using AutoMapper;
using demoProject.DtoModels;
using demoProject.Models;
using demoProject.Services;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace demoProject.Controllers
{
    [ApiController]
    [Route("api/posts")]
    public class PostsController: ControllerBase
    {

        private readonly IDataService _dataService;
        private readonly IMapper _mapper;
        public PostsController(IDataService dataService, IMapper mapper)
        {
            _dataService = dataService ?? throw new ArgumentNullException(nameof(dataService));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        [HttpGet]
        public ActionResult<IEnumerable<PostListDto>> GetPosts() {
            var postDtoList = new List<PostListDto>();

            var postList =_dataService.GetPosts();
            var favoritePosts = _dataService.GetFavoritePosts();
            var allComments = _dataService.GetComments();
            foreach (var post in postList) {
               postDtoList.Add(new PostListDto() { 
                    id = post.id,
                    authorName = post.authorName,
                    postTitle = post.title,
                    numberOfComments = allComments.Where(c => c.postId == post.id).Count(),
                    isFavorite = favoritePosts.Contains(post.id) ? true : false
                });
            }
            return Ok(postDtoList);
        }

        [HttpGet("{postId}")]
        public ActionResult<PostDetailDto> GetPost([FromRoute] int postId)
        {
            var postList = _dataService.GetPosts();
            var post = postList.Single(p => p.id == postId);
            return Ok(_mapper.Map<PostDetailDto>(post));
        }

        [HttpDelete("{postId}")]
        public IActionResult DeletePost([FromRoute] int postId) {
            _dataService.DeletePost(postId);
            return NoContent();
        }

        [HttpPut("{postId}/favorite")]
        public IActionResult SetToFavoritePosts([FromRoute] int postId) {
            _dataService.CreateFavoritePost(postId);
            return NoContent();
        }

        [HttpDelete("{postId}/favorite")]
        public IActionResult DeleteFromFavoritePosts([FromRoute] int postId) {
            _dataService.DeleteFavoritePost(postId);
            return NoContent();
        }

        [HttpPost("createnewdataset")]
        public ActionResult<PostDetailDto> CreateNewDataset()
        {
            return _dataService.initializeDataset() ? StatusCode(201) : NotFound();
        }
    }
}
