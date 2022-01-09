using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace demoProject.DtoModels
{
    public class PostListDto
    {
        public int id { get; set; }
        public string authorName { get; set; }
        public string postTitle { get; set; }
        public int numberOfComments { get; set; }
        public bool isFavorite { get; set; }
    }
}