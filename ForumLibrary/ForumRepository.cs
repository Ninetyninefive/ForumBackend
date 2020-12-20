using System;
using System.Collections.Generic;
using Microsoft.Data.Sqlite;
using System.Linq;
using Dapper;

namespace ForumLibrary
{
    public class SQLiteForumRepository
    {
        private const string _connectionString = "Data Source= Forum.db";

        public IList<Users> GetUsers()
        {
            using var connection = new SqliteConnection(_connectionString);
            var output = connection.Query<Users>("SELECT * FROM Users;");
            return output.ToList();
        }

        public IList<Topics> GetTopics()
        {
            using var connection = new SqliteConnection(_connectionString);
            var output = connection.Query<Topics>("SELECT * FROM Topics;");
            return output.ToList();
        }

        public IList<Threads> GetThreads()
        {
            using var connection = new SqliteConnection(_connectionString);
            var output = connection.Query<Threads>("SELECT * FROM Threads;");
            return output.ToList();
        }
        public IList<Messages> GetMessages()
        {
            using var connection = new SqliteConnection(_connectionString);
            var output = connection.Query<Messages>("SELECT * FROM Messages;");
            return output.ToList();
        }

        public void AddUser(Users newUser)
        {
            using var connection = new SqliteConnection(_connectionString);
            var sql = $"INSERT INTO Users (nickname,Firstname,Lastname, dateCreated) " +
                $"VALUES (@nickName, @FirstName, @LastName, DATE('now'));";
            connection.Execute(sql, newUser);
        }

        public void DeleteUser(Users userToDeleteById)
        {
            using var connection = new SqliteConnection(_connectionString);
            var sql = $"DELETE FROM Users WHERE userId = @userId;";
            connection.Execute(sql, userToDeleteById);
        }

        public void NewTopic(Topics newTopic)
        {
            using var connection = new SqliteConnection(_connectionString);
            var sql = $"INSERT INTO Topics (ownerId, dateCreated, name, description, visible) " +
                $"VALUES (@ownerId, DATE('now'), @name, @description, @visible);";
            connection.Execute(sql, newTopic);
        }

        public void DeleteTopic(Topics topicToDeleteByID)
        {
            using var connection = new SqliteConnection(_connectionString);
            var sql = $"DELETE FROM Topics WHERE topicId = @topicId;";
            connection.Execute(sql, topicToDeleteByID);
        }
        public void NewThread(Threads newThread)
        {
            using var connection = new SqliteConnection(_connectionString);
            var sql = $"INSERT INTO Threads (topicId, ownerId, subject, visible) " +
                $"VALUES (@topicId, @ownerId, @subject, @visible);";
            connection.Execute(sql, newThread);
        }

        public void DeleteThread(Threads threadToDeleteByID)
        {
            using var connection = new SqliteConnection(_connectionString);
            var sql = $"DELETE FROM Topics WHERE threadId = @threadId;";
            connection.Execute(sql, threadToDeleteByID);
        }
        public void NewMessage(Messages newMessage)
        {
            using var connection = new SqliteConnection(_connectionString);
            var sql = $"INSERT INTO Messages (threadId, ownerId, dateCreated, message, visible)" +
                $"VALUES (@threadId, @ownerId, DATE('now'), @message, @visible)";
            connection.Execute(sql, newMessage);
        }

        public void EditMessage(Messages editMessage)
        {
            using var connection = new SqliteConnection(_connectionString);
            var sql = $"UPDATE Messages SET message = @message WHERE messageId = @messageId;";
            connection.Execute(sql, editMessage);
        }

        public void DeleteMessage(Messages messageToDeleteByID)
        {
            using var connection = new SqliteConnection(_connectionString);
            var sql = $"DELETE FROM Messages WHERE messageId = @messageId;";
            connection.Execute(sql, messageToDeleteByID);
        }

        public List<string> ShowAllUsersThatHasMessages()
        {
            using var connection = new SqliteConnection(_connectionString);
            var output = connection.Query<string>("SELECT * FROM Users AS U JOIN Messages AS M ON U.userId = M.ownerId;");
            return output.ToList();
        }
    }
}
