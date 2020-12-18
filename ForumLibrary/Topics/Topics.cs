using System;
using System.Collections.Generic;
using Microsoft.Data.Sqlite;
using System.Linq;
using Dapper;

namespace ForumLibrary
{
    public class Topics
    {
        public int topicId { get; set; }
        public int ownerId { get; set; }
        public string dateCreated { get; set; }
        public string name { get; set; }
        public string description { get; set; }
        public int visible { get; set; }
    }
}
