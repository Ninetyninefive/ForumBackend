using System;
using System.Collections.Generic;
using Microsoft.Data.Sqlite;
using System.Linq;
using Dapper;

namespace ForumLibrary
{
    public class History
    {
        public int messageId { get; set; }
        public string nickName { get; set; }
        public string message { get; set; }
        public string dateCreated { get; set; }
    }
}
