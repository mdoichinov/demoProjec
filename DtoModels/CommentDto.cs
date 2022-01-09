using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace demoProject.DtoModels
{
    public class CommentDto
    {
        public int id { get; set; }
        public string authorEmail { get; set; }
        public string comment { get; set; }
    }
}
