using System;
using System.Collections.Generic;
using Microsoft.Data.Sqlite;
using System.Linq;
using Dapper;

namespace ForumLibrary
{
    public class Users
    {
        public int userId { get; }
        public string nickName { get; set; }
        public string firstName { get; set; }
        public string lastName { get; set; }
        public string dateCreated { get; set; }
        List<Topics> Topics { get; set; }
        List<Threads> Threads { get; set; }
        List<Messages> Messages { get; set; }
    }
}
