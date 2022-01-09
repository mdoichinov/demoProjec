using demoProject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace demoProject.Services
{
    public interface IDataService
    {
        public bool initializeDataset();
        public List<Post> GetPosts();
        public List<Comment> GetComments();
        public List<Comment> GetCommentsForPost(int postId);
        public List<int> GetFavoritePosts();
        public void DeletePost(int postId);
        public bool DeleteFavoritePost(int postId);
        public bool DeleteCommentForPost(int postId, int commentId);
        public bool CreateFavoritePost(int postId);
        public bool CreateComment(CommentForCreation comment);
    }
}
