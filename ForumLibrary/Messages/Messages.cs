using System;
using System.Collections.Generic;
using Microsoft.Data.Sqlite;
using System.Linq;
using Dapper;

namespace ForumLibrary
{
    public class Messages
    {
        public int messageId { get; set; }
        public int threadId { get; set; }
        public int ownerId { get; set; }
        public string dateCreated { get; set; }
        public string name { get; set; }
        public string message { get; set; }
        public int visible { get; set; }
    }
}
