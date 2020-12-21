using System;
using System.Collections.Generic;
using Microsoft.Data.Sqlite;
using System.Linq;
using Dapper;

namespace ForumLibrary
{
    public class Threads
    {
        public int threadId { get; set; }
        public int topicId { get; set; }
        public int ownerId { get; set; }
        public string subject { get; set; }
        public int visible { get; set; }
    }
}
