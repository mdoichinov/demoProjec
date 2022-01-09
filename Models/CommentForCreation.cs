using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace demoProject.Models
{
    public class CommentForCreation
    {
        public int postId { get; set; }
        public string body { get; set; }
        public string email { get; set; }
    }
}
