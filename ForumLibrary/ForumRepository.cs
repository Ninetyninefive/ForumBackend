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

        public IList<Users> GetUsers()
        {
            using var connection = new SqliteConnection(_connectionString);
            var output = connection.Query<Users>("SELECT * FROM Users");
            return output.ToList();
        }

        public IList<Topics> GetTopics()
        {
            using var connection = new SqliteConnection(_connectionString);
            var output = connection.Query<Topics>("SELECT * FROM Topics");
            return output.ToList();
        }

        public IList<Threads> GetThreads()
        {
            using var connection = new SqliteConnection(_connectionString);
            var output = connection.Query<Threads>("SELECT * FROM Topics");
            return output.ToList();
        }
        public IList<Messages> GetMessages()
        {
            using var connection = new SqliteConnection(_connectionString);
            var output = connection.Query<Messages>("SELECT * FROM Topics");
            return output.ToList();
        }

        public void AddUser(Users newUser)
        {
            using var connection = new SqliteConnection(_connectionString);
            var sql = $"INSERT INTO Users (nickname, firstname, lastname, dateCreated) " +
                $"VALUES (@nickName, @firstName, @lastName, DATE('now')";
            connection.Execute(sql, newUser);
        }

        public void NewTopic(Topics newTopic)
        {
            using var connection = new SqliteConnection(_connectionString);
            var sql = $"INSERT INTO Topics (ownerId, dateCreated, name, description, visible  " +
                $"VALUES (@ownerId, DATE('now'), @name, @description, @visible";
            connection.Execute(sql, newTopic);
        }

        public void NewThread(Threads newThread)
        {
            using var connection = new SqliteConnection(_connectionString);
            var sql = $"INSERT INTO Threads (subject, visible, lastname, dateCreated " +
                $"VALUES (@nickName, @firstName, @lastName, DATE('now')";
            connection.Execute(sql, newThread);
        }

        public void NewMessage(Messages newMessage)
        {
            using var connection = new SqliteConnection(_connectionString);
            var sql = $"INSERT INTO Messages (subject, visible, lastname, dateCreated " +
                $"VALUES (@nickName, @firstName, @lastName, DATE('now')";
            connection.Execute(sql, newMessage);
        }
    }
}
