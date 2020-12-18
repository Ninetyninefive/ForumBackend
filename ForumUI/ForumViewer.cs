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

        public Users CreateUser()
        {
            var newUser = new Users();
            Console.Clear();
            Console.WriteLine("Enter nickname:");
            var inputNewNickname = Console.ReadLine();

            Console.WriteLine("Enter first name:");
            var inputNewFirstName = Console.ReadLine();

            Console.WriteLine("Enter last name:");
            var inputNewLastName = Console.ReadLine();

            newUser.nickName = inputNewNickname;
            newUser.firstName = inputNewFirstName;
            newUser.lastName = inputNewLastName;

            return newUser;
        }

        public Topics CreateTopic(Users currentUser)
        {
            
            Console.Clear();
            Console.WriteLine("Enter TOPIC name:");
            var inputNewTopicName = Console.ReadLine();

            Console.WriteLine("Enter DESCRIPTION:");
            var inputNewTopicDescription = Console.ReadLine();

            var newTopic = new Topics();

            newTopic.ownerId = currentUser.userId;
            newTopic.name = inputNewTopicName;
            newTopic.description = inputNewTopicDescription;
            newTopic.visible = 1;
            
            return newTopic;
        }

        public void Run()
        {
            var message = "";
            Users currentUser = null;
            Topics currentTopic = null;
            Threads currentThread = null;
            Messages currentMessage = null;

            var repo = new SQLiteForumRepository();
            IList<Users> users = repo.GetUsers();
            IList<Topics> topics = repo.GetTopics();
            IList<Threads> threads = repo.GetThreads();
            IList<Messages> messages = repo.GetMessages();

            Console.WriteLine("Welcome to ForumViewer");
            Console.WriteLine("Who are you? (enter nickname) [or type'create' to add new forum user]\n");
            Console.WriteLine("UserList: \n");
            foreach (var item in users)
            {
                    Console.WriteLine($"{item.nickName} {item.firstName} {item.lastName} {item.dateCreated}");
            }
            while (currentUser == null)
            {
                var inputName = Console.ReadLine();
                foreach (var user in users)
                {
                    if (inputName == "create")
                    {
                        currentUser = CreateUser();
                    }
                    if (inputName == user.nickName || inputName == user.userId.ToString())
                    {
                        currentUser = user;
                    }
                    else
                    {
                        message = "Try again. [Enter user nickname or ID] or type 'create' to create a new user.";
                        inputName = Console.ReadLine();
                    }
                }
            }
            Console.Clear();
            Console.WriteLine($"Logged on as: {currentUser.nickName} ID: {currentUser.userId}");

            Console.WriteLine("Select Topic\n");
            foreach (var item in topics)
            {
                if (item.visible == 1)
                    Console.WriteLine($"{item.topicId} {item.name} {item.description} {item.ownerId} {item.dateCreated}");
            }
            while (currentTopic == null)
            {
                var inputTopicName = Console.ReadLine();
                foreach (var topic in topics)
                {
                    if (inputTopicName == "create")
                    {
                        currentTopic = CreateTopic(currentUser);
                    }
                    if (inputTopicName == topic.name || inputTopicName == topic.topicId.ToString())
                    {
                        currentTopic = topic;
                    }
                    else
                    {
                        message = "Try again. [Enter topic name or ID to select or 'create' to start new topic.]";
                        inputTopicName = Console.ReadLine();
                    }
                }
            }




        }
    }
}
