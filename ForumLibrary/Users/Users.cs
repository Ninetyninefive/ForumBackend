using System;
using System.Collections.Generic;
using Microsoft.Data.Sqlite;
using System.Linq;
using Dapper;

namespace ForumLibrary
{
    public class Users
    {
        public int userId { get; set; }
        public string nickName { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string dateCreated { get; set; }
    }
}