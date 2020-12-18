using System;
using ForumLibrary;
using System.Collections.Generic;
using System.Linq;

namespace ForumUI
{
    public class ForumViewer
    {
        
        public ForumViewer()
        {

        }
        public void Run()
        {
            var message = "";
            Users currentuser = null;
            Topics currenttopic = null;
            Threads currentthread = null;
            Messages currentmessage = null;

            var repo = new SQLiteForumRepository();
            IList<Users> users = repo.GetUsers();
            IList<Topics> topics = repo.GetTopics();
            IList<Threads> threads = repo.GetThreads();
            IList<Messages> messages = repo.GetMessages();

            Console.WriteLine("Welcome to ForumViewer");
            Console.WriteLine("Who are you? (nickname)");

            var inputName = Console.ReadLine();
            foreach (var user in users)
            {
                if(inputName == user.nickName)
                {
                    currentuser = user;
                }
                else
                {
                    message = "Try again.";
                    inputName = Console.ReadLine();
                }
            }
            Console.Clear();
            Console.WriteLine($"Logged on as: {currentuser.nickName} ID: {currentuser.userId}");

            message = "Make selection";
            Console.WriteLine(message);

            Console.WriteLine("1. Create new User");
            Console.WriteLine("2. Browse Forum");




        }
    }
}
