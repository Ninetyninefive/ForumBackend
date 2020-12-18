using System;
using System.Collections.Generic;
using Microsoft.Data.Sqlite;
using System.Linq;
using Dapper;

namespace ForumLibrary
{
    public class SQLiteForumRepository
    {
        private const string _connectionString = "Data Source= .\\Forum.db";

        private void ServerVersion()
        {
            using var connection = new SqliteConnection(_connectionString);
            //print version
            Console.WriteLine(connection.ServerVersion);
        }

        private IList<Users> GetUsers()
        {
            using var connetion = new SqliteConnection(_connectionString);
            var output = connetion.Query<Users>("SELECT * FROM Users");
            return output.ToList();
        }

        private IList<Topics> GetTopics()
        {
            using var connetion = new SqliteConnection(_connectionString);
            var output = connetion.Query<Topics>("SELECT * FROM Topics");
            return output.ToList();
        }

        private IList<Threads> GetThreads()
        {
            using var connetion = new SqliteConnection(_connectionString);
            var output = connetion.Query<Threads>("SELECT * FROM Topics");
            return output.ToList();
        }
        private IList<Messages> GetMessages()
        {
            using var connetion = new SqliteConnection(_connectionString);
            var output = connetion.Query<Messages>("SELECT * FROM Topics");
            return output.ToList();
        }
    }
}
