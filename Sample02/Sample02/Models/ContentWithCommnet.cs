using System;
using System.Collections.Generic;
using System.Text;

namespace Sample02.Models
{
    public class ContentWithCommnet
    {
        public int id { get; set; }

        public string title { get; set; }

        public string content { get; set; }

        public int status { get; set; }

        public DateTime add_time { get; set; }

        public DateTime? modify_time { get; set; }

        public IEnumerable<Comment> comments { get; set; }
    }
}
