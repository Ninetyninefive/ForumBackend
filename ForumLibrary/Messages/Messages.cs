using System;
using System.Collections.Generic;
using Microsoft.Data.Sqlite;
using System.Linq;
using Dapper;

namespace ForumLibrary
{
    public class Messages
    {
        public int threadid { get; set; }
        public int ownerid { get; set; }
        public string dateCreated { get; set; }
        public string name { get; set; }
        public string description { get; set; }
        public int visible { get; set; }
    }
}
