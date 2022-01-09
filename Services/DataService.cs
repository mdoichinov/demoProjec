using demoProject.Models;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using Newtonsoft.Json.Serialization;

using System.Threading.Tasks;
using Newtonsoft.Json;

namespace demoProject.Services
{
    public class DataService: IDataService
    {
        private IConfiguration _configuration;
        private IHostEnvironment _hostEnvironment;

        public DataService(IConfiguration configuration, IHostEnvironment environment)
        {
            this._configuration = configuration ?? throw new ArgumentNullException(nameof(configuration));
            this._hostEnvironment = environment ?? throw new ArgumentNullException(nameof(environment));
        }

        private string getRawJson(string url) {
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
            request.AutomaticDecompression = DecompressionMethods.GZip;

            using (HttpWebResponse response = (HttpWebResponse)request.GetResponse())
            using (Stream stream = response.GetResponseStream())
            using (StreamReader reader = new StreamReader(stream))
            {
                return reader.ReadToEnd();
            }
        }

        private void saveRawTextFile(string fileName, string fileContent ) {
            string path = Path.Combine(this._hostEnvironment.ContentRootPath, "Resources", "JsonFiles");
            if (!Directory.Exists(path))
            {
                throw new DirectoryNotFoundException(path);
            }
            path = Path.Combine(path, fileName + ".json");
            
            File.WriteAllText(path, fileContent);
        }

        private string readRawTextFile(string fileName)
        {
            string path = Path.Combine(this._hostEnvironment.ContentRootPath, "Resources", "JsonFiles", fileName + ".json");
            if (!File.Exists(path))
            {
                throw new FileNotFoundException(path);
            }
            
            return File.ReadAllText(path);
        }

        public bool initializeDataset() {
            var authors = this.getRawJson(this._configuration.GetValue<string>("ResourceUrls:AuthorsUrl"));
            var posts = this.getRawJson(this._configuration.GetValue<string>("ResourceUrls:PostsUrl"));
            var comments = this.getRawJson(this._configuration.GetValue<string>("ResourceUrls:CommentsUrl"));


            var authorsList = JsonConvert.DeserializeObject<List<Author>>(authors);
            var postList = JsonConvert.DeserializeObject<List<Post>>(posts);
            foreach(var post in postList) {
               var author = authorsList.Single<Author>(a => a.id == post.userId);
                post.authorName = author.name; 
             }
            posts = JsonConvert.SerializeObject(postList);

            this.saveRawTextFile("posts", posts);
            this.saveRawTextFile("comments", comments);
            this.saveRawTextFile("favoritePosts", "");

            return true;
        }

        public List<Post> GetPosts() {
            var rawPosts = this.readRawTextFile("posts");
            return JsonConvert.DeserializeObject<List<Post>>(rawPosts);
        }

        public List<Comment> GetComments()
        {
            var rawComments = this.readRawTextFile("comments");
            return JsonConvert.DeserializeObject<List<Comment>>(rawComments);
        }

        public List<Comment> GetCommentsForPost(int postId)
        {
            var rawComments = this.readRawTextFile("comments");
            var commentList = JsonConvert.DeserializeObject<List<Comment>>(rawComments);
            return commentList.Where<Comment>(p => p.postId == postId).ToList();
        }

        public List<int> GetFavoritePosts()
        {
            var favoritePosts = JsonConvert.DeserializeObject<List<int>>(this.readRawTextFile("favoritePosts"));
            if (favoritePosts == null) {
                favoritePosts = new List<int>();
            }
            return favoritePosts;
        }

        public void DeletePost(int postId) {
            var postList = this.GetPosts();
            int numberOfElements = postList.RemoveAll(p => p.id == postId);
            if (numberOfElements > 1) {
                throw new DataMisalignedException("Found multiple records with the same ID");
            }
            this.saveRawTextFile("posts", JsonConvert.SerializeObject(postList));
        }

        public bool DeleteFavoritePost(int postId)
        {
            var allFavoritePosts = this.GetFavoritePosts();
            if (allFavoritePosts.Contains(postId))
            {
                allFavoritePosts.Remove(postId);
                this.saveRawTextFile("favoritePosts", JsonConvert.SerializeObject(allFavoritePosts));
                return true;
            }
            return false;
        }

        public bool DeleteCommentForPost(int postId, int commentId)
        {
            var commentList = this.GetComments();
            int numberOfElements = commentList.RemoveAll(c => c.id == commentId && c.postId == postId);
            if (numberOfElements > 1)
            {
                throw new DataMisalignedException("Found multiple records with the same ID");
            }
            if (numberOfElements == 0) {
                return false;
            }

            this.saveRawTextFile("comments", JsonConvert.SerializeObject(commentList));
            return true;
        }

        public bool CreateFavoritePost(int postId) {
            var allFavoritePosts = this.GetFavoritePosts();
            if (allFavoritePosts.Contains(postId)) {
                return false;
            }
            allFavoritePosts.Add(postId);
            this.saveRawTextFile("favoritePosts", JsonConvert.SerializeObject(allFavoritePosts));
            return true;
        }

        public bool CreateComment(CommentForCreation comment)
        {
            var allComments = this.GetComments();
            var nextCommentId = allComments.OrderByDescending(c => c.id).FirstOrDefault().id + 1;
            var newComment = new Comment() {
                id = nextCommentId,
                postId = comment.postId,
                body = comment.body,
                email = comment.email,
                name = ""
            };

            allComments.Add(newComment);
            this.saveRawTextFile("comments", JsonConvert.SerializeObject(allComments));
            return true;
        }
    }
}
