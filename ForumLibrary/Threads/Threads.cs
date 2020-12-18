using System;
using System.Collections.Generic;
using Microsoft.Data.Sqlite;
using System.Linq;
using Dapper;

namespace ForumLibrary
{
    public class Threads
    {
        public int threadId { get; }
        public int topicId { get; }
        public int ownerId { get; }
        public string lastPostDate { get; set; }
        public string subject { get; set; }
        public int visible { get; set; }
        List<Messages> Messages { get; set; }
    }
}
