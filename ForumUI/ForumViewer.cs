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
            newUser.FirstName = inputNewFirstName;
            newUser.LastName = inputNewLastName;

            return newUser;
        }

        public string GetUserNameFromID(IList<Users> users, int ownerID)
        {
            var result = "";
            foreach (var item in users)
            {
                if(item.userId == ownerID)
                {
                    result = item.nickName;
                }
            }
            return result;
        }
        public Topics CreateTopic(Users currentUser)
        {
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

        public Threads CreateThread(Topics currentTopic, Users currentUser)
        {
            Console.WriteLine("Enter thread subject:");
            var inputNewThreadSubject = Console.ReadLine();

            var newThread = new Threads();

            newThread.topicId = currentTopic.topicId;
            newThread.ownerId = currentUser.userId;
            newThread.subject = inputNewThreadSubject;
            newThread.visible = 1;

            return newThread;
        }

        public Messages EditMessage(Messages messageToEdit, Users currentUser)
        {
            Console.WriteLine("Enter NEW message:");
            
            var messageToInsert = Console.ReadLine();
            messageToEdit.message = messageToInsert;
            messageToEdit.ownerId = currentUser.userId;
            
            return messageToEdit;
        }


        public Messages CreateMessage(Threads currentThread, Users currentUser)
        {
            Console.WriteLine("Enter message:");
            var inputNewMessage = Console.ReadLine();

            var newMessage = new Messages();

            newMessage.threadId = currentThread.threadId;
            newMessage.ownerId = currentUser.userId;

            newMessage.message = inputNewMessage;
            newMessage.visible = 1;

            return newMessage;
        }

        public string ConCatFullName(string firstName, string lastName)
        {
            return firstName + " " + lastName;
        }

        public void Run()
        {
            var messageMenu = "";
            Users currentUser = null;
            Topics currentTopic = null;
            Threads currentThread = null;
            Messages currentMessage = null;

            var repo = new SQLiteForumRepository();

            var users = repo.GetUsers();
            var topics = repo.GetTopics();
            var threads = repo.GetThreads();
            var messages = repo.GetMessages();

            if (users.Count() == 0)
            {
                var defUser = CreateUser();
                repo.AddUser(defUser);
                users = repo.GetUsers();
            }
            var menuChoice = "";
            while (!(menuChoice == "quit"))
            {
                
                while (currentUser == null)
                {
                    if (menuChoice == "quit")
                    {

                        Environment.Exit(-1);
                    }
                    Console.Clear();
                    Console.WriteLine("\n\nWelcome to ForumViewer ['back' for User Selection || 'quit' to Quit]");


                    Console.WriteLine("\n\tRecent Forum Activity:\n");
                    var recent = repo.LatestMessages();

                    foreach (var item in recent)
                    {
                       // if(item.dateCreated == DateTime.Now)
                        Console.WriteLine($"\t\t{item.message} @({item.dateCreated} by {item.nickName})");
                    }
                    var time = DateTime.Now;
                    Console.WriteLine(time);

                    Console.WriteLine($"\n\nID\tNickname\tFull name\t  Join Date\n");
                    foreach (var item in users)
                    {
                        Console.WriteLine($"{item.userId,-7} {item.nickName,-15} {ConCatFullName(item.FirstName,item.LastName), -15}   {item.dateCreated,-5}");
                    }
                    messageMenu = "\nSelect User: [ID or nickname to select -- 'new' -- 'delete']\n";
                    Console.WriteLine(messageMenu);

                    menuChoice = Console.ReadLine();

                    if (menuChoice == "new")
                    {
                        var newUser = CreateUser();
                        repo.AddUser(newUser);
                        users = repo.GetUsers();
                    }
                    if (menuChoice == "back")
                    {
                        Run();
                    }
                    if (menuChoice == "delete")
                    {
                        Console.WriteLine("\nEnter user ID to delete:");
                        var deletionId = Console.ReadLine();
                        foreach (var item in users)
                        {
                            if (item.userId == Convert.ToInt32(deletionId))
                            {
                                repo.DeleteUser(item);
                                currentUser = null;
                                currentTopic = null;
                                currentThread = null;
                                currentMessage = null;
                            }
                        }
                        users = repo.GetUsers();
                    }
                    foreach (var user in users)
                    {
                        if (menuChoice == user.nickName || menuChoice == user.userId.ToString())
                        {
                            currentUser = user;
                        }
                    }
                }

                while (currentTopic == null)
                {
                    if (menuChoice == "quit")
                    {

                        Environment.Exit(-1);
                    }
                    Console.Clear();
                    Console.WriteLine($"Browsing as: {currentUser.nickName} [ID: {currentUser.userId}]");
                    Console.WriteLine($"\n\nID\tTOPIC\t\t    Description\t\t\t\t\t\t\t Owner\t\t Created\n");
                    foreach (var topic in topics)
                    {
                        if (topic.visible == 1)
                            Console.WriteLine($"{topic.topicId,-7} {topic.name,-20}{topic.description,-60} {GetUserNameFromID(users, topic.ownerId),-15} {topic.dateCreated,-10}");
                    }
                    messageMenu = "\nSelect Topic [ID or NAME to select -- 'new' -- 'delete' -- 'back']\n";
                    Console.WriteLine(messageMenu);

                    menuChoice = Console.ReadLine();

                    if (menuChoice == "new")
                    {
                        var newTopic = CreateTopic(currentUser);
                        repo.NewTopic(newTopic);
                        currentTopic = null;
                        currentThread = null;
                        currentMessage = null;
                        topics = repo.GetTopics();
                    }
                    if (menuChoice == "back")
                    {
                        Run();
                    }
                    if (menuChoice == "delete")
                    {
                        Console.WriteLine("Enter TOPIC ID to delete");
                        var deletionId = Console.ReadLine();
                        foreach (var item in topics)
                        {
                            if (item.topicId == Convert.ToInt32(deletionId))
                            {
                                repo.DeleteTopic(item);
                                currentTopic = null;
                                currentThread = null;
                                currentMessage = null;
                            }
                        }
                        topics = repo.GetTopics();
                    }

                    foreach (var topic in topics)
                    {
                        if (menuChoice == topic.name || menuChoice == topic.topicId.ToString())
                        {
                            currentTopic = topic;
                        }
                    }
                }

                while (currentThread == null)
                {
                    if (menuChoice == "quit")
                    {

                        Environment.Exit(-1);
                    }
                    Console.Clear();
                    Console.WriteLine($"Browsing as: {currentUser.nickName} [ID: {currentUser.userId}]");
                    Console.WriteLine($"  >>TOPIC: {currentTopic.name} ({currentTopic.description}) ID:{currentTopic.topicId} Created:{currentTopic.dateCreated}");

                    Console.WriteLine($"\n\nID\tSubject\t\t\t\t\t\t   Started by\n");
                    foreach (var thread in threads)
                    {
                        if (thread.topicId == currentTopic.topicId)
                            Console.WriteLine($"{thread.threadId,-7} {thread.subject,-50} {GetUserNameFromID(users, thread.ownerId),-15}");
                    }
                    messageMenu = "\nThread [ID or NAME to select -- 'new' -- 'delete' -- 'back']\n";
                    Console.WriteLine(messageMenu);
                    menuChoice = Console.ReadLine();

                    if (menuChoice == "new")
                    {
                        var newThread = CreateThread(currentTopic, currentUser);
                        repo.NewThread(newThread);
                        currentThread = null;
                        currentMessage = null;
                        threads = repo.GetThreads();
                    }
                    if (menuChoice == "back")
                    {
                        currentThread = null;
                        currentMessage = null;
                        topics = repo.GetTopics();
                    }
                    if (menuChoice == "delete")
                    {
                        Console.WriteLine("Enter THREAD ID to delete");
                        var deletionId = Console.ReadLine();
                        foreach (var item in threads)
                        {
                            if (item.threadId == Convert.ToInt32(deletionId))
                            {
                                repo.DeleteThread(item);
                                currentThread = null;
                                currentMessage = null;
                            }
                        }
                        threads = repo.GetThreads();
                    }
                    foreach (var thread in threads)
                    {
                        if (menuChoice == thread.subject || menuChoice == thread.threadId.ToString())
                        {
                            currentThread = thread;
                        }
                    }
                }

                while (currentMessage == null)
                {
                    if (menuChoice == "quit")
                    {

                        Environment.Exit(-1);
                    }
                    Console.Clear();
                    Console.WriteLine($"Browsing as: {currentUser.nickName} [ID: {currentUser.userId}]");
                    Console.WriteLine($"  >>TOPIC: {currentTopic.name} ({currentTopic.description}) ID:{currentTopic.topicId} Created:{currentTopic.dateCreated}");
                    Console.WriteLine($"    >>THREAD: {currentThread.subject} ID:{currentThread.threadId}  Owner:{GetUserNameFromID(users, currentThread.ownerId)} Created:{currentThread.lastPostDate}");

                    Console.WriteLine($"\n\nID\tDATE\t   CREATED BY\t    MESSAGE\n");
                    foreach (var message in messages)
                    {
                        if (message.threadId == currentThread.threadId)
                        {
                            Console.WriteLine($"{message.messageId,-7} {message.dateCreated,-10} {GetUserNameFromID(users, message.ownerId),-15} {message.message,-120}");
                        }
                    }

                    messageMenu = "\nMessages [ 'new' -- 'edit' -- 'delete' -- 'back' -- 'quit' ]\n";
                    Console.WriteLine(messageMenu);
                    menuChoice = Console.ReadLine();

                    if (menuChoice == "new")
                    {
                        var newMessage = CreateMessage(currentThread, currentUser);
                        repo.NewMessage(newMessage);
                        currentMessage = null;
                        messages = repo.GetMessages();
                    }
                    if (menuChoice == "back")
                    {
                        Run();
                    }
                    if (menuChoice == "delete")
                    {
                        Console.WriteLine("Enter MESSAGE ID to delete");
                        var deletionId = Console.ReadLine();
                        foreach (var item in messages)
                        {
                            if (item.messageId == Convert.ToInt32(deletionId))
                            {
                                repo.DeleteMessage(item);
                                currentMessage = null;
                            }
                        }
                        messages = repo.GetMessages();
                    }
                    if (menuChoice == "edit")
                    {
                        Console.WriteLine("Enter MESSAGE ID to edit");
                        var editId = Console.ReadLine();
                        foreach (var item in messages)
                        {
                            if (item.messageId == Convert.ToInt32(editId))
                            {
                                var editMessage = EditMessage(item, currentUser);
                                repo.EditMessage(editMessage);
                                currentMessage = null;
                            }
                        }
                        messages = repo.GetMessages();
                    }
                }
            }
        }
    }
}
