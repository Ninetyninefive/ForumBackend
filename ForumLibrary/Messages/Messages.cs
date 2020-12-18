using System;
using System.Collections.Generic;
using Microsoft.Data.Sqlite;
using System.Linq;
using Dapper;

namespace ForumLibrary
{
    public class Messages
    {
        public int threadid { get; }
        public int ownerid { get; }
        public string dateCreated { get; set; }
        public string name { get; set; }
        public string description { get; set; }
        public int visible { get; set; }
    }
}
